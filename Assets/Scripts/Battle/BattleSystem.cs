using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

//On énumère des états que le script va pouvoir prendre, un seul peut être actif
public enum Action { TARGETATTACK, AOEATTACK, BLOCK, DODGE, HEAL, Null }

public class BattleSystem : MonoBehaviour
{
    public Action playerAction;
    public Action enemyAction;

    public GameObject player;
    public CreatureHealth playerHealth;

    public GameObject enemy;
    public CreatureHealth enemyHealth;
    public AI enemyAI;

    public Text dialogue;
    public GameObject actions;

    public bool allyTypeAdvantage = false;
    public bool enemyTypeAdvantage = false;

    public LevelInfos levelInfos;

    State currentState;

    void Start()
    {
        SetState(new StartState(this));
    }

    public void SetState(State state)
    {
        currentState = state;
        StartCoroutine(currentState.Start());
        Debug.Log("state is " + currentState.GetType().ToString());
    }

    public void LaunchBattle()
    {
        StartCoroutine(currentState.TurnBegin());
    }


    #region Button
    //Chaque boutons fait sensiblement la même chose, dire qu'on a appuyer sur le bouton et envoyer l'attaque que l'on souhaite faire pour continuer le combat
    public void OnTargetAttackButton()
    {
        currentState.SetPlayerAttack(Action.TARGETATTACK);
    }

    public void OnAOEAttackButton()
    {
        currentState.SetPlayerAttack(Action.AOEATTACK);
    }

    public void OnBlockButton()
    {
        currentState.SetPlayerAttack(Action.BLOCK);
    }

    public void OnDodgeButton()
    {
        currentState.SetPlayerAttack(Action.DODGE);
    }

    public void OnHealButton()
    {
        currentState.SetPlayerAttack(Action.HEAL);
    }
    #endregion

}
