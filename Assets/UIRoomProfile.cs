﻿using Photon.Pun;
using Photon.Realtime;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
public class UIRoomProfile : MonoBehaviour
{
    [SerializeField] protected TextMeshProUGUI roomName;
    [SerializeField] protected RoomProfile roomProfile;
    


    public virtual void SetRoomProfile(RoomProfile roomProfile)
    {
        this.roomProfile = roomProfile;
        this.roomName.text = this.roomProfile.name;
    }


    public virtual void OnClick()
    {
        Debug.Log("OnClick :" + roomProfile.name);
        CreateRoom.instance.input.text = this.roomProfile.name;
    }
}