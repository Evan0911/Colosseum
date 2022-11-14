using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelButton : MonoBehaviour
{
    public int numLevel;
    public GameObject clearMark;

    public void LoadLevel()
    {
        PlayerPrefs.SetInt("NumLevel", numLevel);
        PlayerPrefs.Save();
        SceneManager.LoadScene("Battle");
    }

    public void LevelCleared()
    {
        clearMark.SetActive(true);
    }
}
