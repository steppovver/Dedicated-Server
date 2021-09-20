using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClientSend : MonoBehaviour
{
    private static void SendTCPData(Packet _packet)
    {
        _packet.WriteLength();
        Client.instance.tcp.SendData(_packet);
    }

    private static void SendUDPData(Packet _packet)
    {
        _packet.WriteLength();
        Client.instance.udp.SendData(_packet);
    }



    public static void WelcomeReceived()
    {
        using (Packet _packet = new Packet((int)ClientPackets.welcomeReceived))
        {
            _packet.Write(Client.instance.myId);
            _packet.Write(UIManager.instance.userNameField.text);

            SendTCPData(_packet);
        }
    }

    public static void PlayerIsReady()
    {
        using (Packet _packet = new Packet((int)ClientPackets.playerIsReady))
        {
            _packet.Write(GameManager.players[Client.instance.myId].isReady);

            SendTCPData(_packet);
        }
    }

    internal static void StartGame(bool _start)
    {
        using (Packet _packet = new Packet((int)ClientPackets.startGame))
        {
            _packet.Write(_start);

            SendTCPData(_packet);
        }
    }

    internal static void RollADice()
    {
        using (Packet _packet = new Packet((int)ClientPackets.RollDices))
        {
            SendTCPData(_packet);
        }
    }
}
