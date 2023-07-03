using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Author: Matthew Tan 
 * Date: 1/7/2023
 * Description: This is the code for all of the player controls, raycasting and interactions
 */
public class Projectile : MonoBehaviour
{
    public float life = 2;

    void Awake()
    {
        Destroy(gameObject, life);
    }
}
