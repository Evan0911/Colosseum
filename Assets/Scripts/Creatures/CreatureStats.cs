using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Type { FEU, TER, ELE, EAU, VEN, LUM, TEN}

[System.Serializable]
public class CreatureStats
{
    public int id;
    public string name;
    public int lv;
    public int atk;
    public int def;
    public int maxHealth = 100;
    public int currentHealth;
    public int currentExp;
    public int expToLvUp;
    public int stade;
    public Type typePrinc;
    public Type weakness;

    public CreatureStats(int _id, string _name, int _lv, int _atk, int _def, int _health, int _currentExp, int _expToLvUp, int _stade, string type)
    {
        id = _id;
        name = _name;
        lv = _lv;
        atk = _atk * _lv;
        def = _def * _lv;
        maxHealth = _health * _lv;
        currentHealth = maxHealth;
        stade = _stade;
        currentExp = _currentExp;
        expToLvUp = _expToLvUp * lv * stade;
        if (type == Type.FEU.ToString())
        {
            typePrinc = Type.FEU;
            weakness = Type.EAU;
        }

        else if (type == Type.EAU.ToString())
        {
            typePrinc = Type.EAU;
            weakness = Type.TER;
        }

        else if (type == Type.ELE.ToString())
        {
            typePrinc = Type.ELE;
            weakness = Type.VEN;
        }

        else if (type == Type.TER.ToString())
        {
            typePrinc = Type.TER;
            weakness = Type.ELE;
        }

        else if (type == Type.VEN.ToString())
        {
            typePrinc = Type.VEN;
            weakness = Type.FEU;
        }
    }

    public bool GainExp(int exp)
    {
        if (expToLvUp > (exp + currentExp))
        {
            currentExp += exp;
            return false;
        }
        else
        {
            lv++;
            currentExp = exp + currentExp - expToLvUp;
            return true;
        }
    }
}
