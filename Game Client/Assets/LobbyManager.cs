using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LobbyManager : MonoBehaviour
{
    public static LobbyManager instance;

    public Button startGameButton;
    
    [SerializeField] private PlayerCard[] playerCards;

    Dictionary<int, PlayerCard> cardOfPlayerDict = new Dictionary<int, PlayerCard>();

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

    private void Start()
    {
        InitPlayerCards();
    }

    void InitPlayerCards()
    {
        foreach (var _playerId in GameManager.playersOrder)
        {
            AddPlayerCards(_playerId);
        }
    }

    public void AddPlayerCards(int _id)
    {

        if (cardOfPlayerDict.Count == 0)
        {
            if (_id == Client.instance.myId)
            {
                startGameButton.gameObject.SetActive(true);
            }
            else
            {
                startGameButton.gameObject.SetActive(false);
            }
        }

        cardOfPlayerDict.Add(_id, playerCards[cardOfPlayerDict.Count]);
        cardOfPlayerDict[_id].UpdateCard(GameManager.players[_id].username, GameManager.players[_id].isReady);
    }

    public void ResetPlayerCard(int _id)
    {
        foreach (var item in cardOfPlayerDict.Values)
        {
            item.ResetCard();
        }
        cardOfPlayerDict.Clear();

        foreach (var _playerId in GameManager.playersOrder)
        {
            AddPlayerCards(_playerId);
        }
    }

    public void UpdatePlayersPrepare(int _playerId, bool _isReady)
    {
        cardOfPlayerDict[_playerId].UpdateToggle(_isReady);
    }
}
