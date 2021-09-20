using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


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

    private void Update()
    {
        for (int i = 0; i < _dices.Count; i++)
        {
            if (_dices[i].GetComponent<Dice>().IsMoving())
            {
                // TODO: Send UDP position of DICE;
                ServerSend.SendDicePosition(_dices.Count, i, _dices[i].transform);
            }
        }
    }

    public void SetUpDicesAndRoll(int numberOfDices)
    {
        StopAllCoroutines();

        DiceInit(numberOfDices);

        RollTheDice();

        StartCoroutine(getDicesCount());
    }



    void DiceInit(int numberOfDices)
    {
        while (_dices.Count != numberOfDices)
        {
            if (_dices.Count < numberOfDices)
            {
                GameObject newDice = Instantiate
                    (
                                                _dicePrefab,
                                                new Vector3(
                                                    transform.position.x + _dices.Count,
                                                    transform.position.y,
                                                    transform.position.z),
                                                Quaternion.identity,
                                                transform
                                                );
                _dices.Add(newDice);
            }
            else if (_dices.Count > numberOfDices)
            {
                Destroy(_dices[0]);
                _dices.RemoveAt(0);
            }
        }
    }

    void RollTheDice()
    {
        int diceDistance = 0;
        foreach (var item in _dices)
        {

            item.transform.position = transform.position + Vector3.forward * diceDistance;
            //item.transform.rotation = Quaternion.Euler(Random.Range(-2, 3) * 90, Random.Range(-2, 3) * 90, Random.Range(-2, 3) * 90);
            item.transform.rotation = Quaternion.Euler(Random.Range(-180f, 180f), Random.Range(-180f, 180f), Random.Range(-180f, 180f));

            Rigidbody rb = item.GetComponent<Rigidbody>();
            Vector3 direction = new Vector3(0, 0, 0) + Vector3.forward * diceDistance - item.transform.position;

            rb.velocity = new Vector3(0, 0, 0);
            rb.AddForce(direction * 2, ForceMode.Impulse);
            rb.AddTorque(new Vector3(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f)).normalized * 10, ForceMode.Impulse);


            diceDistance++;
        }
    }

    IEnumerator getDicesCount()
    {
        yield return new WaitForSeconds(1.0f);

        // wait for dices to stop
        bool isStop = false;
        int stopFrames = 0;
        while (!isStop && stopFrames < 15)
        {
            isStop = IsEveryDiceStopped();
            if (isStop)
            {
                stopFrames++;
            }
            else
            {
                stopFrames = 0;
            }
            yield return null;
        }

        bool IsDouble = false;
        int sumOfDice = CalcualteSumOfDices(out IsDouble);

        if (sumOfDice == -1)
        {
            SetUpDicesAndRoll(_dices.Count);
        }


        print($"Выпало: {sumOfDice}, Пара? {IsDouble}");
    }

    bool IsEveryDiceStopped()
    {
        foreach (var item in _dices)
        {
            if (item.GetComponent<Dice>().IsMoving()) return false;
        }
        return true;
    }

    int CalcualteSumOfDices(out bool doubl)
    {
        doubl = false;
        int countDices = 0;
        foreach (var item in _dices)
        {
            int currentDiceNumber = item.GetComponent<Dice>().GetDiceCount();
            if (currentDiceNumber == -1)
            {
                return -1;
            }
            if (_dices.Count == 2 && currentDiceNumber == countDices)
            {
                doubl = true;
            }
            countDices += currentDiceNumber;
        }
        print(countDices);

        return countDices;
    }
}
