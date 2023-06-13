using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{

    void OnTriggerEnter(Collider other)
    {
        GetComponent<Animator>().SetTrigger("Enter");
    }

    void OnTriggerExit(Collider other)
    {
        GetComponent<Animator>().SetTrigger("Exit");
    }
}
