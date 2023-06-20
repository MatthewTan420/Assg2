using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Start : MonoBehaviour
{
    public bool start = false;

    public void startGame()
    {
        start = true;
        SceneManager.LoadScene(0);
    }
}
