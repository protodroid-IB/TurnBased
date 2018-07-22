using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private bool alive = true;

    [SerializeField]
    private EnemyType enemyType;

    [SerializeField]
    private int level = 1;

    [SerializeField]
    private int maxHealth = 100;

    private int currentHealth;





    private void Start()
    {
        currentHealth = maxHealth;
    }


    private void Update()
    {
        if(currentHealth <= 0)
        {
            currentHealth = 0;
            alive = false;
        }
    } 




    // GETTERS & SETTERS

    public bool GetAlive()
    {
        return alive;
    }

    public void SetAlive(bool inBool)
    {
        alive = inBool;
    }



    public EnemyType GetEnemyType()
    {
        return enemyType;
    }

    public void SetEnemyType(EnemyType inEnemyType)
    {
        enemyType = inEnemyType;
    }





    public int GetLevel()
    {
        return level;
    }

    public void SetLevel(int inLevel)
    {
        level = inLevel;
    }




    public int GetMaxHealth()
    {
        return maxHealth;
    }

    public void SetMaxHealth(int inHealth)
    {
        maxHealth = inHealth;
    }




    public int GetCurrentHealth()
    {
        return currentHealth;
    }

    public void SetCurrentHealth()
    {
        currentHealth = maxHealth;
    }

    public void SetCurrentHealth(int inHealth)
    {
        currentHealth = inHealth;
    }

    public void SubtractHealth(int inNum)
    {
        currentHealth -= inNum;
    }

    public void AddHealth(int inNum)
    {
        currentHealth += inNum;
    }







    public void CopyEnemyProperties(EnemyProperties enemyProps)
    {
        SetAlive(enemyProps.GetAlive());
        SetEnemyType(enemyProps.GetEnemyType());
        SetLevel(enemyProps.GetLevel());
        SetMaxHealth(enemyProps.GetMaxHealth());
        SetCurrentHealth(enemyProps.GetCurrentHealth());
    }
}
