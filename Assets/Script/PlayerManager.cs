using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using System.Linq;
using System.IO;
using Hashtable = ExitGames.Client.Photon.Hashtable;

public class PlayerManager : MonoBehaviour
{
    PhotonView PV;
    private Vector3 PortBluePos = new Vector3(4, 0, 0);
    private Vector3 PortRedPos = new Vector3(27.8f, 0, 0);

    GameObject controller;

    int kills;
    int deaths;

    void Awake()
    {
        PV = GetComponent<PhotonView>();
    }

    void Start()
    {
        if (PV.IsMine)
        {
            CreateController();
        }
    }

    void CreateController()
    {
        int ran = Random.Range(1, 2);

        switch (ran)
        {
            case 1: controller = PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "PlayerController"), PortBluePos, Quaternion.identity);
                break;
                case 2:
                controller = PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "PlayerController"), PortRedPos, Quaternion.identity);
                break;
        }
       // Transform spawnpoint = SpawnManager.Instance.GetSpawnpoint();
       
            //spawnpoint.position, spawnpoint.rotation, 0, new object[] { PV.ViewID });
    }

    public void Die()
    {
        PhotonNetwork.Destroy(controller);
        CreateController();

        deaths++;

        Hashtable hash = new Hashtable();
        hash.Add("deaths", deaths);
        PhotonNetwork.LocalPlayer.SetCustomProperties(hash);
    }

    public void GetKill()
    {
        PV.RPC(nameof(RPC_GetKill), PV.Owner);
    }

    [PunRPC]
    void RPC_GetKill()
    {
        kills++;

        Hashtable hash = new Hashtable();
        hash.Add("kills", kills);
        PhotonNetwork.LocalPlayer.SetCustomProperties(hash);
    }

    public static PlayerManager Find(Player player)
    {
        return FindObjectsOfType<PlayerManager>().SingleOrDefault(x => x.PV.Owner == player);
    }
}