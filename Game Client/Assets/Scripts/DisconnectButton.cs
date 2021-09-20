using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DisconnectButton : MonoBehaviour
{
    public void DisconnectFromServer()
    {
        Client.instance.Disconnect();
        SceneManager.LoadScene(1);
    }
}
