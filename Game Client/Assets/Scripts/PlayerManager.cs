using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager
{
    public int id;
    public string username;
    public bool isReady = false;

    public PlayerManager(int id, string username, bool isReady)
    {
        this.id = id;
        this.username = username;
        this.isReady = isReady;
    }
}
