using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class Follow : MonoBehaviour
{
    public Transform target;
    

    // Update is called once per frame
    void Update()
    {
        Vector3 a = target.position;
        a.x += 7.67f;
        a.z = -10;
        a.y = 4;

        this.transform.position = a;

        

    }




}
