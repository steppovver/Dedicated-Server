using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PathFinder
{

    Step[] PathStep;

    public int CurrentStep { get; private set; }

    public PathFinder()
    {
        PathStep = PathInit.Instance.PathStep;
        CurrentStep = 0;
    }

    public Vector3 getVectorByIndex(int index)
    {
        index = index % PathStep.Length;
        return PathStep[index].transform.position;
    }

    public Vector3 getNextStepPosition(PlayerMovement playerMovement)
    {
        CurrentStep++;
        CurrentStep = CurrentStep % PathStep.Length;

        Step nextStep = PathStep[CurrentStep];
        return nextStep.transform.position;
    }

    public int AddSteps(int amountSteps)
    {
        int targetStepIndex = CurrentStep + amountSteps;
        targetStepIndex = targetStepIndex % PathStep.Length;
        return targetStepIndex;
    }
}
