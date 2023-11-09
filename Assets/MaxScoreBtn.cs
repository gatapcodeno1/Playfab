using PlayFab.ClientModels;
using PlayFab;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MaxScoreBtn : MonoBehaviour
{
    public InputField yourScore;
    public InputField maxScore;

    private static MaxScoreBtn instance;

    public static MaxScoreBtn Instance { get => instance; }

    public void Awake()
    {
        MaxScoreBtn.instance = this;

    }


    public void SendLeaderBoard()
    {
        
        var request = new UpdatePlayerStatisticsRequest
        {
            Statistics = new List<StatisticUpdate> {
                new StatisticUpdate
                {
                    StatisticName = "PlatformScore",
                    Value = int.Parse(this.yourScore.text)

                }
            }
        };
        PlayFabClientAPI.UpdatePlayerStatistics(request, OnSendLeaderBoard, OnError);
        

    }



    public void GetLeaderBoard()
    {
        var request = new GetLeaderboardRequest
        {
            StatisticName = "PlatformScore",
            StartPosition = 0,
            MaxResultsCount = 10,

        };
        PlayFabClientAPI.GetLeaderboard(request, OnGetLeaderBoard, OnError);
    }

    void OnGetLeaderBoard(GetLeaderboardResult result)
    {
        Debug.Log("Get LeaderBoard Success");
        foreach (var item in result.Leaderboard)
        {
            maxScore.text = Mathf.Max(int.Parse(maxScore.text), int.Parse(item.StatValue.ToString())).ToString();
        }
    }

    void OnSendLeaderBoard(UpdatePlayerStatisticsResult result)
    {
        Debug.Log("Send LeaderBoard Success");
        
    }
    void OnError(PlayFabError error)
    {
        Debug.Log("Error while Login/Create account!");
        Debug.Log(error.GenerateErrorReport());
    }
}
