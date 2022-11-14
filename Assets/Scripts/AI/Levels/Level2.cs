using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level2 : AI
{
    public Level2() : base()
    {

    }

    public override Action SelectNextAction()
    {
        return Action.AOEATTACK;
    }
}
