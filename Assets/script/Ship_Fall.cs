using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Author: Matthew Tan 
 * Date: 1/7/2023
 * Description: This is the code for all of the player controls, raycasting and interactions
 */
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
