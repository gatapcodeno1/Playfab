using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Create : MonoBehaviour
{
    public Transform target;

    public int max;
    public int min;

    

    void Update()
    {
        
        if(this.transform.position.x + 3f < target.transform.position.x)
        {
            Vector2 a = transform.position;
            a.x += Random.Range(min, max);
            this.transform.position = a;
        }
        

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.name != "Capsule") return;
        Debug.Log("vacham");
        SceneManager.LoadScene("SampleScene");
    }


}
