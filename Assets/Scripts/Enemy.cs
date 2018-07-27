using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private int id;

    [SerializeField]
    private Sprite sprite;

    private bool alive = true;

    [SerializeField]
    private FighterType enemyType;

    [SerializeField]
    private Moves moves;

    [SerializeField]
    private string name;

    [SerializeField]
    private int level = 1;

    private int maxHealth = 100;

    private int currentHealth;

    [SerializeField]
    private int baseStrength, baseDexterity, baseIntelligence;

    [SerializeField]
    private float rateStrength, rateDexterity, rateIntelligence;

    private int strength, dexterity, intelligence, totalPoints, speed, attack, defense, accuracy, cooldown;

    private int experienceNeeded, experienceGained;

    private bool levelUp = false;

    private bool updated;


    private void Start()
    {
        updated = false;
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

    public int GetID()
    {
        return id;
    }

    public Sprite GetSprite()
    {
        return sprite;
    }

    public string GetName()
    {
        return name;
    }


    public bool GetAlive()
    {
        return alive;
    }

    public void SetAlive(bool inBool)
    {
        alive = inBool;
    }



    public FighterType GetEnemyType()
    {
        return enemyType;
    }

    public void SetEnemyType(FighterType inEnemyType)
    {
        enemyType = inEnemyType;
    }



    public Moves GetMoves()
    {
        return moves;
    }



    public int GetLevel()
    {
        return level;
    }

    public void SetLevel(int inLevel)
    {
        level = inLevel;
    }



    public int GetBaseStrength()
    {
        return baseStrength;
    }

    public int GetBaseDexterity()
    {
        return baseDexterity;
    }

    public int GetBaseIntelligence()
    {
        return baseIntelligence;
    }

    public float GetRateStrength()
    {
        return rateStrength;
    }

    public float GetRateDexterity()
    {
        return rateDexterity;
    }

    public float GetRateIntelligence()
    {
        return rateIntelligence;
    }

    public int GetStrength()
    {
        return strength;
    }

    public int GetDexterity()
    {
        return dexterity;
    }

    public int GetIntelligence()
    {
        return intelligence;
    }

    public int GetTotalPoints()
    {
        return totalPoints;
    }

    public int GetSpeed()
    {
        return speed;
    }

    public int GetAttack()
    {
        return attack;
    }

    public int GetDefense()
    {
        return defense;
    }

    public int GetAccuracy()
    {
        return accuracy;
    }

    public int GetCooldown()
    {
        return cooldown;
    }

    public int GetExperienceNeeded()
    {
        return experienceNeeded;
    }

    public int GetExperienceGained()
    {
        return experienceGained;
    }

    public bool GetLevelUp()
    {
        return levelUp;
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


    





    public void CopyEnemyProperties(EnemyProperties inEnemy)
    {
        id = inEnemy.GetID();
        sprite = inEnemy.GetSprite();
        name = inEnemy.GetName();

        alive = inEnemy.GetAlive();
        enemyType = inEnemy.GetEnemyType();
        moves = inEnemy.GetMoves();

        level = inEnemy.GetLevel();
        levelUp = inEnemy.GetLevelUp();

        baseStrength = inEnemy.GetBaseStrength();
        rateStrength = inEnemy.GetRateStrength();
        strength = inEnemy.GetStrength();

        baseDexterity = inEnemy.GetBaseDexterity();
        rateDexterity = inEnemy.GetRateDexterity();
        dexterity = inEnemy.GetDexterity();

        baseIntelligence = inEnemy.GetBaseIntelligence();
        rateIntelligence = inEnemy.GetRateIntelligence();
        intelligence = inEnemy.GetIntelligence();

        totalPoints = inEnemy.GetTotalPoints();

        speed = inEnemy.GetSpeed();
        attack = inEnemy.GetAttack();
        defense = inEnemy.GetDefense();
        accuracy = inEnemy.GetAccuracy();
        cooldown = inEnemy.GetCooldown();

        maxHealth = inEnemy.GetMaxHealth();
        currentHealth = inEnemy.GetCurrentHealth();

        experienceNeeded = inEnemy.GetExperienceNeeded();
        experienceGained = inEnemy.GetExperienceGained();

        updated = true;
    }




    // HELPER FUNCTIONS

    public void SubtractHealth(int inNum)
    {
        currentHealth -= inNum;
    }

    public void AddHealth(int inNum)
    {
        currentHealth += inNum;
    }

    public void GainExperience(int inExperience)
    {
        experienceGained += inExperience;
    }

    public bool CheckLevelUp()
    {
        if (experienceGained >= experienceNeeded)
        {
            levelUp = true;
            return true;
        }

        return false;
    }

    public bool GetUpdated()
    {
        return updated;
    }
}
