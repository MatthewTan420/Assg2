using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ship_Fall : MonoBehaviour
{
    public GameObject Target;

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Bullet")
        {
            Target.SetActive(false);
            GetComponent<Animator>().SetTrigger("isShot");
        }
    }
}
