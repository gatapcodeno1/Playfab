using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PhotonLogin : MonoBehaviourPunCallbacks
{
    public TMP_InputField userName;
    private void Start()
    {
        this.userName.text = "Dat";
    }

    public virtual void Login()
    {
        string name = userName.text;
        Debug.Log(transform.name + ": Login" + name);
        PhotonNetwork.AutomaticallySyncScene = true;
        PhotonNetwork.LocalPlayer.NickName = name;
        PhotonNetwork.ConnectUsingSettings();
    }

    


    public override void OnJoinedLobby()
    {
        base.OnJoinedLobby();
        Debug.Log("OnJoinLobby");
    }

    public override void OnConnectedToMaster()
    {
        base.OnConnectedToMaster();
        Debug.Log("On Connected To Master");
        PhotonNetwork.JoinLobby();
        
    }

}
