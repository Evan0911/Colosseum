using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level3 : AI
{
    private Action previousAction;

    public Level3() : base()
    {

    }

    public override Action SelectNextAction()
    {
        if (previousAction == Action.BLOCK)
        {
            previousAction = Action.TARGETATTACK;
            return Action.TARGETATTACK;
        }
        else
        {
            previousAction = Action.BLOCK;
            return Action.BLOCK;
        }
    }
}
