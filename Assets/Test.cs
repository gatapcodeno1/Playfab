using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayFab;
using PlayFab.ClientModels;
using UnityEngine.UI;
using TMPro;
using Newtonsoft.Json;

public class Test : MonoBehaviour
{

    public string messageText ;
    public string emailInput = "kyo.dat.01@gmail.com";
    public string password = "123456";
  

    private void Start()
    {

        
        //ResetPassword();
        
        
    }

    public void Exectute()
    {

        var request = new ExecuteCloudScriptRequest
        {
            FunctionName = "hello",
            FunctionParameter = new
            {
                name = "Dat"
            }
        };
        PlayFabClientAPI.ExecuteCloudScript(request, OnExecute, OnError);

    }

    void OnExecute(ExecuteCloudScriptResult result)
    {
        //testnow.Instance.Text.text = result.FunctionResult.ToString();
    }


    

    

    

    void OnSuccess(RegisterPlayFabUserResult result)
    {

        Debug.Log("Successful Login/Account Create! ");
        
    }

    void OnError(PlayFabError error)
    {
        Debug.Log("Error while Login/Create account!");
        Debug.Log(error.GenerateErrorReport());
    }


   

    


}
