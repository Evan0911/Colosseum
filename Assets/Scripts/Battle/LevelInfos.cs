using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelInfos
{
    public int numLevel;
    public int baseExpEnemy;
    public int expForWinning;
    public int enemyStade;
    public int eggDropOdd;
    public int enemyCreatureId;
    public int moneyDrop;

    public LevelInfos (int _numLevel, int _baseExpEnemy, int _enemyStade, int _eggDropOdd, int _moneyDrop)
    {
        numLevel = _numLevel;
        baseExpEnemy = _baseExpEnemy;
        enemyStade = _enemyStade;
        eggDropOdd = _eggDropOdd;
        moneyDrop = _moneyDrop;
    }

    public void SetExpForWinning (int enemyLevel, int allyLevel)
    {
        expForWinning = baseExpEnemy * ((enemyLevel / 5) * (enemyLevel / allyLevel)) * enemyStade;
    }
}
