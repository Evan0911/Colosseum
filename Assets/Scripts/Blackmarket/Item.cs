using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item
{
    public int id;
    public string name;
    public int prix;

    public Item(int _id, string _name, int _prix)
    {
        id = _id;
        name = _name;
        prix = _prix;
    }
}
