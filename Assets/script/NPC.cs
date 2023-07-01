using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour
{
    private bool DetectionRange = false;
    private bool hitOn = false;
    public GameObject textList;
    public GameObject text;
    public GameObject text1;
    public GameObject text2;
    public GameObject text3;
    public GameObject text4;
    private int textNum = 0;

    public PlayerControl PlayerControl;

    void OnTriggerEnter(Collider other)
    {
        DetectionRange = true;
        textList.SetActive(true);
    }

    void OnTriggerExit(Collider other)
    {
        DetectionRange = false;
        textList.SetActive(false);
    }

    // Update is called once per frame
    public void npcTalk()
    {
        if (DetectionRange)
        {
            textNum++;
            if (textNum == 1)
            {
                text.SetActive(false);
                text1.SetActive(true);
                hitOn = false;
            }
            if (textNum == 2)
            {
                text1.SetActive(false);
                text2.SetActive(true);
                hitOn = false;
            }
            if (textNum == 3)
            {
                text2.SetActive(false);
                text3.SetActive(true);
                hitOn = false;
            }
            if (textNum == 4)
            {
                text3.SetActive(false);
                text4.SetActive(true);
                hitOn = false;
            }
        }
    }
}
