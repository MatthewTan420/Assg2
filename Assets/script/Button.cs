using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

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