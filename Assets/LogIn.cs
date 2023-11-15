using PlayFab.ClientModels;
using PlayFab;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

public class LogIn : MonoBehaviour
{

    public InputField email;
    public InputField password;
    public InputField nameInput;
    public InputField newName;
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
            InfoRequestParameters = new GetPlayerCombinedInfoRequestParams
            {
                GetPlayerProfile = true,

            }

        };
        PlayFabClientAPI.LoginWithEmailAddress(request, OnLogin, OnError);

    }
    void OnLogin(LoginResult result)
    {
        Debug.Log("Login Succeess");
        string name = null;
        if(result.InfoResultPayload.PlayerProfile != null)
            name = result.InfoResultPayload.PlayerProfile.DisplayName;
        
        if(name == null)
        {
            SubmitNameButton();
        }
        nameInput.text = name;
            
    }


    public void SubmitNameButton()
    {
        var request = new UpdateUserTitleDisplayNameRequest
        {
            DisplayName = newName.text,
        };
        PlayFabClientAPI.UpdateUserTitleDisplayName(request, OnDisplayNameUpdate, OnError);
    }

    void OnDisplayNameUpdate(UpdateUserTitleDisplayNameResult result)
    {
        Debug.Log("Update display name");
        nameInput.text = name;
    }

    void OnError(PlayFabError error)
    {
        Debug.Log("Error while Login/Create account!");
        Debug.Log(error.GenerateErrorReport());
    }
}
