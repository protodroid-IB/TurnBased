using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyOverworld : MonoBehaviour
{


    // the nav mesh agent
    private NavMeshAgent agent;

    private GameObject playerGO;

    private GameController gameController;

    private Enemy enemy;

    // the states the the enmey can be in 
    private enum State {Idle, Aggressive, GoHome, NoDetect, Defeated};

    // the current state the enemy is in
    private State currentState = State.Idle;

    private Vector3 currentPosition;




    [Header("IDLE STATE PROPERTIES")]
    [Space(20)]

    [SerializeField]
    private SphereCollider idleRangeCollider;

    [Header("Percentage of idle range sphere collider radius")]
    [SerializeField]
    [Range(1f, 95f)]
    private float idleMinMoveDist = 50f;
    
    [SerializeField]
    [Range(1f, 95f)]
    private float idleMaxMoveDist = 95f;

    [Header("Time between moves")]
    [SerializeField]
    private float idleMinMoveTime = 4f;
    
    [SerializeField] 
    private float idleMaxMoveTime = 8f;

    [SerializeField]
    private float idleMoveSpeed = 1.2f;

    [Header("Maximum distance allowed away from idle range center")]
    [SerializeField]
    private float maximumDistanceAway = 20f;


    private float idleRandomDistance = 0f;
    private Vector3 idleMovePoint;

    private float idleRandomTime = 0f;
    private float idleTimer = 0f;



    [Space(20)]
    [Header("AGGRESSIVE STATE PROPERTIES")]
    [Space(20)]

    [SerializeField]
    private SphereCollider detectRangeCollider;

    [SerializeField]
    private float aggressiveMoveSpeed = 2.2f;

    [SerializeField]
    private float triggerBattleDistance = 1f;





    [Space(20)]
    [Header("GO HOME STATE PROPERTIES")]
    [Space(20)]

    [SerializeField]
    private float distanceUntilDetectPlayer = 5f;





    [Space(20)]
    [Header("NO DETECT STATE PROPERTIES")]
    [Space(20)]

    [SerializeField]
    private float timeUntilAllowedToDetect = 8f;
    private float allowedToDetectTimer;




    // Use this for initialization
    void Start ()
    {
        // grab the nav mesh agent component
        agent = GetComponent<NavMeshAgent>();

        // grab the player game object
        playerGO = GameObject.FindWithTag("Player");

        gameController = GameObject.FindWithTag("GameController").GetComponent<GameController>();

        enemy = transform.parent.GetComponent<Enemy>();

        allowedToDetectTimer = timeUntilAllowedToDetect;
	}
	




	// Update is called once per frame
	void Update ()
    {
        // if the game state is currently in the overworld
        if (GameController.gameState == GameState.Overworld)
        {
            currentPosition = transform.position;

            // switch that controls which method is called based on the current state of the enemy
            switch (currentState)
            {
                case State.Idle:
                    Idle();
                    break;

                case State.Aggressive:
                    Aggressive();
                    break;

                case State.Defeated:
                    Defeated();
                    break;

                case State.GoHome:
                    GoHome();
                    break;

                case State.NoDetect:
                    NoDetect();
                    break;

                default:
                    Idle();
                    break;
            }
        }


	}






    private void Idle()
    {
        //
        // STATE CHANGES
        //

        // if the player is within the detect range
        if(detectRangeCollider.bounds.Contains(playerGO.transform.position))
        {
            currentState = State.Aggressive;
        }

        // if enemy isnt alive
        TriggerDefeated();

        //if player is close enought to trigger the battle
        TriggerBattle();




        //
        // ENEMY BEHAVIOUR
        //

        agent.speed = idleMoveSpeed;

        // if the enemy is within the idle range collider bounding sphere- make the enemy move around within the sphere
        if (idleRangeCollider.bounds.Contains(transform.position))
        {
            // if no movement distance has been calculated yet
            if (idleRandomDistance == 0f)
            {
                // generate random number that will be a percentage of the minimum distance to the idle range bounds
                idleRandomDistance = Random.Range(idleMinMoveDist, idleMaxMoveDist);

                // finds the minimum distance between the enemy and the bounds
                float distanceToBounds = idleRangeCollider.radius - DistanceToIdleRangeCenter();

                // find a point for the agent to move to within the range
                idleMovePoint = Random.insideUnitSphere * distanceToBounds * ((idleRandomDistance) / 100f);

                // determines the time until the agent makes the move
                idleRandomTime = Random.Range(idleMinMoveTime, idleMaxMoveTime);

                // set the timer
                idleTimer = idleRandomTime;
            }

            // if the timer is complete
            if (idleTimer <= 0f)
            {
                // move the agent to the new idle point
                agent.SetDestination(idleMovePoint);

                // reset the distance and timer variables
                idleRandomDistance = 0f;
                idleTimer = idleRandomTime;
            }

            // if the agent has stopped moving, start counting down the timer
            idleTimer -= Time.deltaTime;

            //Debug.Log(idleTimer);
        }

        // if the enemy is no longer in the collider sphere
        else
        {
            // move back to center of the idle range collider
            agent.SetDestination(idleRangeCollider.bounds.center);
        }
    }



    private void Aggressive()
    {
        //
        // STATE CHANGES
        //

        // change to IDLE if the player is not within the detect bounds of the enemy
        if (!detectRangeCollider.bounds.Contains(playerGO.transform.position)) currentState = State.Idle;

        // change to GO HOME if the enemy is more than maximum allowed distance from home
        if (DistanceToIdleRangeCenter() >= maximumDistanceAway) currentState = State.GoHome;

        // if enemy isnt alive
        TriggerDefeated();

        //if player is close enought to trigger the battle
        TriggerBattle();




        //
        // ENEMY BEHAVIOUR
        //

        //set speed to aggressive speed and move the enemy towards the player
        agent.speed = aggressiveMoveSpeed;
        agent.SetDestination(playerGO.transform.position);
    }





    private void GoHome()
    {
        //
        // STATE CHANGES
        //

        // if the enemy is allowed to detect the player again
        if(DistanceToIdleRangeCenter() <= distanceUntilDetectPlayer)
        {
            currentState = State.Idle;
        }

        // if enemy isnt alive
        TriggerDefeated();

        //if player is close enought to trigger the battle
        TriggerBattle();

        //
        // ENEMY BEHAVIOUR
        //

        agent.speed = idleMoveSpeed;
        agent.SetDestination(idleRangeCollider.bounds.center);
    }



    private void NoDetect()
    {
        //
        // STATE CHANGES
        //

        // if the enemy is allowed to detect the player again
        if (allowedToDetectTimer <= 0f)
        {
            currentState = State.GoHome;
            allowedToDetectTimer = timeUntilAllowedToDetect;
        }
        else
        {
            allowedToDetectTimer -= Time.deltaTime;
        }

        // if enemy isnt alive
        TriggerDefeated();

        //if player is close enought to trigger the battle
        TriggerBattle();

        //
        // ENEMY BEHAVIOUR
        //

        agent.speed = idleMoveSpeed;
        agent.SetDestination(idleRangeCollider.bounds.center);
    }







    private void Defeated()
    {
        transform.parent.gameObject.SetActive(false);

        if (enemy.GetAlive()) currentState = State.Idle;
    }









    public void TriggerBattle()
    {
        // trigger transition to battle state
        if (DistanceToPlayer() <= triggerBattleDistance)
        {
            gameController.SetBattleEnemy(transform.parent.gameObject);
            currentState = State.NoDetect;
            GameController.gameState = GameState.TransitionToBattle;
        }
    }

    public void TriggerDefeated()
    {
        // if enemy isnt alive
        if (enemy.GetAlive() == false) currentState = State.Defeated;
    }






    private float DistanceToIdleRangeCenter()
    {
        float dist = (transform.position - idleRangeCollider.bounds.center).magnitude;

        return dist;
    }


    private float DistanceToPlayer()
    {
        float dist = (playerGO.transform.position - transform.position).magnitude;

        return dist;
    }


    public Vector3 CurrentPosition()
    {
        return currentPosition;
    }


    //public void Kill()
    //{
    //    currentState = State.Defeated;
    //}



}
