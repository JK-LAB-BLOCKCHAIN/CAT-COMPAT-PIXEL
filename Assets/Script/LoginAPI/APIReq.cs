using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using WebSocketSharp;

public class APIReq : MonoBehaviour
{
    public static APIReq instance = new APIReq();

    public static APIReq Instance
    {
        get
        {
            if (instance == null)
                instance = new APIReq();
            return instance;
        }
    }
    public static string Base64Encode(string plainText)
    {
        var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(plainText);
        return System.Convert.ToBase64String(plainTextBytes);
    }
    public static string Base64Decode(string base64EncodedData)
    {
        var base64EncodedBytes = System.Convert.FromBase64String(base64EncodedData);
        return System.Text.Encoding.UTF8.GetString(base64EncodedBytes);
    }

    public IEnumerator Register(string _json, Action<bool, string> result)
    {
        byte[] bytes = System.Text.Encoding.UTF8.GetBytes(_json);
        string base64 = Convert.ToBase64String(bytes);
        string linkresult = APILinked.linkRegister + base64;
        
        Debug.Log(linkresult);

        using (UnityWebRequest link = UnityWebRequest.Get(linkresult))
        {
            link.SetRequestHeader("Access-Control-Allow-Origin", "*");
            yield return link.SendWebRequest();
            switch (link.result)
            {
                case UnityWebRequest.Result.ConnectionError:
                case UnityWebRequest.Result.DataProcessingError:
                    result?.Invoke(false, link.error);
                    break;
                case UnityWebRequest.Result.ProtocolError:
                    result?.Invoke(false, link.error);
                    break;
                case UnityWebRequest.Result.Success:
                    JSON itemJson = new JSON();
                    itemJson.serialized = link.downloadHandler.text;
                    bool error = itemJson.ToBoolean("error");
                    if (error == false)
                        result?.Invoke(true, itemJson.ToString("message"));
                    else
                        result?.Invoke(false, itemJson.ToString("message"));
                    break;
            }
        };
    }

    public IEnumerator Login(string _json, Action<bool, string> result)
    {
        byte[] bytes = System.Text.Encoding.UTF8.GetBytes(_json);
        string base64 = Convert.ToBase64String(bytes);
        string linkresult = APILinked.linkLogin + base64;
        Debug.Log(linkresult);
        using (UnityWebRequest link = UnityWebRequest.Get(linkresult))
        {
            yield return link.SendWebRequest();
            switch (link.result)
            {
                case UnityWebRequest.Result.ConnectionError:
                case UnityWebRequest.Result.DataProcessingError:
                    result?.Invoke(false, link.error);
                    break;
                case UnityWebRequest.Result.ProtocolError:
                    result?.Invoke(false, link.error);
                    break;
                case UnityWebRequest.Result.Success:
                    Debug.Log(link.downloadHandler.text);
                    JSON itemJson = new JSON();
                    itemJson.serialized = link.downloadHandler.text;
                    bool error = itemJson.ToBoolean("error");

                    string message = itemJson.ToString("message");
                    if (error)
                    {
                        result?.Invoke(false, message);
                        break;
                    }
                    JSON[] allData = itemJson.ToArray<JSON>("data");

                    JSON useridData = allData[0].ToJSON("User");

                    LoginData.GameData = new Login_Data();
                    //(useridData.ToString());
                    ////(useridData.ToString("username"));
                    LoginData.GameData.User = new UserData()
                    {
                        UserID = useridData.ToString("id"),
                        UserName = useridData.ToString("username"),
                        UserCoin = useridData.ToFloat("coin"),
                        UserCash = useridData.ToFloat("cash"),
                        UserAvatar = useridData.ToInt("avatar"),
                        UserChangeNameTicket = useridData.ToInt("changename_ticket"),

                        UserSpinLeft = useridData.ToInt("nbspin"),
                        UserEmail = useridData.ToString("email"),
                        UserKey = useridData.ToString("security"),

                        LastLoginTime = useridData.ToString("last_login_time"),
                    };
                    result?.Invoke(!error, message);

                    break;
            }
        };
    }

}
