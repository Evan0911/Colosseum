using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level5 : AI
{
    private Action previousAction;

    public Level5() : base()
    {

    }

    public override Action SelectNextAction()
    {
        if (previousAction == Action.HEAL)
        {
            previousAction = Action.AOEATTACK;
            return Action.AOEATTACK;
        }
        else
        {
            previousAction = Action.HEAL;
            return Action.HEAL;
        }
    }
}
