using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Author: Matthew Tan 
 * Date: 1/7/2023
 * Description: This is the code for all of the player controls, raycasting and interactions
 */
public class LockOpen : MonoBehaviour
{
    public GameObject door;
    public GameObject txt;
    public Lock script;

    public void buttionMethode()
    {
        if (script.Key)
        {
            door.SetActive(false);
        }
        else
        {
            txt.SetActive(true);
        }
    }   
}
