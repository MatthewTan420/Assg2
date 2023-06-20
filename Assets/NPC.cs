using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour
{
    private bool DetectiontRange = false;
    public GameObject text;
    public GameObject text1;
    public GameObject text2;
    public GameObject text3;
    public GameObject text4;
    private int textNum = 0;

    void OnTriggerEnter(Collider other)
    {
        DetectiontRange = true;
        text.SetActive(true);
    }

    void OnTriggerExit(Collider other)
    {
        DetectiontRange = false;
        text.SetActive(false);
    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (DetectiontRange && Input.GetKey("e"))
        {
            textNum++;
            if(textNum == 1)
            {
                text.SetActive(false);
                text1.SetActive(true);
            }
            if (textNum == 2)
            {
                text1.SetActive(false);
                text2.SetActive(true);
            }
            if (textNum == 3)
            {
                text2.SetActive(false);
                text3.SetActive(true);
            }
            if (textNum == 4)
            {
                text3.SetActive(false);
                text4.SetActive(true);
            }
        }
    }
}
