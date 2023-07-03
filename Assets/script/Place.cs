using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Author: Matthew Tan 
 * Date: 1/7/2023
 * Description: This is the code for all of the player controls, raycasting and interactions
 */
public class Place : MonoBehaviour
{
    public GameObject button;
    public GameObject obJ;
    public GameObject empty;
    public GameObject Door;
    public Bomb_PickUp script;

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
                if (script.PickUp)
                {
                    obJ.SetActive(true);
                    empty.SetActive(true);
                    Door.SetActive(false);
                }
            }
        }
    }
}
