using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHandler : MonoBehaviour
{
    [SerializeField] Material[] playerMaterials;
    [SerializeField] GameObject playerController;
    [SerializeField] GameObject[] playerPrefab;

    List<Player> playersInGame = new List<Player>();

    // SINGLETON
    private static PlayerHandler _instance;

    public static PlayerHandler Instance { get { return _instance; } }


    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }
    }


    private void Start()
    {
        for (int i = 0; i < GameManager.players.Count; i++)
        {
            GameObject newPlayer = Instantiate(playerController, transform.position, Quaternion.identity, transform);

            Player player = newPlayer.GetComponent<Player>();
            player.playerID = GameManager.playersOrder[i];

            playersInGame.Add(player);
        }

        ClientSend.GameIsReady();
    }

    internal void instantiatePlayers()
    {
        for (int i = 0; i < playersInGame.Count; i++)
        {
            GameObject newPrefab = Instantiate(playerPrefab[i], playersInGame[i].transform.position, Quaternion.identity, playersInGame[i].transform);
        }
    }
}
