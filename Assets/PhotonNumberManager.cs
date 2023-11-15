using ExitGames.Client.Photon;
using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhotonNumberManager : MonoBehaviour
{
    protected void OnEnable()
    {
        PhotonNetwork.NetworkingClient.EventReceived += this.EventReceived;
    }

    protected void OnDisable()
    {
        PhotonNetwork.NetworkingClient.EventReceived -= this.EventReceived;
    }

    protected void EventReceived(EventData obj)
    {
        Debug.Log("Event Received: " + obj.Code.ToString());
        if (obj.Code == (byte)EventCode.onNumberClaimed) this.OnEventNumberClaimed(obj);
    }

    protected void OnEventNumberClaimed(EventData obj)
    {
        object[] datas = (object[])obj.CustomData;
        int number = (int)datas[0];
        Debug.Log("On Number Claimed: " + number);
        GameManager.instance.NumberOnClaimed(number);
    }


}
