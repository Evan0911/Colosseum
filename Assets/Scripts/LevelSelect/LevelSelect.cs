using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelSelect : MonoBehaviour
{
    List<int> levels;
    public GameObject prefab;
    public GameObject levelsUI;

    /*public static LevelSelect instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }*/

    void Start()
    {
        levels = BDD.GetLevels();

        int i = 0;
        foreach(int unLevel in levels)
        {
            i++;
            GameObject levelObj = Instantiate(prefab, levelsUI.transform);
            levelObj.GetComponentInChildren<Text>().text = i.ToString();
            levelObj.GetComponent<LevelButton>().numLevel = i;
            if (unLevel == 1)
            {
                levelObj.GetComponent<LevelButton>().LevelCleared();
            }
        }
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
