using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/*
 * Author: Matthew Tan 
 * Date: 1/7/2023
 * Description: This is the code for all of the player controls, raycasting and interactions
 */
public class Button : MonoBehaviour
{
    public UnityEvent unityEvent = new UnityEvent();
    public GameObject button;

    // Start is called before the first frame update
    void Start()
    {
        button = this.gameObject;
    }

    public void buttonClick()
    {
        unityEvent.Invoke();
    }

}