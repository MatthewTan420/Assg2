using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collide_Trigger : MonoBehaviour
{
    public GameObject text;

    void OnTriggerEnter(Collider other)
    {
        text.SetActive(true);
    }

    void OnTriggerExit(Collider other)
    {
        text.SetActive(false);
    }

    void TeleCheck()
    {
        if (gameObject.tag == "Teleport")
        {
            Debug.Log(gameObject.tag);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
