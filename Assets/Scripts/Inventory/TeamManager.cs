using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TeamManager : MonoBehaviour
{
    public int selectedCreature;

    public List<CreatureStats> creatures;
    public List<CreatureStats> eggs;

    #region Creatures and Eggs
    public GameObject listCreaUI;
    public GameObject listEggUI;
    public GameObject prefabCreature;
    public GameObject prefabEgg;
    public GameObject statWindow;
    public GameObject btnChoice;
    public GameObject selectCreatureMenu;
    public GameObject eggsManagerMenu;

    public GameObject hatchMenu;
    public Text hatchText;
    public GameObject txtCantHatch;
    public GameObject btnHatch;
    #endregion

    #region Items
    public GameObject itemManagerPanel;
    public GameObject itemManagerButtons;
    public GameObject runesPanel;
    public GameObject runesContainerList;

    public List<Item> cRunes;

    public GameObject prefabItem;
    #endregion

    public static TeamManager instance;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    void Start()
    {
        SetCreatures();
        SetEggs();
        SetRunes();
    }

    public void OpenSelectCreatureMenu()
    {
        selectCreatureMenu.SetActive(true);
    }

    public void CloseSelectCreatureMenu()
    {
        selectCreatureMenu.SetActive(false);
    }

    public void OpenEggManagerMenu()
    {
        eggsManagerMenu.SetActive(true);
    }

    public void CloseEggManagerMenu()
    {
        eggsManagerMenu.SetActive(false);
    }

    public Text txtName;
    public Text txtLv;
    public Text txtExp;
    public Text txtHp;
    public Text txtAtk;
    public Text txtDef;

    public void OpenStat(int idCrea)
    {
        statWindow.SetActive(true);
        CreatureStats uneCrea = creatures[idCrea];
        txtName.text = uneCrea.name;
        txtLv.text = "Level " + uneCrea.lv.ToString();
        txtExp.text = "Exp : " + uneCrea.currentExp.ToString() + "/" + uneCrea.expToLvUp.ToString();
        txtAtk.text = "Attack :" + uneCrea.atk.ToString();
        txtDef.text = "Defense :" + uneCrea.def.ToString();
        txtHp.text = "HP :" + uneCrea.maxHealth.ToString();
        selectedCreature = idCrea;
        if (uneCrea.stade == 0)
        {
            btnChoice.SetActive(false);
        }
    }

    public void SelectCreatureButton()
    {
        PlayerPrefs.SetInt("CreatureUsed", creatures[selectedCreature].id);
        PlayerPrefs.Save();
    }

    public void CloseStat()
    {
        btnChoice.SetActive(true);
        statWindow.SetActive(false);
    }

    public void OpenHatchMenu(int idCrea)
    {
        selectedCreature = idCrea;
        hatchText.text = "You are trying to hatch a " + eggs[idCrea].name + "'s egg.";
        hatchMenu.SetActive(true);
        if (BDD.HasRuneToHatch(eggs[idCrea].typePrinc.ToString()))
        {
            btnHatch.SetActive(true);
        }
        else
        {
            txtCantHatch.SetActive(true);
        }
    }
    public void CloseHatchMenu()
    {
        hatchMenu.SetActive(false);
        btnHatch.SetActive(false);
        txtCantHatch.SetActive(false);
    }
    public void HatchEggButton()
    {
        BDD.HatchEgg(eggs[selectedCreature].id, eggs[selectedCreature].typePrinc.ToString());
        CloseHatchMenu();
        SetCreatures();
        SetEggs();
    }

    #region ItemManager
    public void OpenItemPanel()
    {
        itemManagerPanel.SetActive(true);
    }
    public void CloseItemPanel()
    {
        itemManagerPanel.SetActive(false);
    }

    public void OpenRunesPanel()
    {
        runesPanel.SetActive(true);
    }
    public void CloseRunesPanel()
    {
        runesPanel.SetActive(false);
    }
    #endregion

    public void MainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    void SetCreatures()
    {
        creatures = BDD.GetOwnedCreature();

        for (int a = 0; a < listCreaUI.transform.childCount; a++)
        {
            Destroy(listCreaUI.transform.GetChild(a).gameObject);
        }

        int i = 0;
        foreach (CreatureStats uneCrea in creatures)
        {
            GameObject pre = Instantiate(prefabCreature, listCreaUI.transform);
            pre.GetComponent<CreatureSelect>().SetSprite(i, uneCrea);

            i++;
        }
    }

    void SetEggs()
    {
        eggs = BDD.GetEggs();

        for (int a = 0; a < listEggUI.transform.childCount; a++)
        {
            Destroy(listEggUI.transform.GetChild(a).gameObject);
        }

        int i = 0;
        foreach (CreatureStats uneCrea in eggs)
        {
            GameObject pre = Instantiate(prefabEgg, listEggUI.transform);
            pre.GetComponent<CreatureSelect>().SetSprite(i, uneCrea);

            i++;
        }
    }

    public void SetRunes()
    {
        cRunes = BDD.GetRunesOwned();

        for(int i=0 ; i<runesContainerList.transform.childCount; i ++)
        {
            Destroy(runesContainerList.transform.GetChild(i).gameObject);
        }

        foreach (Item unItem in cRunes)
        {
            GameObject pre = Instantiate(prefabItem, runesContainerList.transform);
            pre.GetComponent<ItemManager>().SetItem(unItem);
        }
    }
}
