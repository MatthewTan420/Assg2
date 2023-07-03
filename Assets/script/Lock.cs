using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/*
 * Author: Matthew Tan 
 * Date: 1/7/2023
 * Description: This is the code for all of the player controls, raycasting and interactions
 */
public class Lock : MonoBehaviour
{
    public GameObject text;
    public GameObject door;
    public bool Key = false;

    void OnTriggerEnter(Collider other)
    {
        text.SetActive(true);
    }

    void OnTriggerExit(Collider other)
    {
        text.SetActive(false);
    }

    void OnCollisionEnter(Collision collision)
    {
        if (gameObject.tag == "Key")
        {
            Key = true;
            Destroy(gameObject);
        }
    }
}
