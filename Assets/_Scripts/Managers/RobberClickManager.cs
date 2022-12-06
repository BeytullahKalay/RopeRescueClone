using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class RobberClickManager : MonoBehaviour
{
    [SerializeField] private List<Transform> robbersOnStart;
    [SerializeField] private List<Transform> robbersSlide;
    [SerializeField] private List<Transform> robbersReached;

    private Tween _tween1;
    private Tween _tween2;

    private void OnEnable()
    {
        EventManager.RobberStartedToSlide += AddRobberSlidingRobberToSlidingList;

        EventManager.RobberReachedToFinish += AddReachedRobberToReachedList;
        EventManager.RobberReachedToFinish += RemoveSlidingRobberFromList;
        EventManager.RobberReachedToFinish += TryChangeColorToConnected;

        EventManager.RemoveFromSlidingList += RemoveSlidingRobberFromList;

        EventManager.CheckIsGameOver += CheckIsGameOver;
    }

    private void OnDisable()
    {
        EventManager.RobberStartedToSlide -= AddRobberSlidingRobberToSlidingList;

        EventManager.RobberReachedToFinish -= AddReachedRobberToReachedList;
        EventManager.RobberReachedToFinish -= RemoveSlidingRobberFromList;
        EventManager.RobberReachedToFinish -= TryChangeColorToConnected;

        EventManager.RemoveFromSlidingList -= RemoveSlidingRobberFromList;

        EventManager.CheckIsGameOver -= CheckIsGameOver;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0) && Enums.Instance.RopeState == Enums.RopeStates.Lock)
        {
            if (robbersOnStart.Count <= 0) return;

            SliderRobber();

            RobberGroupSlide(.5f, .1f);
        }

        if (Input.GetKeyUp(KeyCode.Mouse0))
        {
            _tween1?.Kill();
            _tween2?.Kill();
        }
    }

    private void SliderRobber()
    {
        robbersOnStart[0].GetComponent<Robber>().StartFollowingPath(GameManager.Instance.MovePath.ToArray());

        robbersOnStart.RemoveAt(0);
    }

    private void RobberGroupSlide(float waitSeconds, float waitTimeBetweenSpeedSlides)
    {
        float t = 0;
        float t2 = 0;

        _tween1 = DOTween.To(() => t, x => t = x, 1, waitSeconds).OnComplete(() =>
        {
            _tween2 = DOTween.To(() => t2, x => t2 = x, 1, waitTimeBetweenSpeedSlides).
                SetLoops(-1, LoopType.Restart).OnStepComplete(() =>
                {
                    Debug.Log("HERE!");
                    SliderRobber();
                    t2 = 0;
                });
        });
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

    private void CheckIsGameOver()
    {
        if (Enums.Instance.GameState == Enums.GameStates.Play)
        {
            if (robbersSlide.Count <= 0 && robbersOnStart.Count <= 0)
            {
                Debug.Log("Game Over");
            }
        }
    }
}