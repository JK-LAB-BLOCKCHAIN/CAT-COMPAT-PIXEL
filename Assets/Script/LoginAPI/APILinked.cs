using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class APILinked : MonoBehaviour
{
    public static string Linksv = "https://menhweb.info.vn";//"https://coupdo.com/chess";
    public static string LinkAccount = "https://menhweb.info.vn";//"https://coupdo.com/account";
    public static string linkRegister
    {
        get
        {
            return LinkAccount + "/registeraccount/";
        }
    }
    public static string linkLogin
    {
        get
        {
            return LinkAccount + "/login/";
        }
    }
}
