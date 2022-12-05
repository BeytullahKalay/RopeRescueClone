using UnityEngine;

public class RopeStates : MonoBehaviour
{
    #region Singleton

    public static RopeStates Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    #endregion

    private void OnEnable()
    {
        EventManager.Connected += LockRopeState;
        EventManager.UnConnected += FreeRopeState;
    }

    private void OnDisable()
    {
        EventManager.Connected -= LockRopeState;
        EventManager.UnConnected -= FreeRopeState;
    }

    public enum RopeState
    {
        Free,
        Lock
    }

    public RopeState State;

    private void LockRopeState()
    {
        State = RopeState.Lock;
    }

    private void FreeRopeState()
    {
        State = RopeState.Free;
    }
}
