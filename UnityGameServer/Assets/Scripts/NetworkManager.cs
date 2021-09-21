using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NetworkManager : MonoBehaviour
{
    public static NetworkManager instance;

    public GameObject playerPrefab;

    [SerializeField] private int maxPlayers = 4;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Debug.Log("Destroing already exists, destroing object");
            Destroy(this);
        }
    }

    private void Start()
    {
        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = 64;

        Server.Start(maxPlayers, 27089);
    }

    private void OnApplicationQuit()
    {
        Server.Stop();
    }

    public Player InstantiatePlayer()
    {
        return Instantiate(playerPrefab, Vector3.zero, Quaternion.identity).GetComponent<Player>();
    }

    public void GameIsReady(int _fromClient)
    {
        Server.clients[_fromClient].player.IsGameReady = true;

        for (int i = 0; i < Server.clientOrder.Count; i++)
        {
            Debug.Log($"{Server.clientOrder[i]}{Server.clients[Server.clientOrder[i]].player.IsGameReady}");
            if (Server.clients[Server.clientOrder[i]].player.IsGameReady == false)
            {
                print("sosi");
                return;
            }
        }

        print("вызываю инстантиэйт");

        ServerSend.InstantiatePlayers();
    }
}
