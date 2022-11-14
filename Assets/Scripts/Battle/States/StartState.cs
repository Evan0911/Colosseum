using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartState : State
{
    public StartState(BattleSystem _system) : base(_system)
    {
    }

    public override IEnumerator Start()
    {
        //R�cup�re la bonne IA pour le niveau
        string level = "Level" + PlayerPrefs.GetInt("NumLevel");
        Debug.Log(level);
        try
        {
            var type = System.Type.GetType(level);
            system.enemyAI = (AI)Activator.CreateInstance(type);
        }
        catch
        {
            system.enemyAI = new AI();
        }

        Debug.Log("L'IA charg� est : " + system.enemyAI.GetType().ToString());

        //Requ�te pour r�cup�rer les 2 cr�atures
        CreatureStats playerStat = BDD.GetAllyCreature(PlayerPrefs.GetInt("CreatureUsed"));
        CreatureStats enemyStat = BDD.GetEnemyCreature(PlayerPrefs.GetInt("NumLevel"));

        //On r�cup�re les stats de chaque cr�atures et on assigne les HP de chaque c�t�
        system.playerHealth = system.player.GetComponent<CreatureHealth>();
        system.playerHealth.stats = playerStat;
        system.playerHealth.SetHealthAndMaxHealth();
        system.enemyHealth = system.enemy.GetComponent<CreatureHealth>();
        system.enemyHealth.stats = enemyStat;
        system.enemyHealth.SetHealthAndMaxHealth();

        //On set les sprites
        Sprite sprite = Resources.Load<Sprite>("Creatures/" + system.playerHealth.stats.typePrinc.ToString() + "/" + system.playerHealth.stats.name + "_" + system.playerHealth.stats.stade);
        system.player.GetComponent<Image>().sprite = sprite;

        sprite = Resources.Load<Sprite>("Creatures/" + system.enemyHealth.stats.typePrinc.ToString() + "/" + system.enemyHealth.stats.name + "_" + system.enemyHealth.stats.stade);
        system.enemy.GetComponent<Image>().sprite = sprite;
        system.enemy.transform.localScale = new Vector3(system.enemy.transform.localScale.x * -1, system.enemy.transform.localScale.y);

        if (system.playerHealth.stats.weakness == system.enemyHealth.stats.typePrinc)
        {
            system.enemyTypeAdvantage = true;
        }
        else if (system.enemyHealth.stats.weakness == system.playerHealth.stats.typePrinc)
        {
            system.allyTypeAdvantage = true;
        }

        //Recup�re les informations du niveau de progression
        system.levelInfos = BDD.GetLevelInfos(PlayerPrefs.GetInt("NumLevel"));
        system.levelInfos.SetExpForWinning(system.enemyHealth.stats.lv, system.playerHealth.stats.lv);

        system.dialogue.enabled = false;
        system.actions.SetActive(true);

        system.SetState(new ChoseActionState(system));

        yield break;
    }
}
