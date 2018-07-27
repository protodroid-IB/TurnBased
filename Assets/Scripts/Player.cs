using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    private Sprite sprite;

    private bool alive = true;

    [SerializeField]
    private FighterType playerType;

    [SerializeField]
    private Moves moves;

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
        if (currentHealth <= 0)
        {
            currentHealth = 0;
            alive = false;
        }
    }




    // GETTERS & SETTERS

    public Sprite GetSprite()
    {
        return sprite;
    }


    public bool GetAlive()
    {
        return alive;
    }

    public void SetAlive(bool inBool)
    {
        alive = inBool;
    }



    public FighterType GetPlayerType()
    {
        return playerType;
    }

    public void SetPlayerType(FighterType inPlayerType)
    {
        playerType = inPlayerType;
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








    public void CopyPlayerProperties(PlayerProperties inPlayer)
    {
        sprite = inPlayer.GetSprite();

        alive = inPlayer.GetAlive();
        playerType = inPlayer.GetPlayerType();
        moves = inPlayer.GetMoves();

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
