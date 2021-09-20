using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiceRoller : MonoBehaviour
{
    // SINGLETON
    private static DiceRoller _instance;

    public static DiceRoller Instance { get { return _instance; } }

    [SerializeField] private GameObject _dicePrefab;

    List<GameObject> _dices = new List<GameObject>();

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }
    }

    public void MoveTheDice(int count, int id, Vector3 pos, Quaternion rot)
    {
        if (_dices.Count != count)
        {
            PrepareDicesForRoll(count, id, pos, rot);
        }

        _dices[id].transform.position = pos;
        _dices[id].transform.rotation = rot;
    }

    private void PrepareDicesForRoll(int count, int id, Vector3 pos, Quaternion rot)
    {
        while (_dices.Count != count)
        {
            if (_dices.Count < count)
            {
                GameObject newDice = Instantiate(_dicePrefab, pos, rot, transform);
                _dices.Add(newDice);
            }
            else if (_dices.Count > count)
            {
                Destroy(_dices[0]);
                _dices.RemoveAt(0);
            }
        }
        
    }
}
