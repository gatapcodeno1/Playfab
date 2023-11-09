using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using PlayFab;
using PlayFab.ClientModels;

public class ResetPasswordBtn : MonoBehaviour
{
    public InputField email;


    public void ResetPassword()
    {
        var request = new SendAccountRecoveryEmailRequest
        {
            Email = this.email.text,
            TitleId = "11984"

        };

        PlayFabClientAPI.SendAccountRecoveryEmail(request, OnResetPassword, OnError);
    }


    void OnResetPassword(SendAccountRecoveryEmailResult result)
    {
        Debug.Log("Reset Succeess");

    }
    void OnError(PlayFabError error)
    {
        Debug.Log("Error while Login/Create account!");
        Debug.Log(error.GenerateErrorReport());
    }

}
