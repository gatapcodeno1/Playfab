using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseClickEvents : MonoBehaviour
{
    private void OnMouseDown()
    {
        Debug.Log("On Mouse Down");
        PhotonNumber photonNumber = transform.parent.GetComponent<PhotonNumber>();
        photonNumber.Onclaim();
    }
}
