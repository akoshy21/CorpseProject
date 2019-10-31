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
        deathText.text = "Workplace Accidents: " + GameManager.gm.totalCorpses;
    }

    public void LoadNext()
    {
        SceneManager.LoadScene(++GameManager.gm.curScn);
    }
}
