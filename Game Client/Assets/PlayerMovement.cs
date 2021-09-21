using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System;

public class PlayerMovement : MonoBehaviour
{
    PathFinder _pathFinder;

    [SerializeField] private float _delayBetweenSteps;
    [SerializeField] private float _animDuration;

    public bool isMoving = false;

    public Player player;


    private void Start()
    {
        player = GetComponent<Player>();

        _pathFinder = new PathFinder();

        StartCoroutine(MoveToVector3(_pathFinder.getVectorByIndex(0)));
    }

    public void StartMoving(int amountSteps, int targetIndex = 0) // targetIndex for Debug 
    {
        isMoving = true;
        StopAllCoroutines();
        StartCoroutine(MovementCoroutine(amountSteps, targetIndex));
    }

    private IEnumerator MovementCoroutine(int amountSteps, int targetIndex) // targetIndex for Debug 
    {
        int endOfTurnIndex = _pathFinder.AddSteps(amountSteps);
        if (amountSteps < 0)
        {
            endOfTurnIndex = targetIndex;
        }

        while (_pathFinder.CurrentStep != endOfTurnIndex)
        {
            Vector3 nextStep = _pathFinder.getNextStepPosition(this);
            yield return StartCoroutine(MoveToNextStep(nextStep));
            yield return new WaitForSeconds(_delayBetweenSteps);
        }


        // when player stoped
        isMoving = false;
    }

    private IEnumerator MoveToNextStep(Vector3 target)
    {
        Vector3 startPosition = transform.position;

        float t = 0;
        float animDuration = _animDuration;

        while (t < 1)
        {
            Vector3 parabolicPos = Parabola(startPosition, target, 1, t);

            transform.position = parabolicPos;

            t += Time.deltaTime / animDuration;
            yield return null;
        }
        transform.position = target;
    }


    public IEnumerator MoveToVector3(Vector3 target)
    {
        Vector3 startPosition = transform.position;

        float t = 0;
        float animDuration = _animDuration;

        while (t < 1)
        {
            Vector3 parabolicPos = Parabola(startPosition, target, 1, t);

            transform.position = parabolicPos;

            t += Time.deltaTime / animDuration;
            yield return null;
        }
        transform.position = target;
    }

    private Vector3 Parabola(Vector3 start, Vector3 end, float height, float t)
    {
        Func<float, float> f = x => -4 * height * x * x + 4 * height * x;

        var mid = Vector3.Lerp(start, end, t);

        return new Vector3(mid.x, f(t) + Mathf.Lerp(start.y, end.y, t), mid.z);
    }
}
