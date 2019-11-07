using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    //AK

    public GameObject levelView, door;
    public float doorHeight;

    public enum Menu { Start, Tutorial, LvlSelect }
    public Menu men;

    bool doorRise = false;

    void Start()
    {
        if (men == Menu.Start)
        {
            StartCoroutine(DoorDelay(1.5f));
        }
    }

    void Update()
    {
        if (men == Menu.Start)
        {
            if (doorRise && door.transform.position.y <= doorHeight)
            {
                door.transform.Translate(new Vector3(0, 0.01f, 0));
            }
        }
    }

    IEnumerator DoorDelay(float sec)
    {
        yield return new WaitForSeconds(sec);
        doorRise = true;
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
