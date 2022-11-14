using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemManager : MonoBehaviour
{
    Item item;
    public Text txt;

    public void SetItem(Item _item)
    {
        item = _item;
        txt.text = item.name;
    }
}
