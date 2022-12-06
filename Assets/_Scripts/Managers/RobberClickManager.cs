using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.ShaderGraph.Internal;
using UnityEngine;

public class RobberClickManager : MonoBehaviour
{
    [SerializeField] private List<Transform> robbersOnStart;
    [SerializeField] private List<Transform> robbersSlide;
    [SerializeField] private List<Transform> robbersReached;

    private IEnumerator _routine;

    private void OnEnable()
    {
        EventManager.RobberStartedToSlide += AddRobberSlidingRobberToSlidingList;
        EventManager.RobberReachedToFinish += AddReachedRobberToReachedList;
        EventManager.RobberReachedToFinish += RemoveSlidingRobberFromList;
        EventManager.RobberReachedToFinish += TryChangeColorToConnected;
    }

    private void OnDisable()
    {
        EventManager.RobberStartedToSlide -= AddRobberSlidingRobberToSlidingList;
        EventManager.RobberReachedToFinish -= AddReachedRobberToReachedList;
        EventManager.RobberReachedToFinish -= RemoveSlidingRobberFromList;
        EventManager.RobberReachedToFinish -= TryChangeColorToConnected;
    }

    private void Awake()
    {
        _routine = RobberGroupSlide(.55f);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0) && RopeStates.Instance.State == RopeStates.RopeState.Lock)
        {
            if (robbersOnStart.Count <= 0) return;

            SliderRobber();

            StartCoroutine(_routine);
        }

        if (Input.GetKeyUp(KeyCode.Mouse0))
        {
            StopCoroutine(_routine);
        }
        
    }

    private void SliderRobber()
    {
        robbersOnStart[0].GetComponent<Robber>().StartFollowingPath(GameManager.Instance.MovePath.ToArray());

        robbersOnStart.RemoveAt(0);
    }

    private IEnumerator RobberGroupSlide(float waitSeconds)
    {
        float t = 0;
        
        while (t <= waitSeconds)
        {
            t += Time.deltaTime;
            yield return null;
        }

        while (t >= waitSeconds && robbersOnStart.Count > 0)
        {
            SliderRobber();
        }
    }

    private void AddReachedRobberToReachedList(Transform robber)
    {
        robbersReached.Add(robber);
    }

    private void RemoveSlidingRobberFromList(Transform robber)
    {
        robbersSlide.Remove(robber);
    }

    private void AddRobberSlidingRobberToSlidingList(Transform robber)
    {
        robbersSlide.Add(robber);
    }

    private void TryChangeColorToConnected(Transform robber)
    {
        EventManager.TryChangeColorToConnectedColor?.Invoke(robbersSlide.Count);
    }
}