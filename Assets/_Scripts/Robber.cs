using DG.Tweening;
using UnityEngine;


public class Robber : MonoBehaviour
{
    [SerializeField] private Animator animator;
    private Collider _triggerDetectionCollider;

    private Tween _tween;


    private void Awake()
    {
        _triggerDetectionCollider = GetComponent<Collider>();
    }

    private void Start()
    {
        SetUseGravityStateTo(transform, false);
    }

    public void StartFollowingPath(Vector3[] comingPath)
    {
        _tween = transform.DOPath(comingPath, GameManager.Instance.RobberFollowPathSpeed, PathType.Linear,
                PathMode.Sidescroller2D).SetSpeedBased()
            .OnStart(() => { EventManager.RobberStartedToSlide?.Invoke(transform); })
            .OnComplete(() =>
            {
                EventManager.RobberReachedToFinish?.Invoke(transform);
            });
    }

    private void SetUseGravityStateTo(Transform comingTransform, bool state)
    {
        if (comingTransform.TryGetComponent(out Rigidbody rb))
        {
            rb.useGravity = state;
        }

        foreach (Transform child in comingTransform)
        {
            SetUseGravityStateTo(child, state);
        }
    }

    private void AddFlyForceToAllRigidbodies(Transform comingTransform, Vector3 flyDirection, float force)
    {
        if (comingTransform.TryGetComponent(out Rigidbody rb))
        {
            rb.AddForce(flyDirection * force, ForceMode.Impulse);
        }

        foreach (Transform child in comingTransform)
        {
            AddFlyForceToAllRigidbodies(child, flyDirection, force);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Obstacle"))
        {
            Debug.Log("Obstacle DETECTED!!");

            TriggerActions(other);
            
            EventManager.CheckIsGameOver?.Invoke();
        }
    }

    private void TriggerActions(Collider other)
    {
        // disable detection collider
        _triggerDetectionCollider.enabled = false;

        // open ragdoll
        animator.enabled = false;

        // add fly effect
        var direction = (transform.position - other.transform.position).normalized;
        AddFlyForceToAllRigidbodies(transform, direction, GameManager.Instance.RobberFlyForce);

        // open gravity effect
        SetUseGravityStateTo(transform, true);

        // kill the tween
        _tween?.Kill();
        
        // remove from sliding list
        EventManager.RemoveFromSlidingList?.Invoke(transform);

        // destroy after 2 seconds
        Destroy(gameObject, 2f);
    }
    
    
}