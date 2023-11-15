using ExitGames.Client.Photon;
using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class PhotonNumber : MonoBehaviourPun , IPunObservable
{
    // Start is called before the first frame update

    public TextMeshPro textNumber;
    public int number = 0;


    
    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        Debug.Log("OnPhotonSerializeView");
        if (stream.IsWriting) this.StreamWriting(stream);
        else this.StreamReading(stream, info);
    }

    protected virtual void StreamWriting(PhotonStream stream)
    {
        Debug.Log("StreamWriting");
        stream.SendNext(this.number);
        
    }

    protected virtual void StreamReading(PhotonStream stream,PhotonMessageInfo info)
    {
        Debug.Log("StreamReading");
        this.number = (int) stream.ReceiveNext();
        this.textNumber.text = this.number.ToString();
    }

    public virtual void Onclaim()
    {
        Debug.Log(transform.name + ": OnClaim " + this.number);

        if (!PhotonNumberLimit.instance.CanClaim(this.number)) return;

        object[] datas = new object[] { this.number };
        RaiseEventOptions raiseEventOptions = new RaiseEventOptions { Receivers = ReceiverGroup.All };
        PhotonNetwork.RaiseEvent(
            ((byte)EventCode.onNumberClaimed),
            datas,
            raiseEventOptions,
            SendOptions.SendUnreliable
            );
    }

    public virtual void Set(int number)
    {
        this.number = number;
        this.textNumber.text = number.ToString();
    }

    internal void Claimed()
    {
        Debug.Log("Claimed: " + this.number);
        gameObject.SetActive(false);
    }

}
