using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RagdollManager : MonoBehaviour
{

// Annamaria Koshy
// Slight restructuring by Carsen Decker

    public bool dead;
    public float rotateSpeed;
    public Sprite dead1, dead2;

    private PlayerController controller;
    private List<GameObject> children = new List<GameObject>();
    private Starter spawn;

    

    void Start()
    {
        controller = GetComponentInChildren<PlayerController>();
        GetAllChildren(transform);
        if (!dead)
        {
            if (controller.playerInt == 1)
            {
                spawn = GameObject.FindGameObjectWithTag("SpawnOne").GetComponent<Starter>();
                //            Debug.Log("p1 spawner set to" + spawn.gameObject.name);
            }
            else if (controller.playerInt == 2)
            {
                spawn = GameObject.FindGameObjectWithTag("SpawnTwo").GetComponent<Starter>();
            }
            else
            {
                throw new System.Exception("Player int is not 1 or 2");
            }
        }

        if (dead)
        {
            CreateRagdoll();
        }
    }


    public void CreateRagdoll()
    {

        foreach (var child in children)
        {
            if (child.gameObject.name.Equals("Head"))
            {
                if (controller.playerInt == 1)
                {
                    child.GetComponent<SpriteRenderer>().sprite = dead1;
                    child.GetComponent<SpriteRenderer>().color = Color.white;
                }
                else
                {
                    child.GetComponent<SpriteRenderer>().sprite = dead2;
                    child.GetComponent<SpriteRenderer>().color = Color.white;
                }
            }

            if (child.gameObject.GetComponent<PlayerController>() != null)
            {
                Destroy(child.gameObject.GetComponent<PlayerController>());
            }

            if (child.GetComponent<AltAiming>() != null)
            {
                Destroy(child.GetComponent<AltAiming>());
            }

            if (child.gameObject.GetComponent<Rigidbody2D>() != null)
            {
                child.gameObject.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.None;
                child.gameObject.GetComponent<Rigidbody2D>().gravityScale = 1;
            }

            child.gameObject.tag = "Corpse";
            gameObject.tag = "Corpse";
            if (child.gameObject.layer != 0)
            {
                child.gameObject.layer = 0;
            }
        }

        if (spawn != null)
        {
            spawn.newChild();
        }
    }

    private void GetAllChildren(Transform currentTransform)
    {
        foreach (Transform child in currentTransform)
        {
            children.Add(child.gameObject);
            GetAllChildren(child.transform);
        }
    }
    

    private void CorrectRotation()
    {
        if (Mathf.Abs(Quaternion.Angle(Quaternion.identity, transform.rotation)) <= 2)
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.identity, Time.time * rotateSpeed);
        }
    }
}
