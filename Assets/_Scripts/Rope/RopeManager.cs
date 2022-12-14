using System.Collections.Generic;
using UnityEngine;

public class RopeManager : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private Transform startPositionTransform;

    [SerializeField] private LineRenderer rope;
    [SerializeField] private LayerMask collMask;

    [SerializeField] private List<Vector3> _ropePositions = new List<Vector3>();

    private void Awake()
    {
        AddPosToRope(startPositionTransform.position);
    }

    private void OnEnable()
    {
        EventManager.Connected += UpdateMovePathOnGameManager;
    }

    private void OnDisable()
    {
        EventManager.Connected -= UpdateMovePathOnGameManager;
    }

    private void Update()
    {
        UpdateRopePositions();
        LastSegmentGoToPlayerPos();
    }

    private void FixedUpdate()
    {
        DetectCollisionEnter();
        if (_ropePositions.Count > 2) DetectCollisionExits();
    }

    private void DetectCollisionEnter()
    {
        if (Physics.Linecast(player.position, rope.GetPosition(_ropePositions.Count - 2), out var hit, collMask))
        {
            if (hit.point == _ropePositions[_ropePositions.Count - 2]) return;

            _ropePositions.RemoveAt(_ropePositions.Count - 1);

            var awayFromColliderAmount = (hit.point - hit.collider.gameObject.transform.position).normalized * .05f;
            
            AddPosToRope(hit.point + awayFromColliderAmount);
        }
    }

    private void DetectCollisionExits()
    {
        RaycastHit hit;
        if (!Physics.Linecast(player.position, rope.GetPosition(_ropePositions.Count - 3), out hit, collMask))
        {
            _ropePositions.RemoveAt(_ropePositions.Count - 2);
        }
    }

    private void AddPosToRope(Vector3 _pos)
    {
        _ropePositions.Add(_pos);
        _ropePositions.Add(player.position); //Always the last pos must be the player
    }

    private void UpdateRopePositions()
    {
        rope.positionCount = _ropePositions.Count;
        rope.SetPositions(_ropePositions.ToArray());
    }

    private void LastSegmentGoToPlayerPos()
    {
        var dir = (_ropePositions[^2] - player.position).normalized;

        var moveAmount = .2f;
        
        rope.SetPosition(rope.positionCount - 1, player.position + dir * moveAmount);
    }

    private void UpdateMovePathOnGameManager()
    {
        List<Vector3> lineRendererPositions = new List<Vector3>();

        for (int i = 0; i < rope.positionCount; i++)
        {
            lineRendererPositions.Add(rope.GetPosition(i));
        }

        GameManager.Instance.MovePath = new List<Vector3>(lineRendererPositions);
    }
}