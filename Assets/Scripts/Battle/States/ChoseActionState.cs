using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChoseActionState : State
{
    public ChoseActionState(BattleSystem _system):base(_system)
    {

    }

    public override IEnumerator Start()
    {
        system.enemyAction = system.enemyAI.SelectNextAction();
        yield break;
    }

    public override void SetPlayerAttack(Action action)
    {
        system.playerAction = action;
        system.dialogue.enabled = true;
        system.actions.SetActive(false);
        system.SetState(new BattleState(system));
        system.LaunchBattle();
    }
}
