using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Author: Matthew Tan 
 * Date: 1/7/2023
 * Description: This is the code for all of the player controls, raycasting and interactions
 */
public class Disappear : MonoBehaviour
{
    public float duration;
    float timerVal = 0;

    public void Timer(float sec)
    {
        timerVal += Time.deltaTime;
        if (timerVal > sec)
        {
            Destroy(gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        Timer(duration);
    }
}
