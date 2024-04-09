using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class AccountManager : MonoBehaviour
{
    [SerializeField] TMP_InputField usernameInput;
    [SerializeField] TMP_InputField passwordInput;

    void Start()
    {
        if (PlayerPrefs.HasKey("username") && PlayerPrefs.HasKey("password"))
        {
            usernameInput.text = PlayerPrefs.GetString("username");
            PhotonNetwork.NickName = PlayerPrefs.GetString("username");

            passwordInput.text = PlayerPrefs.GetString("password");
        }
        else
        {
            usernameInput.text = "Player " + Random.Range(0, 10000).ToString("0000");
            OnUsernameInputValueChanged();
        }
    }

    public void OnUsernameInputValueChanged()
    {
        PhotonNetwork.NickName = usernameInput.text;
        PlayerPrefs.SetString("username", usernameInput.text);
    }
}