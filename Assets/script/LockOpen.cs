using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
