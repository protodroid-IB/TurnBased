using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerProperties
{
    // MEMBER FIELDS
    private Sprite sprite;

    private FighterType playerType;

    private Moves moves;

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
    public PlayerProperties(FighterType inType, Moves inMoves, Sprite inSprite, bool inAlive, int inLevel, int inStrength, float inRateStrength, int inDexterity, float inRateDexterity, int inIntelligence, float inRateIntelligence)
    {
        playerType = inType;
        moves = inMoves;
        sprite = inSprite;

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


    public PlayerProperties(PlayerProperties inPlayer)
    {
        playerType = inPlayer.GetPlayerType();
        moves = inPlayer.GetMoves();
        sprite = inPlayer.GetSprite();

        alive = inPlayer.GetAlive();
        level = inPlayer.GetLevel();
        levelUp = inPlayer.GetLevelUp();

        baseStrength = inPlayer.GetBaseStrength();
        rateStrength = inPlayer.GetRateStrength();
        strength = inPlayer.GetStrength();

        baseDexterity = inPlayer.GetBaseDexterity();
        rateDexterity = inPlayer.GetRateDexterity();
        dexterity = inPlayer.GetDexterity();

        baseIntelligence = inPlayer.GetBaseIntelligence();
        rateIntelligence = inPlayer.GetRateIntelligence();
        intelligence = inPlayer.GetIntelligence();

        totalPoints = inPlayer.GetTotalPoints();

        speed = inPlayer.GetSpeed();
        attack = inPlayer.GetAttack();
        defense = inPlayer.GetDefense();
        accuracy = inPlayer.GetAccuracy();
        cooldown = inPlayer.GetCooldown();

        maxHealth = inPlayer.GetMaxHealth();
        currentHealth = inPlayer.GetCurrentHealth();

        experienceNeeded = inPlayer.GetExperienceNeeded();
        experienceGained = inPlayer.GetExperienceGained();
    }


    // GETTERS

    public FighterType GetPlayerType()
    {
        return playerType;
    }

    public Moves GetMoves()
    {
        return moves;
    }

    public Sprite GetSprite()
    {
        return sprite;
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
    public void SetPlayerType(FighterType inType)
    {
        playerType = inType;
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
    //public bool Equals(EnemyProperties inEnemy)
    //{
    //    bool isEqual = false;

    //    if (inEnemy != null)
    //    {
    //        if (id == inEnemy.GetID())
    //        {
    //            isEqual = true;
    //        }
    //    }

    //    return isEqual;
    //}





    // TO STRING
    override
    public string ToString()
    {
        string str = "";

        str = "TYPE: " + playerType + "  ALIVE: " + alive + "  LEVEL: " + level;
        str += "  HEALTH: " + currentHealth + " / " + maxHealth;
        str += "  STRENGTH: " + strength + "  DEXTERITY: " + dexterity + "  INTELLIGENCE: " + intelligence;
        str += "  SPEED: " + speed + "  ACCURACY: " + accuracy + "  ATTACK: " + attack + "  DEFENSE: " + defense;
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
    public void CopyPlayerData(Player inPlayer)
    {
        playerType = inPlayer.GetPlayerType();
        moves = inPlayer.GetMoves();
        alive = inPlayer.GetAlive();
        sprite = inPlayer.GetSprite();

        if (inPlayer.GetLevelUp() == false)
        {
            level = inPlayer.GetLevel();
            baseStrength = inPlayer.GetBaseStrength();
            rateStrength = inPlayer.GetRateStrength();
            strength = inPlayer.GetStrength();

            baseDexterity = inPlayer.GetBaseDexterity();
            rateDexterity = inPlayer.GetRateDexterity();
            dexterity = inPlayer.GetDexterity();

            baseIntelligence = inPlayer.GetBaseIntelligence();
            rateIntelligence = inPlayer.GetRateIntelligence();
            intelligence = inPlayer.GetIntelligence();

            totalPoints = inPlayer.GetTotalPoints();

            speed = inPlayer.GetSpeed();
            attack = inPlayer.GetAttack();
            defense = inPlayer.GetDefense();
            accuracy = inPlayer.GetAccuracy();
            cooldown = inPlayer.GetCooldown();

            maxHealth = inPlayer.GetMaxHealth();
            currentHealth = inPlayer.GetCurrentHealth();

            experienceNeeded = inPlayer.GetExperienceNeeded();
            experienceGained = inPlayer.GetExperienceGained();
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

        experienceGained = 0;
        experienceNeeded = ExperienceNeededCalc();
    }


    public void GainExperience(int inExperience)
    {
        experienceGained += inExperience;
    }

}
