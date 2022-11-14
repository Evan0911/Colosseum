using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level4 : AI
{
    public Level4() : base()
    {

    }

    public override Action SelectNextAction()
    {
        return Action.DODGE;
    }
}
