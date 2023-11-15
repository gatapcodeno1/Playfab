using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PhotonStatus : MonoBehaviourPunCallbacks
{
    public string photonStatus;
    public TextMeshProUGUI textStatus;

    private void Update()
    {
        this.photonStatus = PhotonNetwork.NetworkClientState.ToString();
        this.textStatus.text = this.photonStatus;
    }

}
