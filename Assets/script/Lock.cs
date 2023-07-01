using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

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
