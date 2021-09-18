using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ServerSend
{
    #region Send
    private static void SendTCPData(int _toClient, Packet _packet)
    {
        _packet.WriteLength();
        Server.clients[_toClient].tcp.SendData(_packet);
    }

    private static void SendUDPData(int _toClient, Packet _packet)
    {
        _packet.WriteLength();
        Server.clients[_toClient].udp.SendData(_packet);
    }

    private static void SendTCPDataToAll(Packet _packet)
    {
        _packet.WriteLength();
        for (int i = 1; i <= Server.MaxPlayers; i++)
        {
            Server.clients[i].tcp.SendData(_packet);
        }
    }

    private static void SendTCPDataToAll(int _exceptClient, Packet _packet)
    {
        _packet.WriteLength();
        for (int i = 1; i <= Server.MaxPlayers; i++)
        {
            if (i != _exceptClient)
            {
                Server.clients[i].tcp.SendData(_packet);
            }
        }
    }


    private static void SendUDPDataToAll(Packet _packet)
    {
        _packet.WriteLength();
        for (int i = 1; i <= Server.MaxPlayers; i++)
        {
            Server.clients[i].udp.SendData(_packet);
        }
    }

    private static void SendUDPDataToAll(int _exceptClient, Packet _packet)
    {
        _packet.WriteLength();
        for (int i = 1; i <= Server.MaxPlayers; i++)
        {
            if (i != _exceptClient)
            {
                Server.clients[i].udp.SendData(_packet);
            }
        }
    }
    #endregion


    #region Packets
    public static void Welcome(int _toClient, string _msg)
    {
        using (Packet _packet = new Packet((int)ServerPackets.welcome))
        {
            _packet.Write(_msg);
            _packet.Write(_toClient);

            SendTCPData(_toClient, _packet);
        }
    }

    public static void PlayerDisconnected(int _playerId)
    {
        using (Packet _packet = new Packet((int)ServerPackets.playerDisconnected))
        {
            _packet.Write(_playerId);

            SendTCPDataToAll(_packet);
        }
    }

    public static void LoadScene(int _playerId, NameOfScene _scene)
    {
        using (Packet _packet = new Packet((int)ServerPackets.loadScene))
        {
            _packet.Write((int)_scene);

            SendTCPData(_playerId, _packet);
        }
    }

    public static void LoadScene(NameOfScene _scene)
    {
        using (Packet _packet = new Packet((int)ServerPackets.loadScene))
        {
            _packet.Write((int)_scene);

            SendTCPDataToAll(_packet);
        }
    }

    public static void SendPlayerData(int _playerId, Player _player, bool _updateData = false)
    {
        using (Packet _packet = new Packet((int)ServerPackets.sendPlayerData))
        {
            _packet.Write(_player.id);
            _packet.Write(_player.username);
            _packet.Write(_updateData);
            _packet.Write(Server.clients[_player.id].player.IsReadyForStart);

            SendTCPData(_playerId, _packet);
        }
    }

    internal static void SendPlayersPrepared(int _playerId, bool _isReady)
    {
        using (Packet _packet = new Packet((int)ServerPackets.sendPlayersPrepared))
        {
            _packet.Write(_playerId);
            _packet.Write(_isReady);

            SendTCPDataToAll(_packet);
        }
    }
    #endregion
}
