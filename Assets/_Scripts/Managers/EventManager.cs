using System;
using UnityEngine;

public static class EventManager
{
    public static Action Connected;
    public static Action UnConnected;
    public static Action CheckIsGameOver;


    public static Action<Transform> RobberStartedToSlide;
    public static Action<Transform> RobberReachedToFinish;
    public static Action<Transform> RemoveFromSlidingList;


    public static Action<int> TryChangeColorToConnectedColor;

}
