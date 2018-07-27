using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyUI : MonoBehaviour
{

    private Enemy enemy;

    private GameObject enemyStatsGO;

    private Text[] stats;

    private bool updatedUI;

    // Use this for initialization
    void Start()
    {
        updatedUI = false;

        enemy = GetComponent<Enemy>();
        enemyStatsGO = GameObject.FindWithTag("EnemyStats").gameObject;

        stats = new Text[enemyStatsGO.transform.childCount];

        for (int i = 0; i < stats.Length; i++)
        {
            if (i <= 2) stats[i] = enemyStatsGO.transform.GetChild(i).GetComponent<Text>();
            else stats[i] = enemyStatsGO.transform.GetChild(i).GetChild(0).GetComponent<Text>();
        }


    }

    // Update is called once per frame
    void Update()
    {
        if (enemy.GetUpdated() == true && updatedUI == false)
        {
            updatedUI = true;
            stats[0].text = enemy.GetName().ToUpper();
            stats[1].text = enemy.GetEnemyType().ToString().ToUpper();
            stats[2].text = "LEVEL " + enemy.GetLevel().ToString();
            stats[4].text = enemy.GetStrength().ToString();
            stats[5].text = enemy.GetDexterity().ToString();
            stats[6].text = enemy.GetIntelligence().ToString();
            stats[7].text = enemy.GetSpeed().ToString();
            stats[8].text = enemy.GetAttack().ToString();
            stats[9].text = enemy.GetDefense().ToString();
            stats[10].text = enemy.GetAccuracy().ToString();
            stats[11].text = enemy.GetExperienceGained() + " / " + enemy.GetExperienceNeeded();
        }


        stats[3].text = enemy.GetCurrentHealth() + " / " + enemy.GetMaxHealth();
    }
}
