using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationController : MonoBehaviour
{
    LevelLoader LevelLoader;
    bool IsAnimEnded;

    private void Start()
    {
        LevelLoader = GetComponentInParent<LevelLoader>();
    }

    public void OnEndAnimationEvent()
    {
        LevelLoader.IsAnimEnded = true;
    }
}
