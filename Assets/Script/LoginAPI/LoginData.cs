
using System;
using System.Collections.Generic;

using System.Globalization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
//using CodeBeautify;
using UnityEngine;
//using HDGameData;
//using CodeStage.AntiCheat.ObscuredTypes;
public class LoginData
{
    public static Login_Data GameData = new Login_Data();
    public static SharingUserData GetSharingData()
    {
        return new SharingUserData()
        {
            UserID = GameData.User.UserID,
            UserName = GameData.User.UserName,
            UserCoin = GameData.User.UserCoin,
            UserCash = GameData.User.UserCash,
            UserAvatar = GameData.User.UserAvatar,
            UserChangeNameTicket = GameData.User.UserChangeNameTicket,
            UserEXP = GameData.User.UserEXP,
            UserSpinLeft = GameData.User.UserSpinLeft,
            UserLanguage = GameData.User.UserLanguage,
            UserEmail = GameData.User.UserEmail,
            UserPhone = GameData.User.UserPhone,
            UserLevel = GameData.User.UserLevel,
            LastLoginTime = GameData.User.LastLoginTime,
            UserKey = GameData.User.UserKey,
        };
    }
}

public partial class Login_Data
{
    public UserData User { get; set; }
}

public class UserData
{
    public string UserID { get; set; }
    public string UserName { get; set; }
    public float UserCoin { get; set; }
    public float UserCash { get; set; }
    public int UserAvatar { get; set; }
    public int UserChangeNameTicket { get; set; }
    public string UserEXP { get; set; }
    public int UserSpinLeft { get; set; }
    public string UserLanguage { get; set; }
    public string UserPlayingRoom { get; set; }

    public string UserEmail { get; set; }
    public string UserPhone { get; set; }
    public int UserLevel { get; set; }

    public string LastLoginTime { get; set; }
    public string UserKey { get; set; }

}
public class SharingUserData
{
    public string UserID { get; set; }
    public string UserName { get; set; }
    public float UserCoin { get; set; }
    public float UserCash { get; set; }
    public int UserAvatar { get; set; }
    public int UserChangeNameTicket { get; set; }
    public string UserEXP { get; set; }
    public int UserSpinLeft { get; set; }
    public string UserLanguage { get; set; }
    public string UserEmail { get; set; }
    public string UserPhone { get; set; }
    public int UserLevel { get; set; }
    public float UserPerFish { get; set; }
    public float UserPerBoss { get; set; }
    public int UserAds { get; set; }
    public string LastLoginTime { get; set; }
    public string UserKey { get; set; }
}









