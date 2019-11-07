using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuDoor : MonoBehaviour
{
    Animation anim;

    private void Start()
    {
        anim = this.GetComponent<Animation>();
    }

    private void OnCollisionEnter2D()
    {
        StartCoroutine(ToLevels());
    }

    IEnumerator ToLevels()
    {
        anim.Play("jump");
        yield return new WaitForSeconds(1f);

    }
}
