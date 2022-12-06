using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class SortPosition : MonoBehaviour
{
    [SerializeField] private float verticalSpaceBetweenRobbers = .4f;
    [SerializeField] private List<Transform> sortPositions = new List<Transform>();

    private int sortNum;
    private int sortPositionAmount;

    private void Awake()
    {
        sortPositionAmount = sortPositions.Count;
    }

    private void OnEnable()
    {
        EventManager.SortRobber += SortRobber;
    }

    private void OnDisable()
    {
        EventManager.SortRobber -= SortRobber;
    }

    private void SortRobber(Transform robberTransform)
    {
        List<Vector3> path = new List<Vector3>();

        path.Add(robberTransform.position);

        var position = sortPositions[sortNum % sortPositionAmount].position;
        position.y += (sortNum / sortPositionAmount) * verticalSpaceBetweenRobbers;

        path.Add(position);

        robberTransform.DOPath(path.ToArray(), GameManager.Instance.RobberFollowPathSpeed, PathType.Linear,
            PathMode.Sidescroller2D).SetSpeedBased();

        sortNum++;
    }
}