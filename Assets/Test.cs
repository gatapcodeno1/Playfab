using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayFab;
using PlayFab.ClientModels;
using UnityEngine.UI;
using TMPro;

public class Test : MonoBehaviour
{



    private void Start()
    {
        Login();
        Debug.Log(testnow.Instance.Get());

    }

    void Login()
    {
        var request = new LoginWithCustomIDRequest
        {
            CustomId = SystemInfo.deviceUniqueIdentifier,
            CreateAccount = true
        };
        PlayFabClientAPI.LoginWithCustomID(request, OnSuccess, OnError);

    }

    void OnSuccess(LoginResult loginResult)
    {
        Debug.Log("Successful Login/Account Create! ");
        SendLeaderBoard(5);
        GetLeaderBoard();
        SaveApperance();
        GetTitleData();
    }

    void OnError(PlayFabError error)
    {
        Debug.Log("Error while Login/Create account!");
        Debug.Log(error.GenerateErrorReport());
    }


    public void SaveApperance()
    {
        // Debug.Log(test.Get());

    }

    void GetTitleData()
    {
        PlayFabClientAPI.GetTitleData(new GetTitleDataRequest(), OnTitleDataReceived, OnError);
    }


    public void OnTitleDataReceived(GetTitleDataResult result)
    {
        if (result.Data == null || !result.Data.ContainsKey("Message"))
        {
            Debug.Log("No testkey Data");
        }
        else
        {
            testnow.Instance.Text.text = result.Data["Message"];
            Debug.Log("Message :" + result.Data["Message"]);
        }
    }
    

    public void OnDataSend(UpdateUserDataResult result)
    {
        Debug.Log("Success send data");
    }

    void SendLeaderBoard(int score)
    {
        var request = new UpdatePlayerStatisticsRequest
        {
            Statistics = new List<StatisticUpdate> { 
                new StatisticUpdate {
                    StatisticName = "PlatformScore",
                    Value = score 
                } 
            }
        };
        PlayFabClientAPI.UpdatePlayerStatistics(request, OnLeaderBoardUpdate, OnError);
    }

    void OnLeaderBoardUpdate(UpdatePlayerStatisticsResult result)
    {
        Debug.Log("Sucess LeaderBoard Update");
    }

    public void GetLeaderBoard()
    {
        var request = new GetLeaderboardRequest
        {
            StatisticName = "PlatformScore",
            StartPosition = 0,
            MaxResultsCount = 10
        };
        PlayFabClientAPI.GetLeaderboard(request, OnLeaderboardGet, OnError);
        
    }

    public void OnLeaderboardGet(GetLeaderboardResult result)
    {
       
        foreach(var item in result.Leaderboard)
        {
           
            Debug.Log(item.Position + " " + item.PlayFabId + " " + item.StatValue);
        }
    }
}
