using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/*
 * Author: Matthew Tan 
 * Date: 1/7/2023
 * Description: This is the code for all of the player controls, raycasting and interactions
 */
public class HealthBar : MonoBehaviour
{

    public Slider slider;
    
    public void SetMaxHealth(float health)
    {
        slider.maxValue = health;
        slider.value = health; //slider = maxhealth
    }

    public void SetHealth(float health)
    {
        slider.value = health; //slider = currenthealth
    }

}