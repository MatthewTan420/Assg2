using UnityEngine.Audio;
using UnityEngine;

/*
 * Author: Matthew Tan 
 * Date: 1/7/2023
 * Description: This is the code for all of the player controls, raycasting and interactions
 */
public class Volume : MonoBehaviour
{
    public AudioMixer mainMixer;

    public void setVolume(float volume)
    {
        mainMixer.SetFloat("MyExposedParam", volume);
    }
}
