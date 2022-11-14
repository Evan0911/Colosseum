using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoseState : State
{
    public LoseState(BattleSystem _system) : base(_system)
    {

    }

    public override IEnumerator Start()
    {
        system.dialogue.text = "You lose !";
        yield return new WaitForSeconds(2);
        LoadMainMenu();
        yield break;
    }
}
