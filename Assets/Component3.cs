using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Component3 : MonoBehaviour
{

    public static Component3 com3;

    // Start is called before the first frame update
    void Start()
    {
        com3 = this;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void M1()
    {
        Debug.Log("This is M1");

    }

    public void M2()
    {
        Debug.Log("This is M2");

    }




}

public class Component5 : Component3
{
    

}




public class Component4  : Component3
{

    

    public void M3()
    {
        Debug.Log("This is M3");

        com3.M1();
        com3.M2();
       // com3.Method1();

    }

}
