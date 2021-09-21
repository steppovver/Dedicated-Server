using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public int id;
    public string username;
    public bool IsReadyForStart = false;
    public bool IsGameReady = false;

    private void Start()
    {
        IsGameReady = false;
        IsReadyForStart = false;
    }

    public void Initialize(int _id, string _username)
    {
        id = _id;
        username = _username;
    }
}
