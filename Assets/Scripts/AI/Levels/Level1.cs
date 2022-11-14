using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level1 : AI
{
    public Level1 ():base()
    {

    }

    public override Action SelectNextAction()
    {
        return Action.TARGETATTACK;
    }
}
