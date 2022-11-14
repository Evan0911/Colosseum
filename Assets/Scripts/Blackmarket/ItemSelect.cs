using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemSelect : MonoBehaviour
{
    public int idItem;
    public Item item;
    public Text itemNameText;

    /*public void SetSprite(int id, CreatureStats laCreature)
    {
        idCrea = id;
        creature = laCreature;
        Sprite sprite;
        if (creature.stade == 0)
        {
            sprite = Resources.Load<Sprite>("Creatures/Egg");
            creature.name += " Egg";
        }
        else
        {
            sprite = Resources.Load<Sprite>("Creatures/" + creature.typePrinc + "/" + creature.name + "_" + creature.stade);
        }
        spriteCrea.sprite = sprite;
    }*/

    public void SetText(int _id, Item _item)
    {
        idItem = _id;
        item = _item;
        itemNameText.text = item.name;
    }

    public void BuyEggButton()
    {
        ShopManager.instance.BuyEgg(idItem);
    }

    public void BuyRuneButton()
    {
        ShopManager.instance.BuyRune(idItem);
    }
}
