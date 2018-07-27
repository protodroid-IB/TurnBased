using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class GameController : MonoBehaviour
{
    private SceneFader sceneFader;

    public static GameState gameState = GameState.Overworld;

    private GameObject playerGO;
    private Player playerWorld;
    private GameObject playerBattleGO;
    private Player playerBattle;
    private PlayerProperties playerProperties;

    private bool playerPosSet = true;
    private Vector3 playerLastPosition;

    private int enemyID = 0; 

    private GameObject enemiesInHierarchy; // gameobject that all enemies in overworld are children of
    private List<GameObject> enemiesWorldGO = new List<GameObject>(); // list of all enemy gameobjects in overworld
    private List<Enemy> enemiesWorld = new List<Enemy>(); // list of all enemy script instances in overworld

    private BattleController battleController;

    private GameObject battleEnemyGO; // enemy gameobject being battled
    private Enemy battleEnemy; // enemy script instance being battled
    private int battleEnemyIndex = 0;
    private bool justBattled = false;
    private bool battleTriggered = false;

    private List<EnemyProperties> enemiesProperties = new List<EnemyProperties>(); // updated persistent list of enemy properties

    private GameObject randomEnemiesInHierarchy;
    private GameObject[] randomEnemiesGO;
    private Enemy[] randomEnemies;
    private EnemyProperties randomEnemyPropeties;

    [SerializeField]
    private string overworldSceneName, battleSceneName;

    private bool randomBattle = false;

    private void Start()
    {
        gameState = GameState.Overworld;

        // grabs all necessary references
        UpdateReferences();
        InitialiseEnemyLists();
        InitialisePlayer();
    }


    private void Update()
    {
        GameStateSwitch();
    }




    private void GameStateSwitch()
    {
        switch (gameState)
        {
            case GameState.Overworld:
                Overworld();
                break;

            case GameState.Battle:
                Battle();
                break;

            case GameState.TransitionToBattle:
                TransitionToBattle();
                break;

            case GameState.TransitionToOverworld:
                TransitionToOverworld();
                break;

            default:
                gameState = GameState.Overworld;
                break;
        }
    }



    private void Overworld()
    {
        // if the scene is the correct scene
        if (sceneFader.GetCurrentSceneName().Equals(overworldSceneName))
        {
            if (playerPosSet == false)
            {
                playerGO.transform.position = playerLastPosition;
                playerPosSet = true;
            }
                

            // if a battle just occured
            if (justBattled == true)
            {
                battleTriggered = false;

                justBattled = false;

                playerPosSet = false;

                // updates all references needed for overworld scene
                UpdateReferences();

                // updates the enemy to reflect changes after battle
                UpdateWorldEnemiesAfterBattle();

                // updates the player to reflect changes after battle
                UpdateWorldPlayerAfterBattle();  
            }

            
        }
    }

    private void Battle()
    {
        //
        // STATE CHANGES
        //

        // if in the battle scene
        if (sceneFader.GetCurrentSceneName().Equals(battleSceneName))
        {
            if(battleEnemy == null)
            {
                // updates all references needed for battle scene
                UpdateReferences();

                // updates the properties of the battle enemy
                UpdateBattleEnemy();

                UpdateBattlePlayer();
            }


            // if the battle has finished

            // if the enemy is at 0 health
            if (battleController.GetBattleState() == BattleState.End)
            {
                if(battleController.GetPlayerWon())
                {
                    gameState = GameState.TransitionToOverworld;
                }
                else
                {
                    sceneFader.FadeIntoScene("MainMenu");
                }
                
            }

            
        }
    }




    private void TransitionToOverworld()
    {
        justBattled = true;

        if(randomBattle == false)
        {
            UpdateEnemyPropertiesAfterBattle();
        }
        else
        {
            randomBattle = false;
        }
        
        UpdatePlayerPropertiesAfterBattle();
        battleEnemy = null;
        battleEnemyIndex = 0;

        gameState = GameState.Overworld;
        sceneFader.FadeIntoScene(overworldSceneName);
    }


    private void TransitionToBattle()
    {
        battleTriggered = true;
        gameState = GameState.Battle;
        sceneFader.FadeIntoScene(battleSceneName);
        playerLastPosition = playerGO.transform.position;
    }








    // this function updates the references needed in each scene
    private void UpdateReferences()
    {
        // in every scene, grab the scene fader
        if (sceneFader == null)
            sceneFader = GameObject.Find("SceneFader").GetComponent<SceneFader>();



        // only when the game state is in overworld and the scene matches the game state, grab the enemiesInHierarchy gameobject
        if (gameState == GameState.Overworld)
        {
            if (sceneFader.GetCurrentSceneName().Equals(overworldSceneName))
            {
                if (enemiesInHierarchy == null) enemiesInHierarchy = GameObject.FindWithTag("HierarchyEnemies");


                if (randomEnemiesInHierarchy == null)
                {
                    randomEnemiesInHierarchy = GameObject.FindWithTag("RandomEnemies");

                    randomEnemiesGO = new GameObject[randomEnemiesInHierarchy.transform.childCount];
                    randomEnemies = new Enemy[randomEnemiesInHierarchy.transform.childCount];

                    for (int i = 0; i < randomEnemiesInHierarchy.transform.childCount; i++)
                    {
                        randomEnemiesGO[i] = randomEnemiesInHierarchy.transform.GetChild(i).gameObject;
                        randomEnemies[i] = randomEnemiesGO[i].GetComponent<Enemy>();
                    }
                }

                if (playerGO == null)
                {
                    playerGO = GameObject.FindWithTag("Player");
                    playerWorld = playerGO.GetComponent<Player>();
                }
            }
        }



        // only when the game state is in battle and the scene matches the game state, grab the battleEnemy gameobject and script
        if (gameState == GameState.Battle)
        {
            if (sceneFader.GetCurrentSceneName().Equals(battleSceneName))
            {
                if (battleEnemy == null)
                {
                    battleEnemyGO = GameObject.FindWithTag("BattleEnemy");
                    battleEnemy = battleEnemyGO.GetComponent<Enemy>();
                }

                if (playerBattle == null)
                {
                    playerBattleGO = GameObject.FindWithTag("BattlePlayer");
                    playerBattle = playerBattleGO.GetComponent<Player>();
                }

                if(battleController == null)
                {
                    battleController = GameObject.FindWithTag("BattleController").GetComponent<BattleController>();
                }

            }
        }
    }



    private void InitialiseEnemyLists()
    {
        // cycle through each enemy in the overworld
        for(int i = 0; i < enemiesInHierarchy.transform.childCount; i++)
        {
            // store the gameobjects and the enemy script in lists
            enemiesWorldGO.Add(enemiesInHierarchy.transform.GetChild(i).gameObject);
            enemiesWorld.Add(enemiesWorldGO[i].GetComponent<Enemy>());

            // set up the enemy properties list
            enemiesProperties.Add(new EnemyProperties(enemyID, enemiesWorld[i].GetEnemyType(), enemiesWorld[i].GetMoves(), enemiesWorld[i].GetSprite(), enemiesWorld[i].GetName(), enemiesWorld[i].GetAlive(), enemiesWorld[i].GetLevel(), enemiesWorld[i].GetBaseStrength(), enemiesWorld[i].GetRateStrength(), enemiesWorld[i].GetBaseDexterity(), enemiesWorld[i].GetRateDexterity(), enemiesWorld[i].GetBaseIntelligence(), enemiesWorld[i].GetRateIntelligence()));

            enemiesProperties.ToString();

            enemiesWorld[i].CopyEnemyProperties(enemiesProperties[i]);

            enemyID++;
        }
    }

    
    private void InitialisePlayer()
    {
        playerProperties = new PlayerProperties(playerWorld.GetPlayerType(), playerWorld.GetMoves(), playerWorld.GetSprite(), playerWorld.GetAlive(), playerWorld.GetLevel(), playerWorld.GetBaseStrength(), playerWorld.GetRateStrength(), playerWorld.GetBaseDexterity(), playerWorld.GetRateDexterity(), playerWorld.GetBaseIntelligence(), playerWorld.GetRateIntelligence());
        playerWorld.CopyPlayerProperties(playerProperties);
    }




    public void SetBattleEnemy(GameObject inEnemy)
    {
        // if the enemy world gameobject list contains the enemy to battle
        if (enemiesWorldGO.Contains(inEnemy))
        {
            // cycle through each enemy in the list
            for (int i = 0; i < enemiesWorldGO.Count; i++)
            {
                // find the enemy to battle
                if (enemiesWorldGO[i].Equals(inEnemy))
                {
                    // set the index for when setting properties in battle scene
                    battleEnemyIndex = i;
                    return;
                }
            }
        }
    }

    public void TriggerRandomBattle()
    {
        battleTriggered = true;

        randomBattle = true;

        int randomEnemyIndex = Random.Range(0, randomEnemies.Length);

        int level = playerWorld.GetLevel() - Random.Range(0, 3);

        if (level < 0) level = 0;

        Debug.Log(level);

        randomEnemyPropeties = new EnemyProperties(enemyID, randomEnemies[randomEnemyIndex].GetEnemyType(), randomEnemies[randomEnemyIndex].GetMoves(), randomEnemies[randomEnemyIndex].GetSprite(), randomEnemies[randomEnemyIndex].GetName(), randomEnemies[randomEnemyIndex].GetAlive(), level, randomEnemies[randomEnemyIndex].GetBaseStrength(), randomEnemies[randomEnemyIndex].GetRateStrength(), randomEnemies[randomEnemyIndex].GetBaseDexterity(), randomEnemies[randomEnemyIndex].GetRateDexterity(), randomEnemies[randomEnemyIndex].GetBaseIntelligence(), randomEnemies[randomEnemyIndex].GetRateIntelligence());
        Debug.Log(randomEnemyPropeties.ToString());

        enemyID++;

        gameState = GameState.Battle;
        sceneFader.FadeIntoScene(battleSceneName);
        playerLastPosition = playerGO.transform.position;
    }



    private void UpdateBattleEnemy()
    {
        // copies the properties of the enemy to the battle enemy script
        if (randomBattle) battleEnemy.CopyEnemyProperties(randomEnemyPropeties);
        else battleEnemy.CopyEnemyProperties(enemiesProperties[battleEnemyIndex]);

    }

    private void UpdateBattlePlayer()
    {
        playerBattle.CopyPlayerProperties(playerProperties);
    }




    private void UpdateEnemyPropertiesAfterBattle()
    {
        // this method changes the properties in the persistent enemiesPropertiesList so that
        // changes can be made after a battle has finished and the overworld scene is about to be reloaded
        // this method should run only in the battle scene directly after the battle has finished

        enemiesProperties[battleEnemyIndex].CopyEnemyData(battleEnemy);
    }

    private void UpdatePlayerPropertiesAfterBattle()
    {
        playerProperties.CopyPlayerData(playerBattle);
    }




    private void UpdateWorldEnemiesAfterBattle()
    {
        // this method changes the properties of all enemies to reflect the changes made after
        // the battle.
        // this method should run only at the beginning of the overworld scene, directly after a battle.

        // clear the lists
        enemiesWorldGO.Clear();
        enemiesWorld.Clear();

        // create new references
        for (int i = 0; i < enemiesInHierarchy.transform.childCount; i++)
        {
            // store the gameobjects and the enemy script in lists
            enemiesWorldGO.Add(enemiesInHierarchy.transform.GetChild(i).gameObject);
            enemiesWorld.Add(enemiesWorldGO[i].GetComponent<Enemy>());
        }

        // copy data from enemy properties to enemies in the overworld
        for (int i=0; i < enemiesWorld.Count; i++)
        {
            Debug.Log(enemiesProperties[i].ToString());

            enemiesWorld[i].CopyEnemyProperties(enemiesProperties[i]);
        }
    }

    private void UpdateWorldPlayerAfterBattle()
    {
        playerWorld.CopyPlayerProperties(playerProperties);
    }




    // GETTERS

    public bool GetBattleTriggered()
    {
        return battleTriggered;
    }


}