using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserBlink : MonoBehaviour
{
    public float onTime;
    public float offTime;

    private float timer;
    bool rendere;
    bool lighting;

    void Awake()
    {
        bool renderer = GetComponent<MeshRenderer>().enabled;
        bool light = GetComponent<Light>().enabled;
    }

    
    void Update()
    {
        timer += Time.deltaTime;

        if(rendere && timer >= onTime)
        {
            SwitchBeam();
        }

        if(!rendere && timer >= offTime)
        {
            SwitchBeam();
        }


    }

    private void SwitchBeam()
    {
        rendere = !rendere;
        lighting = !lighting;
    }
}
