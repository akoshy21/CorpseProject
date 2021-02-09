using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/// <summary>
/// This script is niche af
/// </summary>
public class YellowInFrontOfDoor : MonoBehaviour
{
    public Color YellowColor;

    private Color originalColor;
    private SpriteRenderer sr;

    private void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        originalColor = sr.color;
    }

    void Update()
    {
        //Wait this doesn't work the doors ignore raycasts smh
        RaycastHit2D[] behindCheck = Physics2D.RaycastAll(transform.position, Vector2.left);

        bool foundDoor = false;
        foreach (RaycastHit2D hit in behindCheck)
        {
            if (hit.collider.GetComponent<Goal>())
            {
                foundDoor = true;
            }
        }

        if (foundDoor)
            sr.color = YellowColor;
        else
            sr.color = originalColor;
    }

}
