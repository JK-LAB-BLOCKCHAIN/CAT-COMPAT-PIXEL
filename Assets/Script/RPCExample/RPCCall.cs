using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RPCCall : MonoBehaviour
{
    [PunRPC]
    void CallStart(bool isStart)
    {
        //GameController.instance.CallStartGame();
        Debug.Log("Star Game");
    }
}
