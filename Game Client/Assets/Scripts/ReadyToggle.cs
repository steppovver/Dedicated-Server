using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReadyToggle : MonoBehaviour
{
    public void OnChangeToggle()
    {
        ClientSend.PlayerIsReady();
    }
}
