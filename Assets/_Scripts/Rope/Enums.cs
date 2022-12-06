using UnityEngine;

public class Enums : MonoBehaviour
{
    #region Singleton

    public static Enums Instance;

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

    
    public RopeStates RopeState;
    public enum RopeStates
    {
        Free,
        Lock
    }

    public GameStates GameState;
    public enum GameStates
    {
        Play,
        End
    }


    private void LockRopeState()
    {
        RopeState = RopeStates.Lock;
    }

    private void FreeRopeState()
    {
        RopeState = RopeStates.Free;
    }
}
