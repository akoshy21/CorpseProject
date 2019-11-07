using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LvlEndScreen : MonoBehaviour
{
    public Text deathText;

    private void OnEnable()
    {
       // deathText.text = "Workplace Accidents: " + LevelManager.lm.totalCorpses;
    }

    private void Update()
    {
        if (Input.anyKeyDown)
        {
            LoadNext();
        }
    }

    public void LoadNext()
    {
        if(LevelManager.lm != null)
           Destroy(LevelManager.lm.gameObject);
        SceneManager.LoadScene(++GameManager.gm.lastScn);
    }
}
