using UnityEngine;
using DG.Tweening;

public class EndConnectionEffect : MonoBehaviour
{
    [SerializeField] private GameObject circleGameObject;
    [SerializeField] private float circleAnimationSpeed = 2f;

    private Collider _collider;

    private Tween _tween;


    private void Awake()
    {
        _collider = GetComponent<Collider>();
    }

    private void OnEnable()
    {
        EventManager.Connected += DisableCollider;
        EventManager.Connected += KillAnimation;
        EventManager.UnConnected += EnableCollider;
    }

    private void OnDisable()
    {
        EventManager.Connected -= DisableCollider;
        EventManager.Connected -= KillAnimation;
        EventManager.UnConnected -= EnableCollider;
    }


    private void Start()
    {
        circleGameObject.transform.localScale = Vector3.zero;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            StartAnimation();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            StopAnimation();
        }
    }

    private void StartAnimation()
    {
        _tween?.Kill();
        circleGameObject.transform.localScale = Vector3.one * .5f;
        _tween = circleGameObject.transform.DOScale(Vector3.one, circleAnimationSpeed).SetLoops(-1,LoopType.Yoyo).SetEase(Ease.InOutCirc).SetSpeedBased();
    }
    
    private void StopAnimation()
    {
        _tween?.Kill();
        _tween = circleGameObject.transform.DOScale(Vector3.zero, circleAnimationSpeed).SetEase(Ease.InQuint).SetSpeedBased();
    }

    private void DisableCollider()
    {
        _collider.enabled = false;
    }

    private void KillAnimation()
    {
        _tween?.Kill();
        _tween = circleGameObject.transform.DOScale(Vector3.zero, circleAnimationSpeed).SetEase(Ease.InQuint).SetSpeedBased();
    }

    private void EnableCollider()
    {
        _collider.enabled = true;
    }
}
