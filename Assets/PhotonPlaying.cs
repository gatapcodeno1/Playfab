using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhotonPlaying : MonoBehaviourPunCallbacks
{

    public static PhotonPlaying instance;
    public string photonPlayerName = "PhotonPlayer";
    public List<PlayerProfile> players = new List<PlayerProfile>();

    protected void Awake()
    {
        PhotonPlaying.instance = this;
        this.LoadRoomPlayers();
        this.SpawnPlayer();
    }

    public virtual void Leave()
    {
        Debug.Log(transform.name + ": Leave Room");
        PhotonNetwork.LeaveRoom();

    }


    protected virtual void SpawnPlayer()
    {
        if(PhotonNetwork.NetworkClientState == ClientState.Joined)
        {
            this.LoadPlayerPrefab();
            return;
        }

        GameObject playerObj = Resources.Load(this.photonPlayerName) as GameObject;
        Instantiate(playerObj);

    }

    protected virtual void LoadPlayerPrefab()
    {
        PhotonNetwork.Instantiate(this.photonPlayerName, Vector3.zero, Quaternion.identity);
    }

    protected virtual void LoadRoomPlayers()
    {
        if (PhotonNetwork.NetworkClientState != ClientState.Joined) return;
        PlayerProfile playerProfile;
        foreach (KeyValuePair<int,Player> playerData in PhotonNetwork.CurrentRoom.Players)
        {
            Debug.Log(playerData.Value.NickName);
            playerProfile = new PlayerProfile
            {
                nickName = playerData.Value.NickName,
            };
            this.players.Add(playerProfile);
        }
    } 

    public override void OnLeftRoom()
    {
        base.OnLeftRoom();
        Debug.Log(transform.name + ": On Left Room");
        PhotonNetwork.LoadLevel("Photon");
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        base.OnPlayerEnteredRoom(newPlayer);
        Debug.Log("On Player Entered Room " + newPlayer.NickName);
    }
    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        base.OnPlayerLeftRoom(otherPlayer);
        Debug.Log("On Player Left Room " + otherPlayer.NickName);
    }
}
