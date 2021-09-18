using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public int id;
    public string username;
    public bool IsReadyForStart;
    
    private void Start()
    {
    }

    public void Initialize(int _id, string _username)
    {
        id = _id;
        username = _username;
    }
}
