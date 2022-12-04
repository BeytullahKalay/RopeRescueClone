
using UnityEngine;

public class HoldAndDrag : MonoBehaviour
{
    [Header("Follow")]
    [SerializeField] private float followSpeed = 4f;
    [SerializeField] private float minDistanceForMove = .1f;
    
    [Header("Connect")]
    [SerializeField] private Transform connectPosition;
    [SerializeField] private float minDistanceForConnection = .5f;
    
    private Vector2 _stopPosition;

    private bool _isHolding;

    private void OnMouseDown()
    {
        _isHolding = true;
        
        EventManager.UnConnected?.Invoke();
    }

    private void OnMouseUp()
    {
        _isHolding = false;

        CheckIsCanConnect();
    }

    private void CheckIsCanConnect()
    {
        if (Vector3.Distance(transform.position, connectPosition.position) < minDistanceForConnection)
        {
            transform.position = connectPosition.position;
            EventManager.Connected?.Invoke();
        }
    }

    private void FixedUpdate()
    {
        if (_isHolding)
        {
            var desPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            desPos.z = 0;

            if(Vector3.Distance(desPos,transform.position) < minDistanceForMove) return;
            var moveDir = (desPos - transform.position).normalized;
            transform.Translate(moveDir * (followSpeed * Time.fixedDeltaTime));
        }
    }
}