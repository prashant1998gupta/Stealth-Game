using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Component2 : MonoBehaviour
{
  
    // Start is called before the first frame update
    void Start()
    {
        
        gameObject.BroadcastMessage("Method1");
        gameObject.BroadcastMessage("Method");
    }

    // Update is called once per frame
    void Update()
    {
        
    }



}
