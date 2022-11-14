using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    private void Start()
    {
        //BDD.CreateDB();
        if (!PlayerPrefs.HasKey("CreatureUsed"))
        {
            PlayerPrefs.SetInt("CreatureUsed", 1);
        }
    }

    public void TeamManagerButton()
    {
        SceneManager.LoadScene("Inventory");
    }

    public void LevelSelectButton()
    {
        SceneManager.LoadScene("LevelSelect");
    }

    public void BlackmarketButton()
    {
        SceneManager.LoadScene("Blackmarket");
    }

    public void CreditsButton()
    {
        SceneManager.LoadScene("Credits");
    }

    public void QuitButton()
    {
        Application.Quit();
    }
}
