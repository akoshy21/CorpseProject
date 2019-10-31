using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager gm;

    [HideInInspector] public bool paused;
    [HideInInspector] public int curScn; //current scene
    [HideInInspector] public int corpseCount1, corpseCount2, totalCorpses;

    private void Awake()
    {
        gm = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        curScn = SceneManager.GetActiveScene().buildIndex;
    }

    // Update is called once per frame
    void Update()
    {
    }
}
