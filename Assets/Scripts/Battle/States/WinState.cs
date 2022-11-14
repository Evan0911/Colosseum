using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinState : State
{
    public WinState(BattleSystem _system) : base(_system)
    {

    }

    public override IEnumerator Start()
    {
        system.dialogue.text = "You won !";
        yield return new WaitForSeconds(2);

        //afficher le gain d'exp, check si level up
        system.dialogue.text = "You got " + system.levelInfos.expForWinning + " exp. points";
        yield return new WaitForSeconds(2);
        if (system.playerHealth.stats.GainExp(system.levelInfos.expForWinning))
        {
            system.dialogue.text = "Level up !";
            BDD.SaveCreature(system.playerHealth.stats.currentExp, system.playerHealth.stats.lv, system.playerHealth.stats.id);
            yield return new WaitForSeconds(2);
        }
        else
        {
            BDD.SaveCreature(system.playerHealth.stats.currentExp, system.playerHealth.stats.id);
        }

        //Check le drop d'oeuf
        System.Random rnd = new System.Random();
        int dropValue = rnd.Next(1, 101);
        if (dropValue < system.levelInfos.eggDropOdd)
        {
            //On drop l'oeuf
            system.dialogue.text = "You dropped an egg !";
            BDD.SaveNewEgg(system.enemyHealth.stats.id);
            yield return new WaitForSeconds(2);
        }

        //Argent
        system.dialogue.text = "You earned " + system.levelInfos.moneyDrop + " gold !";
        PlayerPrefs.SetInt("Money", PlayerPrefs.GetInt("Money") + system.levelInfos.moneyDrop);
        yield return new WaitForSeconds(2);

        LoadMainMenu();
        yield break;
    }
}
