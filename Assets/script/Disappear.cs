using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Disappear : MonoBehaviour
{
    public float duration;
    float timerVal = 0;

    public void Timer(float sec)
    {
        timerVal += Time.deltaTime;
        if (timerVal > sec)
        {
            Destroy(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Timer(duration);
    }
}
