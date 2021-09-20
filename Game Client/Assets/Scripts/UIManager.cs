using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;

    public GameObject startMenu;
    public TMP_InputField ipAddress;
    public TMP_InputField userNameField;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Debug.Log("Destroing already exists, destroing object");
            Destroy(this);
        }
    }

    public void ConnectToServer()
    {
        startMenu.SetActive(false);
        Client.instance.ip = ipAddress.text;

        userNameField.interactable = false;
        ipAddress.interactable = false;
        Client.instance.ConnectToServer();
    }
}
