using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Size : MonoBehaviour
{
    public Size _size;
    public int id = 5;
    void Awake()
    {
        _size = GetComponent<Size>();
    }

    public void CircleSize()
    {

    }

    public void CubeSize()
    {

    }
}
