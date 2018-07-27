using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProperties
{
    // MEMBER FIELDS
    private int id;

    private Sprite sprite;

    private FighterType enemyType;

    private Moves moves;

    private string name;

    private bool alive;

    private int level;

    private int maxHealth;

    private int currentHealth;

    private int strength, dexterity, intelligence, totalPoints, speed, attack, defense, accuracy, cooldown;

    private int baseStrength, baseDexterity, baseIntelligence;

    private float rateStrength, rateDexterity, rateIntelligence;

    private int experienceNeeded, experienceGained;

    private bool levelUp;


    // CONSTRUCTORS
    public EnemyProperties(int inID, FighterType inType, Moves inMoves, Sprite inSprite, string inName, bool inAlive, int inLevel, int inStrength, float inRateStrength, int inDexterity, float inRateDexterity, int inIntelligence, float inRateIntelligence)
    {
        id = inID;
        enemyType = inType;
        moves = inMoves;
        sprite = inSprite;
        name = inName;

        alive = inAlive;
        level = inLevel;
        levelUp = false;

        baseStrength = inStrength;
        rateStrength = inRateStrength;
        strength = MainStatCalc(baseStrength, rateStrength);

        baseDexterity = inDexterity;
        rateDexterity = inRateDexterity;    
        dexterity = MainStatCalc(baseDexterity, rateDexterity);

        baseIntelligence = inIntelligence;
        rateIntelligence = inRateIntelligence;
        intelligence = MainStatCalc(baseIntelligence, rateIntelligence);

        totalPoints = TotalPointsCalc();

        speed = SpeedCalc();
        attack = AttackCalc();
        defense = DefenseCalc();
        accuracy = AccuracyCalc();
        cooldown = CooldownCalc();

        maxHealth = HealthCalc();
        currentHealth = maxHealth;

        experienceNeeded = ExperienceNeededCalc();
        experienceGained = 0;
    }


    public EnemyProperties(EnemyProperties inEnemy)
    {
        id = inEnemy.GetID();
        enemyType = inEnemy.GetEnemyType();
        moves = inEnemy.GetMoves();

        sprite = inEnemy.GetSprite();
        name = inEnemy.GetName();

        alive = inEnemy.GetAlive();
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
    }


    // GETTERS
    public int GetID()
    {
        return id;
    }

    public FighterType GetEnemyType()
    {
        return enemyType;
    }

    public Moves GetMoves()
    {
        return moves;
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

    public int GetLevel()
    {
        return level;
    }

    public bool GetLevelUp()
    {
        return levelUp;
    }

    public int GetMaxHealth()
    {
        return maxHealth;
    }

    public int GetCurrentHealth()
    {
        return currentHealth;
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




    // SETTERS
    public void SetID(int inNum)
    {
        id = inNum;
    }

    public void SetEnemyType(FighterType inType)
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




    // EQUALS
    public bool Equals(EnemyProperties inEnemy)
    {
        bool isEqual = false;

        if(inEnemy != null)
        {
            if (id == inEnemy.GetID())
            {
                isEqual = true;
            }
        }

        return isEqual;
    }





    // TO STRING
    override
    public string ToString()
    {
        string str = "";

        str = "ID: " + id + "  ENEMY TYPE: " + enemyType + "  ALIVE: " + alive + "  LEVEL: " + level;
        str += "  HEALTH: " + currentHealth + " / " + maxHealth;
        str += "  STR: " + strength + "  DEX: " + dexterity + " INT: " + intelligence + "  SPEED: " + speed;
        str += "\n";

        return str;
    }






    // CALCULATIONS


    private int MainStatCalc(int baseStat, float rate)
    {
        float outStat = (float)baseStat + rate * (float)level;

        return (int)outStat;
    }


    private int SpeedCalc()
    {
        float outSpeed = ((float)dexterity + (float)intelligence) / 2f + (float)level * ((((float)dexterity + (float)intelligence) / 2f) / (float)totalPoints);

        return (int)outSpeed;
    }

    private int AttackCalc()
    {
        float outAttack = strength + (float)level * (strength / (float)totalPoints);

        return (int)outAttack;
    }

    private int DefenseCalc()
    {
        float outDefense = intelligence + (float)level * (intelligence / (float)totalPoints);

        return (int)outDefense;
    }

    private int AccuracyCalc()
    {
        float outAccuracy = dexterity + (float)level * (dexterity / (float)totalPoints);

        return (int)outAccuracy;
    }

    private int CooldownCalc()
    {
        float outCooldown = ((float)strength + (float)intelligence) / 2f + (float)level * ((((float)strength + (float)intelligence) / 2f) / (float)totalPoints);

        return (int)outCooldown;
    }

    private int HealthCalc()
    {
        int outHealth = speed + attack + defense + accuracy + cooldown;

        return outHealth;
    }

    private int TotalPointsCalc()
    {
        int outTotalPoints = strength + dexterity + intelligence;

        return outTotalPoints;
    }

    private int ExperienceNeededCalc()
    {
        int outExperienceNeeded = totalPoints + level;

        return outExperienceNeeded;
    }



















    // HELPER FUNCTIONS
    public void CopyEnemyData(Enemy inEnemy)
    {
        id = inEnemy.GetID();
        enemyType = inEnemy.GetEnemyType();
        moves = inEnemy.GetMoves();
        alive = inEnemy.GetAlive();
        sprite = inEnemy.GetSprite();
        name = inEnemy.GetName();

        if(inEnemy.GetLevelUp() == false)
        {
            level = inEnemy.GetLevel();
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
        }
        else
        {
            LevelUp();
            levelUp = false;
        }
        
    }


    public void LevelUp()
    {
        level++;
        
        strength = MainStatCalc(baseStrength, rateStrength);
        dexterity = MainStatCalc(baseDexterity, rateDexterity);
        intelligence = MainStatCalc(baseIntelligence, rateIntelligence);

        totalPoints = strength + dexterity + intelligence;

        speed = SpeedCalc();
        attack = AttackCalc();
        defense = DefenseCalc();
        accuracy = AccuracyCalc();
        cooldown = CooldownCalc();

        maxHealth = HealthCalc();
        currentHealth = maxHealth;

        experienceGained = (experienceGained - experienceNeeded);
        experienceNeeded = ExperienceNeededCalc();
    }


    public void GainExperience(int inExperience)
    {
        experienceGained += inExperience;
    }

}
