using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveLibrary 
{
    public MoveLibrary()
    {

    }

    public void MoveSelect(ref string outString, Move move, Player player, Enemy enemy, bool playerAttack)
    {
        switch(move)
        {
            case Move.Punch:
                Punch(ref outString, player, enemy, playerAttack);
                break;

            case Move.Mansplain:
                Mansplain(ref outString, player, enemy, playerAttack);
                break;

            case Move.Flex:
                Flex(ref outString, player, enemy, playerAttack);
                break;

            case Move.Squat:
                Squat(ref outString, player, enemy, playerAttack);
                break;

            case Move.Spit:
                Spit(ref outString, player, enemy, playerAttack);
                break;
        }  
    }


    private void Punch(ref string outString, Player player, Enemy enemy, bool playerAttack)
    {
        if(playerAttack == true)
        {
            int damage = player.GetAttack();
            enemy.SubtractHealth(damage);
            outString = "PLAYER DEALT " + damage + " DAMAGE TO " + enemy.GetName().ToUpper() + "!";
        }
        else
        {
            int damage = enemy.GetAttack();
            player.SubtractHealth(damage);
            outString = enemy.GetName().ToUpper() + " DEALT " + damage + " DAMAGE TO PLAYER!";
        }
    }

    private void Mansplain(ref string outString, Player player, Enemy enemy, bool playerAttack)
    {
        if (playerAttack == true)
        {
            outString = "...IT ACHIEVED NOTHING.";
        }
        else
        {
            outString = "...IT ACHIEVED NOTHING.";
        }
    }

    private void Flex(ref string outString, Player player, Enemy enemy, bool playerAttack)
    {

    }

    private void Squat(ref string outString, Player player, Enemy enemy, bool playerAttack)
    {

    }

    private void Spit(ref string outString, Player player, Enemy enemy, bool playerAttack)
    {
        if (playerAttack == true)
        {
            int damage = player.GetAttack();
            enemy.SubtractHealth(damage);
            outString = "PLAYER DEALT " + damage + " DAMAGE TO " + enemy.GetName().ToUpper() + "!";
        }
        else
        {
            int damage = enemy.GetAttack();
            player.SubtractHealth(damage);
            outString = enemy.GetName().ToUpper() + " DEALT " + damage + " DAMAGE TO PLAYER!";
        }
    }

    private void Eat(ref string outString, Player player, Enemy enemy, bool playerAttack)
    {

    }

    private void Flatulence(ref string outString, Player player, Enemy enemy, bool playerAttack)
    {

    }

    private void Feces(ref string outString, Player player, Enemy enemy, bool playerAttack)
    {

    }

}
