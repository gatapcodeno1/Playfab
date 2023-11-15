using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CreateRoom : MonoBehaviourPunCallbacks
{
    public TMP_InputField input;
    public Transform roomContent;
    public UIRoomProfile roomPrefab;
    public List<RoomInfo> updateRooms;
    public List<RoomProfile> rooms = new List<RoomProfile>();

    public static CreateRoom instance;
    private void Awake()
    {
        CreateRoom.instance = this;
    }



    private void Start()
    {
        this.input.text = "Room1";
        
    }

    public void SetInput(string input)
    {
        this.input.text = input;
    }
    

    public virtual void Create()
    {
        string name = input.text;
        Debug.Log(transform.name + ": Create Room " + name);
        PhotonNetwork.CreateRoom(name);
        
        
    }

    public virtual void Join()
    {
        string name = input.text;
        Debug.Log(transform.name + ": Join Room " + name);
        PhotonNetwork.JoinRoom(name);
        
    }

    public virtual void Leave()
    {
        string name = input.text;
        Debug.Log(transform.name + ": Leave Room " + name);
        PhotonNetwork.LeaveRoom();
    }


    public override void OnCreatedRoom()
    {
        base.OnCreatedRoom();
        Debug.Log("OnCreatedRoom");
    }

    public override void OnJoinedRoom()
    {
        base.OnJoinedRoom();
        Debug.Log("OnJoinedRoom");
    }

    public override void OnLeftRoom()
    {
        base.OnLeftRoom();
        Debug.Log("OnLeftRoom");
    }

    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        base.OnRoomListUpdate(roomList);
        Debug.Log("On Room List Update");
        Debug.Log(roomList.Count);
        this.updateRooms = roomList;
        foreach(RoomInfo roomInfo in roomList)
        {
            if (roomInfo.RemovedFromList) this.RoomAdd(roomInfo);
            else this.RoomAdd(roomInfo);
        }
        this.UpdateRoomProfileUI();
    }

    
    public virtual void StartGame()
    {
        Debug.Log(transform.name + ": Start Game");
        if (PhotonNetwork.IsMasterClient) PhotonNetwork.LoadLevel("PhotonPlay");
        else Debug.Log("You are not client master");
    }

    protected virtual void RoomAdd(RoomInfo roomInfo)
    {
        Debug.Log("ADD");
        RoomProfile roomProfile = new RoomProfile
        {
            name = roomInfo.Name,
        };
        this.rooms.Add(roomProfile);

    }

   

    protected virtual void UpdateRoomProfileUI()
    {
        
        foreach(Transform child in this.roomContent)
        {
            Destroy(child.gameObject);
        }
        foreach (RoomProfile roomProfile in this.rooms)
        {
            UIRoomProfile uiRoomProfile = Instantiate(this.roomPrefab);
            uiRoomProfile.SetRoomProfile(roomProfile);
            uiRoomProfile.transform.SetParent(this.roomContent);
            uiRoomProfile.transform.localScale= Vector3.one;
        }
    }

     protected virtual void RoomRemove(RoomInfo roomInfo)
    {
        
        RoomProfile roomProfile = this.RoomByName(roomInfo.Name);
        if (roomProfile == null) return;
        Debug.Log("remove");
        this.rooms.Remove(roomProfile);
    } 

    protected virtual RoomProfile RoomByName(string name)
    {
        foreach(RoomProfile roomProfile in this.rooms)
        {
            if(roomProfile.name == name) return roomProfile;
        }
        return null;
    }


    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        base.OnCreateRoomFailed(returnCode, message);
        Debug.Log("OnCreateRoomFailed" + message);
    }
}
