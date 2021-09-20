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

    }
}
