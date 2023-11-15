using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using System.Collections.Generic;
using ExitGames.Client.Photon.StructWrapping;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public Vector2 xPosLimit = new Vector2(-16f, 0f);
    public Vector2 yPosLimit = new Vector2(-4f,5);
    public int maxNumber = 55;
    public int numberPerLine = 11;
    public string numberPrefab = "PhotonNumber";
    public List<int> numbers = new List<int>();
    public List<PhotonNumber> photonNumbers = new List<PhotonNumber>();
    protected void Awake()
    {
        GameManager.instance = this;
        this.RandomNumber();
        if (PhotonNetwork.IsMasterClient) this.MasterSpawnNumbers();
        else this.ClientReceivedNumbers();

    }

    protected virtual void ClientReceivedNumbers()
    {
        Debug.Log("ClientReceivedNumbers");
        PhotonNumber[] photonNumbers = GameObject.FindObjectsOfType<PhotonNumber>();
        foreach (PhotonNumber photonNumber in photonNumbers)
        {
            this.photonNumbers.Add(photonNumber);
        }
        if (this.photonNumbers.Count == 0) Invoke("ClientReceivedNumbers", 1f);
    }


    
    public virtual void MasterSpawnNumbers()
    {
        if (PhotonNetwork.NetworkClientState != ClientState.Joined)
        {
            Invoke("GameStart", 1f);
            return;
        }
        for (int i = 0; i < this.maxNumber; i++)
        {
            int lineNumber = Mathf.RoundToInt(i / this.numberPerLine);
            int colNumber = Mathf.RoundToInt(i % this.numberPerLine);
            this.SpawnNumber(i, lineNumber, colNumber);
        }
    }
    protected virtual void SpawnNumber(int number, int lineNumber, int colNumber)
    {
        if (!PhotonNetwork.IsMasterClient) return;
        GameObject numberObj;
        //if (PhotonNetwork.NetworkClientState == ClientState.Joined) numberObj = this.SpawnNetwork(number);
        //else numberObj = this.SpawnLocal(number);
        numberObj = this.SpawnNetwork(number);
        Vector3 pos = this.StartPoint();
        pos.x += colNumber * 1f;
        pos.y -= lineNumber * 1f;
        numberObj.transform.position = pos;
        PhotonNumber photonNumber = numberObj.GetComponent<PhotonNumber>();
        photonNumber.Set(this.GetNumber());
        this.photonNumbers.Add(photonNumber);
    }
    protected virtual void RandomNumber()
    {
        for (int i = 0; i < this.maxNumber; i++)
        {
            this.numbers.Add(i);
        }
    }
    protected virtual int GetNumber()
    {
        int rand = Random.Range(0, this.numbers.Count);
        int number = this.numbers[rand];
        this.numbers.RemoveAt(rand);
        return number;
    }
    protected virtual GameObject SpawnLocal(int i)
    {
        GameObject numberLocal = Resources.Load(this.numberPrefab) as GameObject;
        return Instantiate(numberLocal);
    }
    protected virtual GameObject SpawnNetwork(int i)
    {
        return PhotonNetwork.Instantiate(this.numberPrefab, Vector3.zero, Quaternion.identity);
    }
    protected virtual Vector3 StartPoint()
    {
        return new Vector3(this.xPosLimit.x, this.yPosLimit.y, 0);
    }


    public virtual void NumberOnClaimed(int number)
    {
        PhotonNumber photonNumber = this.FindPhotonNumber(number);
        if (!photonNumber)
        {
            Debug.Log("Can not find number " + number.ToString());
            return;
        }
        photonNumber.Claimed();

        if (PhotonNetwork.IsMasterClient) PhotonNumberLimit.instance.Set(photonNumber.number + 1);
    }

    public virtual PhotonNumber FindPhotonNumber(int number)
    {
        foreach (PhotonNumber photonNumber in this.photonNumbers)
        {
            if (photonNumber.number == number) return photonNumber;
        }
        return null;
    }


    

}