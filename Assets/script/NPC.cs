using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Author: Matthew Tan 
 * Date: 1/7/2023
 * Description: This is the code for all of the player controls, raycasting and interactions
 */
public class NPC : MonoBehaviour
{
    private bool DetectionRange = false;
    public GameObject textList;
    public GameObject text;
    public GameObject text1;
    public GameObject text2;
    public GameObject text3;
    public GameObject text4;
    private int textNum = 0;

    public PlayerControl PlayerControl;

    ///<summary>
    /// if within NPC range
    ///</summary>
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

    ///<summary>
    /// NPC talk to you
    ///</summary>
    public void npcTalk()
    {
        if (DetectionRange)
        {
            textNum++;
            if (textNum == 1)
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
