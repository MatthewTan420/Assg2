using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCrane : MonoBehaviour
{
    public GameObject box;

    public void buttionMethode()
    {
        //open sesame
        GetComponent<Animator>().SetTrigger("liftOn");
    }
}
