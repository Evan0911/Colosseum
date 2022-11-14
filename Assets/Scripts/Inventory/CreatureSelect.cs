using UnityEngine;
using UnityEngine.UI;

public class CreatureSelect : MonoBehaviour
{
    public int idCrea;
    public CreatureStats creature;
    public Image spriteCrea;

    public void SetSprite(int id, CreatureStats laCreature)
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
    }

    public void OpenStatButton()
    {
        TeamManager.instance.OpenStat(idCrea);
    }

    public void OpenHatchMenu()
    {
        TeamManager.instance.OpenHatchMenu(idCrea);
    }
}
