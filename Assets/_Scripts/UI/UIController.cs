using UnityEngine;

public class UIController : MonoBehaviour
{
    [SerializeField] private GameObject levelCompletedPanel;
    [SerializeField] private GameObject levelFailedPanel;


    private void OnEnable()
    {
        EventManager.LevelCompleted += OpenLevelCompletedPanel;
        EventManager.LevelFailed += OpenLevelFailedPanel;
    }

    private void OnDisable()
    {
        EventManager.LevelCompleted -= OpenLevelCompletedPanel;
        EventManager.LevelFailed -= OpenLevelFailedPanel;
    }


    private void OpenLevelCompletedPanel()
    {
        levelCompletedPanel.SetActive(true);
    }
    
    private void OpenLevelFailedPanel()
    {
        levelFailedPanel.SetActive(true);
    }
}
