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

    // Update is called once per frame
    void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        //if user mouse is at the button
        if (Input.GetMouseButtonDown(0))
        {
            //if user clicks on the button
            if (Physics.Raycast(ray, out hit) && hit.collider.gameObject == gameObject)
            {
                //what happens after clicking
                unityEvent.Invoke();
            }
        }
    }
}