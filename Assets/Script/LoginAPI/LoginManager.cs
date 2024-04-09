using Newtonsoft.Json;
using Photon.Pun;
using Photon.Realtime;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows;
using TMPro;
using UnityEngine.UI;

public class LoginManager : MonoBehaviour
{
    public enum LoginMode
    {
        Guest,
        RegistedAccount
    }
    public class RegisterForm
    {
        public string username;
        public string password;
        public string phone { get; set; }
        public string email { get; set; }
    }

    public class LoginForm
    {
        public string username;
        public string password;
        public string uniqueID;
        public int loginMode; // 0 = Normal , 1 = QuickGuest
        public string Platform;
        public string version;
    }

    public static LoginMode loginMode;
    private void Start()
    {
        //Test
        //Invoke("OnRegister",2);
        //Invoke("OnClickStartAsRegister",1);

        //Invoke("TestCallRPC",5);
    }
    void TestCallRPC()
    {
        var view = FindAnyObjectByType<RPCCall>().GetComponent<PhotonView>() ;
        view.RPC("CallStart", RpcTarget.All, true);
    }

    [SerializeField] TMP_InputField userRegister;
    [SerializeField] TMP_InputField passRegister;
    public void OnRegister()
    {
        if (string.IsNullOrWhiteSpace(userRegister.text))
        {
            Debug.LogError("Tên Đăng Nhập Không Được Để Trống");
            return;
        }
        else
        {
            if (userRegister.text.Length < 6)
            {
                Debug.Log("Tên Đăng Nhập Quá Ngắn");
                return;
            }
        }
        if (string.IsNullOrWhiteSpace(passRegister.text))
        {
            Debug.LogError("Mật Khẩu Không Được Để Trống");
            return;
        }

        var register = new RegisterForm() { username = userRegister.text, password = passRegister.text };

        StartCoroutine(APIReq.Instance.Register(JsonConvert.SerializeObject(register), (aSucess, aJson) => {
            //UIManager.instance.CALLLOADING(false);
            //OnCloseRegis();
            if (aSucess == false)
            {
                Debug.LogError(aJson);
            }
            else
            {
                //UIManager.instance.PopupMessage.CallAlert("Đăng kí thành công !!!");
                Debug.LogError("Đăng kí thành công !!!");
                MenuManager.Instance.OpenMenu("title");

            }
        }));
        /* if (string.IsNullOrWhiteSpace(Regis_PassRepeat.value))
         {
             txt_Notice.text = "Vui Lòng Nhập Lại Mật Khẩu";
             return;
         }
         if (Regis_Pass.value != Regis_PassRepeat.value)
         {
             txt_Notice.text = "Mật Khẩu Không Trùng Khớp";
             return;
         }*/

        var regisform = new RegisterForm()
        {
            username = "ngocnhan",
            password = "123456",
            email = "",
            phone = ""
        };
        //Send 
        StartCoroutine(APIReq.Instance.Register(JsonConvert.SerializeObject(regisform), (aSucess, aJson) => {
            //UIManager.instance.CALLLOADING(false);
            //OnCloseRegis();
            if (aSucess == false)
            {
                Debug.LogError("Fasle: "+aJson);
            }
            else
            {
                //UIManager.instance.PopupMessage.CallAlert("Đăng kí thành công !!!");
                Debug.LogError("Đăng kí thành công !!!");
            }
        }));
    }

    [SerializeField] TMP_InputField userLogin;
    [SerializeField] TMP_InputField passLogin;
    public void OnClickStartAsRegister()
    {
        loginMode = LoginMode.RegistedAccount;
        
        if (string.IsNullOrWhiteSpace(userLogin.text) || string.IsNullOrWhiteSpace(passLogin.text))
        {
            Debug.Log("Vui lòng nhập Id và mật khẩu");
            return;
        }
        
        //CacheLogin(loginMode);
        //UIManager.instance.CALLLOADING(true);
        var loginF = new LoginForm() { username = userLogin.text, password = passLogin.text, loginMode = 0, Platform = "Android", version = "1.1" };

        StartCoroutine(APIReq.Instance.Login(JsonConvert.SerializeObject(loginF), (isSuccess, aMessage) => {
           // UIManager.instance.CALLLOADING(false);
            if (isSuccess == false)
            {
                //UIManager.instance.PopupMessage.CallAlert(aMessage);
                //pnl_Load.SetActive(false);
                Debug.Log("Faild");
                
            }
            else
            {
                Debug.LogError(aMessage);
                //pnl_Load.SetActive(true);

                //Login Success ==> connect to Photon
                MenuManager.Instance.OpenMenu("title");
                Debug.Log("Login Success");
                /*
                if (!PhotonNetwork.IsConnected)
                    CallConnectToPhoton();
                else
                {
                    CallConnectToMQTT();
                }
                */

                Debug.Log(Newtonsoft.Json.JsonConvert.SerializeObject(LoginData.GameData.User));
            }
        }));
    }
}
