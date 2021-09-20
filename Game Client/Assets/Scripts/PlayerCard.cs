using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerCard : MonoBehaviour
{
    [Header("cards")]
    [SerializeField] private GameObject waitingForPlayer;
    [SerializeField] private GameObject playerCard;

    [Header("toggle")]
    [SerializeField] private Toggle toggle;

    [Header("text")]
    [SerializeField] private TMP_Text username;


    public void UpdateCard(string _playerName, bool _isReady)
    {
        waitingForPlayer.SetActive(false);
        playerCard.SetActive(true);

        username.text = _playerName;
        UpdateToggle(_isReady);
    }

    public void ResetCard()
    {
        if (waitingForPlayer != null)
        {
            waitingForPlayer.SetActive(true);
        }
        if (playerCard !=null)
        {
            playerCard.SetActive(false);
        }
    }

    public void UpdateToggle(bool _isReady)
    {
        toggle.isOn = _isReady;

        if (GameManager.players.Keys.Count < 2)
        {
            LobbyManager.instance.startGameButton.interactable = false;
            return;
        }

        foreach (var player in GameManager.players.Values)
        {
            if (player.isReady == false)
            {
                LobbyManager.instance.startGameButton.interactable = false;
                return;
            }
        }

        LobbyManager.instance.startGameButton.interactable = true;
    }
}
