using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomEnemy : MonoBehaviour
{
    private GameController gameController;

    private GameObject playerGO;
    private PlayerMovement playerMovement;

    private float encounterTimer;
    private float encounterTime = 0.5f;

    private bool enemyFound = false;

	// Use this for initialization
	void Start ()
    {
        playerGO = GameObject.FindWithTag("Player").gameObject;
        playerMovement = playerGO.GetComponent<PlayerMovement>();
        encounterTimer = encounterTime;

        gameController = GameObject.FindWithTag("GameController").GetComponent<GameController>();

    }


    private void OnTriggerStay(Collider other)
    {
        if (enemyFound == false)
        {
            if (other.CompareTag("Player"))
            {
                if (playerMovement.isMoving())
                {
                    if (encounterTimer <= 0f)
                    {
                        int d100 = Random.Range(1, 101);

                        if (d100 <= 15)
                        {
                            Debug.Log("ENEMY FOUND!");
                            gameController.TriggerRandomBattle();
                            enemyFound = true;
                        }

                        encounterTimer = encounterTime;
                    }
                    else
                    {
                        encounterTimer -= Time.deltaTime;
                    }
                }

            }
        }
    }
}
