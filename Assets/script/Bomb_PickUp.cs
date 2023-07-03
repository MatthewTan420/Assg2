using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Author: Matthew Tan 
 * Date: 1/7/2023
 * Description: This is the code for all of the player controls, raycasting and interactions
 */
public class Bomb_PickUp : MonoBehaviour
{
    public GameObject button;
    public bool PickUp = false;

    // Start is called before the first frame update
    void Start()
    {
        button = this.gameObject;
    }

    // Update is called once per frame
    public void bombUp()
    {
        PickUp = true;
        Destroy(gameObject);
    }
}
