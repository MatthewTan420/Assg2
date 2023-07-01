using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb_PickUp : MonoBehaviour
{
    public GameObject button;
    public bool PickUp = false;

    // Start is called before the first frame update
    void Start()
    {
        button = this.gameObject;
    }

    // Update is called once per frame
    public void bombUp()
    {
        PickUp = true;
        Destroy(gameObject);
    }
}
