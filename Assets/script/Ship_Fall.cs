using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ship_Fall : MonoBehaviour
{
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Bullet")
        {
            GetComponent<Animator>().SetTrigger("isShot");
        }
    }
}
