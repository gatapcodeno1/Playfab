using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{

    public Rigidbody _rigidbody;
    
    void Update()
    {
        Vector3 a = transform.position;
        a.x += 0.01f;
        

        bool isjump = transform.position.y > 1.52;

        float x = Input.GetAxis("Jump");

        Quaternion res = Quaternion.Euler(0, 0, 0);
        this.transform.rotation = res;
        a.z = 0;

        if (x != 0 && isjump == false)
        {
            _rigidbody.AddForce(new Vector2(1, 3) * 3f, ForceMode.Impulse);
            isjump = true;
        }

        float y = Input.GetAxis("Vertical");

        if (y != 0)
        {
            _rigidbody.AddForce(new Vector2(1, -2) * 2f, ForceMode.Impulse);
            isjump = false;
        }


        this.transform.position = a;





    }
}
