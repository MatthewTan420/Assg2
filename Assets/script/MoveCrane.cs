using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Author: Matthew Tan 
 * Date: 1/7/2023
 * Description: This is the code for all of the player controls, raycasting and interactions
 */
public class MoveCrane : MonoBehaviour
{
    public GameObject box;

    public void buttionMethode()
    {
        //open sesame
        GetComponent<Animator>().SetTrigger("liftOn");
    }
}
