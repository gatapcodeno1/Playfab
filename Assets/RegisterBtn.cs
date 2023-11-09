using PlayFab.ClientModels;
using PlayFab;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RegisterBtn : MonoBehaviour
{
    public InputField email;
    public InputField password;

    private static RegisterBtn instance;

    public static RegisterBtn Instance { get => instance; }

    public void Awake()
    {
        RegisterBtn.instance = this;
        
    }


    public void Register()
    {
        var request = new RegisterPlayFabUserRequest
        {
            Email = this.email.text,
            Password = this.password.text,
            RequireBothUsernameAndEmail = false,
        };
        PlayFabClientAPI.RegisterPlayFabUser(request, OnRegister, OnError);
        Debug.Log(this.email.text + " " + this.password.text);

    }
    void OnRegister(RegisterPlayFabUserResult result)
    {
        Debug.Log("Login Succeess");

    }
    void OnError(PlayFabError error)
    {
        Debug.Log("Error while Login/Create account!");
        Debug.Log(error.GenerateErrorReport());
    }
}
