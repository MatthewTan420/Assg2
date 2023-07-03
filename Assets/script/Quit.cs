using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/*
 * Author: Matthew Tan 
 * Date: 1/7/2023
 * Description: This is the code for all of the player controls, raycasting and interactions
 */
public class Quit : MonoBehaviour
{
    public void quitGame()
    {
        SceneManager.LoadScene(5);
    }
}
