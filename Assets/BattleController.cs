using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleController : MonoBehaviour
{
    private BattleState battleState = BattleState.Begin;

    private MoveLibrary moveLibrary;

    private Player player;
    private Enemy enemy;

    private GameObject battleDialogueGO;
    private Text battleDialogue;

    private GameObject moveSelectionGO;

    private Move selectedMove = Move.Punch;

    private bool playerTurn = false;
    private bool turnSwitched = false;

    private bool moveSelected = false;
    private bool moveDone = false;

    private float afterMoveTimer;

    [SerializeField]
    private float afterMoveStateChangeTime = 2f;

    private bool experienceRewarded = false;
    private bool battleDone = false;
    private bool playerWon = false;

    private int coinToss = 0;

    // Use this for initialization
    void Start ()
    {
        battleDialogueGO = GameObject.FindWithTag("BattleDialogue");
        battleDialogue = battleDialogueGO.transform.GetChild(0).GetComponent<Text>();
        battleDialogue.text = "";

        moveSelectionGO = GameObject.FindWithTag("MoveSelection").gameObject;

        moveLibrary = new MoveLibrary();
   
        player = GameObject.FindWithTag("BattlePlayer").GetComponent<Player>();
        enemy = GameObject.FindWithTag("BattleEnemy").GetComponent<Enemy>();

        afterMoveTimer = afterMoveStateChangeTime;

    }
	
	// Update is called once per frame
	void Update ()
    {
        BattleStateSwitch();	
	}

    private void BattleStateSwitch()
    {
        switch(battleState)
        {
            case BattleState.Begin:
                Begin();
                break;

            case BattleState.TurnSelect:
                TurnSelect();
                break;

            case BattleState.PlayerTurn:
                PlayerTurn();
                break;

            case BattleState.EnemyTurn:
                EnemyTurn();
                break;

            case BattleState.ApplyMove:
                ApplyMove();
                break;

            case BattleState.CheckBattle:
                CheckBattle();
                break;

            case BattleState.TurnSwitch:
                TurnSwitch();
                break;
        }
    }


    private void Begin()
    {
        // STATE CHANGES
        if (Input.GetMouseButtonDown(0))
        {
            battleState = BattleState.TurnSelect;
        }


        // BEHAVIOUR
        battleDialogue.text = "BEGIN BATTLE!";
    }


    private void TurnSelect()
    {
        // STATE CHANGES
        if (Input.GetMouseButtonDown(0))
        {
            if (playerTurn) battleState = BattleState.PlayerTurn;
            else battleState = BattleState.EnemyTurn;
        }


        // BEHAVIOUR
        if (player.GetSpeed() > enemy.GetSpeed()) playerTurn = true;
        else if (player.GetSpeed() < enemy.GetSpeed()) playerTurn = false;
        else
        {
            if(coinToss == 0)
            {
                coinToss = Random.Range(1, 3);
                if (coinToss == 1) playerTurn = true;
                else playerTurn = false;
            }
            
        }
       

        if (playerTurn) battleDialogue.text = "PLAYER ATTACKS FIRST!";
        else battleDialogue.text = enemy.GetName().ToUpper() + " ATTACKS FIRST!";
    }


    private void PlayerTurn()
    {
        battleDialogue.text = "PLAYER SELECT ATTACK!";

        moveSelectionGO.SetActive(true);

        // STATE CHANGES
        if (moveSelected == true) battleState = BattleState.ApplyMove;
    }


    private void EnemyTurn()
    {
        // BEHAVIOUR 
        int randNum = Random.Range(0, 4);

        switch(0)
        {
            case 0:
                selectedMove = enemy.GetMoves().move01;
                break;

            case 1:
                selectedMove = enemy.GetMoves().move02;
                break;

            case 2:
                selectedMove = enemy.GetMoves().move03;
                break;

            case 3:
                selectedMove = enemy.GetMoves().move04;
                break;
        }

        moveSelected = true;

        // STATE CHANGES
        if (moveSelected == true) battleState = BattleState.ApplyMove;

    }


    private void ApplyMove()
    {
        // BEHAVIOUR
        if(moveDone == false && moveSelected == true)
        {
            moveSelected = false;
            moveSelectionGO.SetActive(false);
            
            if(playerTurn) battleDialogue.text = "PLAYER USED " + selectedMove.ToString().ToUpper();
            else battleDialogue.text = enemy.GetName() + " USED " + selectedMove.ToString().ToUpper();

        }

        if(Input.GetMouseButtonDown(0) && moveSelected == false && moveDone == false)
        {
            moveDone = true;

            string battleText = "";

            moveLibrary.MoveSelect(ref battleText, selectedMove, player, enemy, playerTurn);

            battleDialogue.text = battleText;
        }



        // STATE CHANGE
        if (moveDone == true)
        {
            if (afterMoveTimer <= 0f)
            {
                battleState = BattleState.CheckBattle;
                afterMoveTimer = afterMoveStateChangeTime;
                moveDone = false;
            }
            else afterMoveTimer -= Time.deltaTime;
        }
    }


    private void CheckBattle()
    {

        // STATE CHANGE
        if (Input.GetMouseButtonDown(0))
        {
            if (battleDone) battleState = BattleState.End;

        }



        // BEHAVIOUR
        if (battleDone == false)
        { 
            if (player.GetCurrentHealth() <= 0)
            {
                playerWon = false;
                battleDialogue.text = "GAME OVER! YOU LOST! RETURNING TO MAIN MENU.";
                battleDone = true;
            }
            else if (enemy.GetCurrentHealth() <= 0)
            {
                if (experienceRewarded == true)
                {
                    if (Input.GetMouseButtonDown(0))
                    {
                        if (player.CheckLevelUp())
                        {
                            battleDialogue.text = "YOU GAINED A LEVEL!";
                            battleDone = true;
                        }
                        else
                        {
                            battleDone = true;
                        }
                    }
                }
                else
                {
                    experienceRewarded = true;

                    int gainedExperience = (int)(((float)enemy.GetMaxHealth() + ((float)enemy.GetStrength() + (float)enemy.GetDexterity() + (float)enemy.GetIntelligence()) / 3f) / 2f);
                    player.GainExperience(gainedExperience);
                    battleDialogue.text = "YOU GAINED " + gainedExperience + " EXPERIENCE!";

                    playerWon = true;
                }
            }
            else
            {
                battleState = BattleState.TurnSwitch;
            }
        }    
    }



    private void TurnSwitch()
    {
        // BEHAVIOUR

        if (playerTurn && turnSwitched == false)
        {
            playerTurn = false;
            turnSwitched = true;
        }
        else if (playerTurn == false && turnSwitched == false)
        {
            playerTurn = true;
            turnSwitched = true;
        }

        if (playerTurn) battleDialogue.text = "PLAYER'S TURN!";
        else battleDialogue.text = enemy.GetName().ToUpper() + "'S TURN!";

        // STATE CHANGES

        if (Input.GetMouseButtonDown(0))
        {
            if (playerTurn) battleState = BattleState.PlayerTurn;
            else battleState = BattleState.EnemyTurn;

            turnSwitched = false;
        }
    }







    public void Move1Selected()
    {
        selectedMove = player.GetMoves().move01;
        moveSelected = true;
    }

    public void Move2Selected()
    {
        selectedMove = player.GetMoves().move02;
        moveSelected = true;
    }

    public void Move3Selected()
    {
        selectedMove = player.GetMoves().move03;
        moveSelected = true;
    }

    public void Move4Selected()
    {
        selectedMove = player.GetMoves().move04;
        moveSelected = true;
    }




    public BattleState GetBattleState()
    {
        return battleState;
    }

    public bool GetPlayerWon()
    {
        return playerWon;
    }

    public bool GetBattleDone()
    {
        return battleDone;
    }
}
