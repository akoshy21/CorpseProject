using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    //AK

    public GameObject levelView;

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void ViewLevels()
    {
        levelView.SetActive(true);
    }

    public void LoadLevelOne()
    {
        SceneManager.LoadScene("FakeLevel1");
    }

    public void LoadLevelTwo()
    {
        SceneManager.LoadScene("FakeLevelFast");
    }

    public void LoadLevelThree()
    {
        SceneManager.LoadScene("Level 1");
    }
}
