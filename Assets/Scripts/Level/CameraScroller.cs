using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScroller : MonoBehaviour
{
    float startingX;
    float currentX;
    public GameObject playerOne;
    public GameObject playerTwo;
    float playerOneX;
    float playerTwoX;
    float playerOneSpawnX;
    float playerTwoSpawnX;

    public float speed;

    public float standerdDistance;

    


    // Start is called before the first frame update
    void Start()
    {
        playerOneSpawnX = GameObject.FindGameObjectWithTag("SpawnOne").transform.position.x;
        playerTwoSpawnX = GameObject.FindGameObjectWithTag("SpawnTwo").transform.position.x;

        startingX = transform.position.x;
    }

    // Update is called once per frame
    void Update()
    {
        //playerOne
        playerOne = GameObject.FindGameObjectWithTag("PlayerOne");
        if(playerOne != null)
        {
            GameObject torso = playerOne.GetComponentInChildren<PlayerController>().gameObject;
            if(torso != null)
            {
                playerOne = torso;
            }
        }

        //playone position
        if (playerOne != null)
        {
            playerOneX = playerOne.transform.position.x;
        }
        else
        {
            playerOneX = playerOneSpawnX;
        }


        //playerTwo
        playerTwo = GameObject.FindGameObjectWithTag("PlayerTwo");
        if (playerTwo != null)
        {
            GameObject torso = playerTwo.GetComponentInChildren<PlayerController>().gameObject;
            if (torso != null)
            {
                playerTwo = torso;
            }
        }

        //playTwo position
        if (playerTwo != null)
        {
            playerTwoX = playerTwo.transform.position.x;
        }
        else
        {
            playerTwoX = playerTwoSpawnX;
        }


        //pick player thats farthest along in level
        if (playerOneX < playerTwoX)
        {
            if(playerTwoX < startingX)
            {
                currentX = startingX;
            }
            else
            {
                currentX = playerTwoX;
            }
        }
        else
        {
            if (playerOneX < startingX)
            {
                currentX = startingX;
            }
            else
            {
                currentX = playerOneX;
            }
        }
        float distanceBetweenPlayers = Mathf.Abs(playerOneX - playerTwoX);

        Vector3 targetLocation = new Vector3(currentX, transform.position.y, transform.position.z);
        float distance = Vector3.Distance(transform.position, targetLocation);
        if (distance > standerdDistance)
        {
            transform.position = targetLocation;
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, targetLocation, speed * Time.deltaTime);
        }
        //transform.position = Vector3.MoveTowards(transform.position, targetLocation, speed * Time.deltaTime);
    }
}
