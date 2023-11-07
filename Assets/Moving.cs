using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class Moving : MonoBehaviour
{
    public Rigidbody _rigidbody;
    public float x;
    public float y;

    private void Start()
    {

        this._rigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector2 a = transform.position;
        a.x += 0.1f + Mathf.Min(transform.position.x / 10f * 0.01f,0.2f);
        Quaternion res = Quaternion.Euler(0,0,0);
        
        x = Input.GetAxis("Jump");
        bool isJump = transform.position.y > 1.26f;

        if (x != 0 && isJump == false)
        {
            _rigidbody.AddForce(new Vector2(1,3) * 3f, ForceMode.Impulse);
        }
        this.transform.position = a;
        this.transform.rotation = res;

        y = Input.GetAxis("Vertical");

        if(y != 0)
        {
            _rigidbody.AddForce(new Vector2(1,-2) * 2f, ForceMode.Impulse);
            isJump = false;
        }
        

    }
}
