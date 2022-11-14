using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ShopManager : MonoBehaviour
{
    public Text dialogueText;

    public Text goldAmountText;

    public GameObject buttonsMenu;
    public GameObject eggsMenu;
    public GameObject confirmBuyEggMenu;
    public Text confirmEggText;
    public GameObject yesEggButton;
    public GameObject runesMenu;
    public GameObject confirmBuyRuneMenu;
    public Text confirmRuneText;
    public GameObject yesRuneButton;

    int gold;

    int selectedItem;

    public GameObject eggsListContainer;
    public GameObject runesListContainer;
    public GameObject eggPrefab;
    public GameObject runePrefab;

    List<Item> eggs;
    List<Item> runes;

    public static ShopManager instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    void Start()
    {
        gold = PlayerPrefs.GetInt("Money");
        goldAmountText.text = gold.ToString();
        StartCoroutine(SetText());

        eggs = BDD.GetEggsShop();
        runes = BDD.GetRunesShop();

        int i = 0;
        foreach (Item unItem in eggs)
        {
            unItem.name += "'s egg";
            GameObject pre = Instantiate(eggPrefab, eggsListContainer.transform);
            pre.GetComponent<ItemSelect>().SetText(i, unItem);

            i++;
        }

        i = 0;
        foreach (Item uneCrea in runes)
        {
            GameObject pre = Instantiate(runePrefab, runesListContainer.transform);
            pre.GetComponent<ItemSelect>().SetText(i, uneCrea);

            i++;
        }
    }

    IEnumerator SetText()
    {
        string dialogue = dialogueText.text;
        dialogueText.text = "";
        char[] letters = dialogue.ToCharArray();
        foreach(char letter in letters)
        {
            dialogueText.text += letter;
            yield return null;
        }
    }

    #region Egg
    public void OpenEggsMenu()
    {
        buttonsMenu.SetActive(false);
        eggsMenu.SetActive(true);
    }
    public void CloseEggsMenu()
    {
        buttonsMenu.SetActive(true);
        eggsMenu.SetActive(false);
        confirmBuyEggMenu.SetActive(false);
        yesEggButton.SetActive(false);
    }
    public void BuyEgg(int idItem)
    {
        selectedItem = idItem;
        confirmBuyEggMenu.SetActive(true);
        if (gold < eggs[idItem].prix)
        {
            confirmEggText.text = "You can't buy this egg, come back when you are a little hum... Richer !";
        }
        else
        {
            confirmEggText.text = "You are trying to buy an " + eggs[idItem].name + " for " + eggs[idItem].prix + " gold.\nDo you agree ? ";
            yesEggButton.SetActive(true);
        }
    }
    public void ConfirmBuyEggButton()
    {
        Debug.Log("you bought an egg");
        BDD.SaveNewEgg(eggs[selectedItem].id);
        confirmBuyEggMenu.SetActive(false);
        yesEggButton.SetActive(false);
        gold -= eggs[selectedItem].prix;
        PlayerPrefs.SetInt("Money", gold);
        goldAmountText.text = gold.ToString();
    }
    public void CancelBuyEggButton()
    {
        confirmBuyEggMenu.SetActive(false);
        yesEggButton.SetActive(false);
    }
    #endregion

    #region Rune
    public void OpenRunesMenu()
    {
        buttonsMenu.SetActive(false);
        runesMenu.SetActive(true);
    }
    public void CloseRunesMenu()
    {
        buttonsMenu.SetActive(true);
        runesMenu.SetActive(false);
        confirmBuyRuneMenu.SetActive(false);
        yesRuneButton.SetActive(false);
    }
    public void BuyRune(int idItem)
    {
        selectedItem = idItem;
        confirmBuyRuneMenu.SetActive(true);
        if (gold < runes[idItem].prix)
        {
            confirmRuneText.text = "You can't buy this rune, come back when you are a little hum... Richer !";
        }
        else
        {
            confirmRuneText.text = "You are trying to buy an " + runes[idItem].name + " for " + runes[idItem].prix + " gold.\nDo you agree ? ";
            yesRuneButton.SetActive(true);
        }
    }
    public void ConfirmBuyRuneButton()
    {
        Debug.Log("you bought a rune");
        BDD.SaveNewItem(runes[selectedItem].id);
        confirmBuyRuneMenu.SetActive(false);
        yesRuneButton.SetActive(false);
        gold -= runes[selectedItem].prix;
        PlayerPrefs.SetInt("Money", gold);
        goldAmountText.text = gold.ToString();
    }
    public void CancelBuyRuneButton()
    {
        confirmBuyRuneMenu.SetActive(false);
        yesRuneButton.SetActive(false);
    }
    #endregion

    public void MainMenuButton()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
