using System.Collections;
using System.Collections.Generic;

public class EnemyProperties
{
    // MEMBER FIELDS
    private int indexInList;

    private EnemyType enemyType;

    private bool alive;

    private int level;

    private int maxHealth;

    private int currentHealth;


    // CONSTRUCTORS
    public EnemyProperties(int inIndex, EnemyType inType, bool inAlive, int inLevel, int inMaxHealth)
    {
        indexInList = inIndex;
        enemyType = inType;
        alive = inAlive;
        level = inLevel;
        maxHealth = inMaxHealth;
        currentHealth = maxHealth;
    }


    public EnemyProperties(EnemyProperties inEnemy)
    {
        indexInList = inEnemy.GetIndexInList();
        enemyType = inEnemy.GetEnemyType();
        alive = inEnemy.GetAlive();
        level = inEnemy.GetLevel();
        maxHealth = inEnemy.GetMaxHealth();
        currentHealth = inEnemy.GetCurrentHealth();
    }


    // GETTERS
    public int GetIndexInList()
    {
        return indexInList;
    }

    public EnemyType GetEnemyType()
    {
        return enemyType;
    }

    public bool GetAlive()
    {
        return alive;
    }

    public int GetLevel()
    {
        return level;
    }

    public int GetMaxHealth()
    {
        return maxHealth;
    }

    public int GetCurrentHealth()
    {
        return currentHealth;
    }




    // SETTERS
    public void SetIndexInList(int inNum)
    {
        indexInList = inNum;
    }

    public void SetEnemyType(EnemyType inType)
    {
        enemyType = inType;
    }

    public void SetAlive(bool inAlive)
    {
        alive = inAlive;
    }

    public void SetLevel(int inNum)
    {
        level = inNum;
    }

    public void SetMaxHealth(int inNum)
    {
        maxHealth = inNum;
    }

    public void SetCurrentHealth(int inNum)
    {
        currentHealth = inNum;
    }




    // EQUALS
    public bool Equals(EnemyProperties inEnemy)
    {
        bool isEqual = false;

        if(inEnemy != null)
        {
            if (indexInList == inEnemy.GetIndexInList())
            {
                if (enemyType == inEnemy.GetEnemyType())
                {
                    if (level == inEnemy.GetLevel())
                    {
                        if (alive == inEnemy.GetAlive())
                        {
                            if (maxHealth == inEnemy.GetMaxHealth())
                            {
                                if (currentHealth == inEnemy.GetCurrentHealth())
                                {
                                    isEqual = true;
                                }
                            }
                        }
                    }
                }
            }
        }

        return isEqual;
    }





    // TO STRING
    override
    public string ToString()
    {
        string str = "";

        str = "INDEX: " + indexInList + "  ENEMY TYPE: " + enemyType + "  ALIVE: " + alive + "  LEVEL: " + level;
        str += "  HEALTH: " + currentHealth + " / " + maxHealth;
        str += "\n";

        return str;
    }



    public void CopyEnemyData(int index, Enemy inEnemy)
    {
        indexInList = index;
        enemyType = inEnemy.GetEnemyType();
        alive = inEnemy.GetAlive();
        level = inEnemy.GetLevel();
        maxHealth = inEnemy.GetMaxHealth();
        currentHealth = inEnemy.GetCurrentHealth();
    }

}
