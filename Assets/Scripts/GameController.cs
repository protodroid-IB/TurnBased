using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class GameController : MonoBehaviour
{
    private SceneFader sceneFader;

    public static GameState gameState = GameState.Overworld;

    private GameObject enemiesInHierarchy; // gameobject that all enemies in overworld are children of
    private List<GameObject> enemiesWorldGO = new List<GameObject>(); // list of all enemy gameobjects in overworld
    private List<Enemy> enemiesWorld = new List<Enemy>(); // list of all enemy script instances in overworld

    private GameObject battleEnemyGO; // enemy gameobject being battled
    private Enemy battleEnemy; // enemy script instance being battled
    private int battleEnemyIndex = 0;
    private bool justBattled = false;

    private List<EnemyProperties> enemiesProperties = new List<EnemyProperties>(); // updated persistent list of enemy properties



    private void Start()
    {
        // grabs all necessary references
        UpdateReferences();
        InitialiseEnemyLists();
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
        if (sceneFader.GetCurrentSceneName().Equals("Overworld"))
        {
            // if a battle just occured
            if(justBattled == true)
            {
                justBattled = false;

                // updates all references needed for overworld scene
                UpdateReferences();

                // updates the enemy to reflect changes after battle
                UpdateWorldEnemiesAfterBattle();
            }
        }
    }

    private void Battle()
    {
        //
        // STATE CHANGES
        //

        // if in the battle scene
        if (sceneFader.GetCurrentSceneName().Equals("Battle"))
        {
            if(battleEnemy == null)
            {
                // updates all references needed for battle scene
                UpdateReferences();

                // updates the properties of the battle enemy
                UpdateBattleEnemy();
            }


            // if the battle has finished

            // if the enemy is at 0 health
            if (battleEnemy.GetCurrentHealth() <= 0)
            {
                gameState = GameState.TransitionToOverworld;
            }

            // if player health is lower than 0 go back to main menu
            //
            //  INSERT STATE CHANGE HERE!!!!
            //

            battleEnemy.SubtractHealth(2);
        }
    }




    private void TransitionToOverworld()
    {
        justBattled = true;
        UpdateEnemyPropertiesAfterBattle();
        battleEnemy = null;
        battleEnemyIndex = 0;

        gameState = GameState.Overworld;
        sceneFader.FadeIntoScene("Overworld");
    }


    private void TransitionToBattle()
    {
        
        gameState = GameState.Battle;
        sceneFader.FadeIntoScene("Battle");
    }








    // this function updates the references needed in each scene
    private void UpdateReferences()
    {
        // in every scene, grab the scene fader
        if (sceneFader == null)
            sceneFader = GameObject.Find("SceneFader").GetComponent<SceneFader>();

        // only when the game state is in overworld and the scene matches the game state, grab the enemiesInHierarchy gameobject
        if (gameState == GameState.Overworld)
            if (sceneFader.GetCurrentSceneName().Equals("Overworld"))
                if (enemiesInHierarchy == null)
                    enemiesInHierarchy = GameObject.FindWithTag("HierarchyEnemies");

        // only when the game state is in battle and the scene matches the game state, grab the battleEnemy gameobject and script
        if (gameState == GameState.Battle)
            if (sceneFader.GetCurrentSceneName().Equals("Battle"))
                if (battleEnemy == null)
                {
                    battleEnemyGO = GameObject.FindWithTag("BattleEnemy");
                    battleEnemy = battleEnemyGO.GetComponent<Enemy>();
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
            enemiesProperties.Add(new EnemyProperties(i, enemiesWorld[i].GetEnemyType(), enemiesWorld[i].GetAlive(), enemiesWorld[i].GetLevel(), enemiesWorld[i].GetMaxHealth()));
        }
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



    private void UpdateBattleEnemy()
    {
        // copies the properties of the enemy to the battle enemy script
        battleEnemy.CopyEnemyProperties(enemiesProperties[battleEnemyIndex]);
    }




    private void UpdateEnemyPropertiesAfterBattle()
    {
        // this method changes the properties in the persistent enemiesPropertiesList so that
        // changes can be made after a battle has finished and the overworld scene is about to be reloaded
        // this method should run only in the battle scene directly after the battle has finished

        enemiesProperties[battleEnemyIndex].CopyEnemyData(battleEnemyIndex, battleEnemy);
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


}









//public class GameController : MonoBehaviour
//{

    //private SceneFader sceneFader;

    //public static GameState gameState = GameState.Overworld;

    //private GameObject enemiesInHierarchy;
    //private List<GameObject> enemiesGO = new List<GameObject>();
    //private List<EnemyOverworld> enemiesOverworld = new List<EnemyOverworld>();
    //private List<EnemyProperties> enemiesProperties = new List<EnemyProperties>();

    //private BattleEnemyProperties inBattleEnemyProperties;
    //private GameObject battleEnemy;
    //private EnemyProperties battleEnemyProperties;
    //private bool copyDone = false;
    //private bool killBattleEnemy = false;

    //// Use this for initialization
    //void Start ()
    //{
    //    UpdateReferences();
    //    UpdateEnemyLists();
    //}
	


	//// Update is called once per frame
	//void Update ()
 //   {
	//	switch(gameState)
 //       {
 //           case GameState.Overworld:
 //               Overworld();
 //               break;

 //           case GameState.Battle:
 //               Battle();
 //               break;

 //           case GameState.TransitionToBattle:
 //               TransitionToBattle();
 //               break;

 //           case GameState.TransitionToOverworld:
 //               TransitionToOverworld();
 //               break;

 //           default:
 //               gameState = GameState.Overworld;
 //               break;
 //       }
	//}






    //private void Overworld()
    //{
    //    UpdateReferences();

    //    if (sceneFader.GetCurrentSceneName().Equals("Overworld"))
    //    {
    //        if (killBattleEnemy == true)
    //        {
    //            killBattleEnemy = false;
    //            copyDone = false;

    //            UpdateEnemyLists(); 
    //            enemiesOverworld[inBattleEnemyProperties.indexInList].Kill();
    //        }
    //    }
    //}


    //private void TransitionToBattle()
    //{
    //    gameState = GameState.Battle;
    //    sceneFader.FadeIntoScene("Battle");
    //}


    //private void Battle()
    //{
    //    UpdateReferences();

    //    // if in the battle scene
    //    if (sceneFader.GetCurrentSceneName().Equals("Battle"))
    //    {
            
    //        //
    //        // STATE BEHAVIOUR
    //        //

    //        // copy the properties of the enemy to the battle enemy game object
    //        UpdateBattleEnemyProperties();

    //        // enable battle controller!!!!


    //        battleEnemyProperties.SubtractHealth(10f * Time.deltaTime);

    //        //Debug.Log(battleEnemyProperties.GetCurrentHealth());





    //        //
    //        // STATE CHANGES
    //        //

    //        // if the enemies health is below zero, transition back to overworld
    //        if (battleEnemyProperties.GetCurrentHealth() <= 0f)
    //        {
    //            gameState = GameState.TransitionToOverworld;
    //            killBattleEnemy = true;
    //        }

    //        // if the player health is below zero, transition back to main menu


    //    }
    //}




    //private void TransitionToOverworld()
    //{
    //    gameState = GameState.Overworld;
    //    sceneFader.FadeIntoScene("Overworld");
    //}






    //private void UpdateReferences()
    //{
    //    if (sceneFader == null)
    //        sceneFader = GameObject.Find("SceneFader").GetComponent<SceneFader>();

    //    if (gameState == GameState.Overworld)
    //        if (sceneFader.GetCurrentSceneName().Equals("Overworld"))
    //            if (enemiesInHierarchy == null)
    //                enemiesInHierarchy = GameObject.FindWithTag("HierarchyEnemies");

    //    if (gameState == GameState.Battle)
    //        if(sceneFader.GetCurrentSceneName().Equals("Battle"))
    //            if (battleEnemy == null)
    //            {
    //                battleEnemy = GameObject.FindWithTag("BattleEnemy");
    //                battleEnemyProperties = battleEnemy.GetComponent<EnemyProperties>();
    //            }
    //}








    //private void UpdateBattleEnemyProperties()
    //{
    //    if (copyDone == false)
    //    {
    //        copyDone = true;
    //        battleEnemyProperties.SetAlive(inBattleEnemyProperties.alive);
    //        battleEnemyProperties.SetLevel(inBattleEnemyProperties.level);
    //        battleEnemyProperties.SetMaxHealth(inBattleEnemyProperties.maxHealth);
    //        battleEnemyProperties.SetCurrentHealth();
    //    } 
    //}












    //public void SetBattleEnemy(GameObject inBattleEnemey)
    //{
    //    // if the enemies go list contains the enemy to battle
    //    if (enemiesGO.Contains(inBattleEnemey))
    //    {
    //        Debug.Log("IN LIST!");
    //        // cycle through each enemy in the list
    //        for (int i = 0; i < enemiesGO.Count; i++)
    //        {
    //            // find the enemy to battle
    //            if (enemiesGO[i].Equals(inBattleEnemey))
    //            {
    //                Debug.Log("FOUND!");
    //                inBattleEnemyProperties = new BattleEnemyProperties(i, enemiesProperties[i].GetAlive(), enemiesProperties[i].GetLevel(), enemiesProperties[i].GetMaxHealth());

    //                // exit the loop
    //                return;
    //            }
    //        }
    //    }
    //}



    //private void UpdateEnemyLists()
    //{
    //    Debug.Log(enemiesInHierarchy.transform.childCount);

    //    enemiesGO.Clear();
    //    enemiesOverworld.Clear();
    //    enemiesProperties.Clear();

    //    for (int i = 0; i < enemiesInHierarchy.transform.childCount; i++)
    //    {
    //        enemiesGO.Add(enemiesInHierarchy.transform.GetChild(i).gameObject);
    //        enemiesOverworld.Add(enemiesGO[i].transform.GetChild(0).GetComponent<EnemyOverworld>());
    //        enemiesProperties.Add(enemiesGO[i].GetComponent<EnemyProperties>());
    //    }
    //}


    ////public void RemoveEnemyFromList(GameObject inEnemyGO)
    ////{
    ////    enemiesGO.Remove(inEnemyGO);
    ////    enemiesOverworld.Remove(inEnemyGO.transform.GetChild(0).GetComponent<EnemyOverworld>());
    ////    enemiesProperties.Remove(inEnemyGO.GetComponent<EnemyProperties>());

    ////    inEnemyGO.SetActive(false);

    ////    PrintEnemiesGOList();

    ////    UpdateEnemyLists();
    ////}



    ////public void RemoveEnemyFromList(int index)
    ////{
    ////    enemiesGO.RemoveAt(index);
    ////    enemiesOverworld.RemoveAt(index);
    ////    enemiesProperties.RemoveAt(index);

    ////    UpdateEnemyLists();
    ////}


    //private void PrintEnemiesGOList()
    //{
    //    foreach(GameObject go in enemiesGO)
    //    {
    //        Debug.Log(go.name);
    //    }
    //}







    //private class BattleEnemyProperties
    //{
    //    public int indexInList;

    //    public bool alive;

    //    public int level;

    //    public float maxHealth;

    //    public float currentHealth;

    //    public BattleEnemyProperties(int inIndex, bool inAlive, int inLevel, float inMaxHealth)
    //    {
    //        indexInList = inIndex;
    //        alive = inAlive;
    //        level = inLevel;
    //        maxHealth = inMaxHealth;
    //        currentHealth = maxHealth;
    //    }
    //}
