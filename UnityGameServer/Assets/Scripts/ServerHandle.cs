using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public class ServerHandle
{
    public static void WelcomeReceived(int _fromClient, Packet _packet)
    {
        int _clientIdCheck = _packet.ReadInt();
        string _username = _packet.ReadString();

        Debug.Log($"{Server.clients[_fromClient].tcp.socket.Client.RemoteEndPoint} connected succesfully ans is now player {_fromClient} with username = {_username}.");
        if (_fromClient != _clientIdCheck)
        {
            Debug.Log($"player \"{_username}\" (ID: {_fromClient}) has assumed wrong cliend ID ({_clientIdCheck})!");
        }

        Server.clients[_fromClient].SendIntoLobby(_username);
    }

    internal static void PlayerIsReady(int _fromClient, Packet _packet)
    {
        bool _isReady = _packet.ReadBool();

        Server.clients[_fromClient].player.IsReadyForStart = !Server.clients[_fromClient].player.IsReadyForStart;

        ServerSend.SendPlayersPrepared(_fromClient, Server.clients[_fromClient].player.IsReadyForStart);
    }

    internal static void StartGame(int _fromClient, Packet _packet)
    {
        ServerSend.LoadScene(NameOfScene.Game);
    }

    internal static void RollADice(int _fromClient, Packet _packet)
    {
        DiceRoller.Instance.SetUpDicesAndRoll(2);
    }
}
