using PlayFab.ClientModels;
using PlayFab;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.PackageManager;
using UnityEngine;
using UnityEngine.UI;

public class LogIn : MonoBehaviour
{

    public InputField email;
    public InputField password;

    private static LogIn instance;

    public static LogIn Instance { get => instance; }

    public void Awake()
    {
        LogIn.instance = this;
    }


    public void Login()
    {
        var request = new LoginWithEmailAddressRequest
        {
            Email = this.email.text,
            Password = this.password.text,

        };
        PlayFabClientAPI.LoginWithEmailAddress(request, OnLogin, OnError);

    }
    void OnLogin(LoginResult result)
    {
        Debug.Log("Login Succeess");

    }
    void OnError(PlayFabError error)
    {
        Debug.Log("Error while Login/Create account!");
        Debug.Log(error.GenerateErrorReport());
    }
}
