using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI
{
    public AI ()
    {
    }

    public virtual Action SelectNextAction()
    {
        System.Random rnd = new System.Random();
        int i = rnd.Next(0, 5);

        if (i % 5 == 0)
        {
            return Action.TARGETATTACK;
        }
        else if (i % 5 == 1)
        {
            return Action.AOEATTACK;
        }
        else if (i % 5 == 2)
        {
            return Action.BLOCK;
        }
        else if (i % 5 == 3)
        {
            return Action.DODGE;
        }
        else
        {
            return Action.HEAL;
        }
    }
}
