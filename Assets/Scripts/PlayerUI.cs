using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour
{
    private Player player;

    private GameObject playerStatsGO;

    private Text[] stats;

    private bool updatedUI;

    private GameObject moveSelectionGO;

    private Text[] moves;

    // Use this for initialization
    void Start ()
    {
        updatedUI = false;

        player = GetComponent<Player>();
        playerStatsGO = GameObject.FindWithTag("PlayerStats").gameObject;

        moveSelectionGO = GameObject.FindWithTag("MoveSelection").gameObject;

        stats = new Text[playerStatsGO.transform.childCount];

        moves = new Text[4];

        for(int i = 0; i < stats.Length; i++)
        {
            if (i <= 2) stats[i] = playerStatsGO.transform.GetChild(i).GetComponent<Text>();
            else stats[i] = playerStatsGO.transform.GetChild(i).GetChild(0).GetComponent<Text>();
        }

        for(int i=0; i < moves.Length; i++)
        {
            moves[i] = moveSelectionGO.transform.GetChild(i).GetChild(0).GetComponent<Text>();
        }

        
    }
	
	// Update is called once per frame
	void Update ()
    {
        if(player.GetUpdated() == true && updatedUI == false)
        {
            updatedUI = true;
            stats[1].text = player.GetPlayerType().ToString().ToUpper();
            stats[2].text = "LEVEL " + player.GetLevel().ToString();
            stats[4].text = player.GetStrength().ToString();
            stats[5].text = player.GetDexterity().ToString();
            stats[6].text = player.GetIntelligence().ToString();
            stats[7].text = player.GetSpeed().ToString();
            stats[8].text = player.GetAttack().ToString();
            stats[9].text = player.GetDefense().ToString();
            stats[10].text = player.GetAccuracy().ToString();
            stats[11].text = player.GetExperienceGained() + " / " + player.GetExperienceNeeded();

            moves[0].text = player.GetMoves().move01.ToString().ToUpper();
            moves[1].text = player.GetMoves().move02.ToString().ToUpper();
            moves[2].text = player.GetMoves().move03.ToString().ToUpper();
            moves[3].text = player.GetMoves().move04.ToString().ToUpper();

            moveSelectionGO.SetActive(false);
        }


        stats[3].text = player.GetCurrentHealth() + " / " + player.GetMaxHealth();
    }
}
