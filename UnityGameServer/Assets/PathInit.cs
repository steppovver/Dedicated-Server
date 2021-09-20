using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathInit : MonoBehaviour
{
    // SINGLETON
    private static PathInit _instance;

    public static PathInit Instance { get { return _instance; } }

    public Step[] PathStep;

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

        PathStep = GetComponentsInChildren<Step>();

        for (int i = 0; i < PathStep.Length; i++)
        {
            PathStep[i].myIndex = i;
        }
    }
}

