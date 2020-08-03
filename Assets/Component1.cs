using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class Component1 : MonoBehaviour, IPointerClickHandler
{

    /*Component1()
    {

    }*/
   
  public static  Component1 component1;


    void Awake()
    {
        component1 = this;
    }


    // Start is called before the first frame update
    void Start()
    {
         UnityEngine.Object obj = gameObject;



        //gameObject.AddComponent<Component1>();
        //gameObject.AddComponent(Type.GetType("Component2"));

        this.GetComponent<Rigidbody>();

        gameObject.GetComponent<Rigidbody>();


        gameObject.AddComponent<Component1>();



        MonoBehaviour abc = GetComponent<Component1>();
        
    }

    

    // Update is called once per frame
    void Update()
    {
        
    }


    private void Method()
    {
        print("This is Method");

    }

    public void Method1()
    {
        print("This is Method1");

    }

    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("This is a Intraface Implimantation");

    }


}
