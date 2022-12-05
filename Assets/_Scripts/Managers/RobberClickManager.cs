using System;
using System.Collections.Generic;
using UnityEngine;

public class RobberClickManager : MonoBehaviour
{
    [SerializeField] private List<Transform> robbersOnStart;
    [SerializeField] private List<Transform> robbersReached;

    private void OnEnable()
    {
        EventManager.RobberReachedToFinish += AddReachedRobberToReachedList;
    }

    private void OnDisable()
    {
        EventManager.RobberReachedToFinish -= AddReachedRobberToReachedList;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0) && RopeStates.Instance.State == RopeStates.RopeState.Lock)
        {
            if(robbersOnStart.Count <= 0) return;
            
            robbersOnStart[0].GetComponent<Robber>().StartFollowingPath(GameManager.Instance.MovePath.ToArray());
            robbersOnStart.RemoveAt(0);
        }
    }

    private void AddReachedRobberToReachedList(Transform robber)
    {
        robbersReached.Add(robber);
    }
}
