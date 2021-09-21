using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ClientHandle : MonoBehaviour
{
    public static void Welcome(Packet _packet)
    {
        string _msg = _packet.ReadString();
        int _myId = _packet.ReadInt();

        Debug.Log($"Message from server: {_msg}");
        Client.instance.myId = _myId;
        ClientSend.WelcomeReceived();

        Client.instance.udp.Connect(((IPEndPoint) Client.instance.tcp.socket.Client.LocalEndPoint).Port);
    }

    public static void PlayerDisconnected(Packet _packet)
    {
        int _id = _packet.ReadInt();

        GameManager.players.Remove(_id);
        GameManager.playersOrder.Remove(_id);

        LobbyManager.instance.ResetPlayerCard(_id);
    }

    public static void SendPlayerData(Packet _packet)
    {
        int _id = _packet.ReadInt();
        string _username = _packet.ReadString();
        bool _update = _packet.ReadBool();
        bool _isReady = _packet.ReadBool();
        print($"new user with name {_username} and id {_id}");
        GameManager.players.Add(_id, new PlayerManager(_id, _username, _isReady));
        GameManager.playersOrder.Add(_id);


        if (_update == true)
        {
            print("new");
            LobbyManager.instance.AddPlayerCards(_id);
        }       
    }

    public static void LoadScene(Packet _packet)
    {
        int _sceneIndex = _packet.ReadInt();
        LevelLoader.instance.LoadScene(_sceneIndex);
    }

    internal static void RecievePlayersPrepared(Packet _packet)
    {
        int _id = _packet.ReadInt();
        bool _isReady = _packet.ReadBool();

        GameManager.players[_id].isReady = _isReady;
        LobbyManager.instance.UpdatePlayersPrepare(_id, _isReady);
    }

    internal static void MovingOfDice(Packet _packet)
    {
        int count = _packet.ReadInt();
        int id = _packet.ReadInt();
        Vector3 pos = _packet.ReadVector3();
        Quaternion rot = _packet.ReadQuaternion();

        DiceRoller.Instance.MoveTheDice(count, id, pos, rot);
        print($"Количество фишек {count}, id {id}, position {pos}, rotation {rot}");
    }

    internal static void InstantiatePlayers(Packet _packet)
    {
        PlayerHandler.Instance.instantiatePlayers();
    }
}
