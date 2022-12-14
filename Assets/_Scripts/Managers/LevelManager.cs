using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private int desEscapedRobberNumber = 3;
    [SerializeField] private int escapedRobberNumber = 0;


    private void OnEnable()
    {
        EventManager.RobberReachedToFinish += IncreaseEscapedRobberNumber;
    }

    private void OnDisable()
    {
        EventManager.RobberReachedToFinish -= IncreaseEscapedRobberNumber;
    }


    private void IncreaseEscapedRobberNumber(Transform robberTransform)
    {
        escapedRobberNumber++;

        if (escapedRobberNumber >= desEscapedRobberNumber && Enums.Instance.GameState != Enums.GameStates.End)
        {
            Enums.Instance.GameState = Enums.GameStates.End;
            Debug.Log("Level Completed");
            EventManager.LevelCompleted?.Invoke();
        }
        
        EventManager.UpdateSignText?.Invoke();
    }


    public int DesEscapedRobberNumber => desEscapedRobberNumber;
    public int EscapedRobberNumber => escapedRobberNumber;
}
