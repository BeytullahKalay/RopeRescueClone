using UnityEngine;

public class RopeColorChanger : MonoBehaviour
{
    [SerializeField] private Color defaultColor;
    [SerializeField] private Color connectedColor;
    
    private LineRenderer _lr;

    private void Awake()
    {
        _lr = GetComponent<LineRenderer>();
    }

    private void OnEnable()
    {
        EventManager.Connected += ChaneColorToConnectedColor;
        EventManager.UnConnected += ChangeColorToDefaultColor;
    }

    private void OnDisable()
    {
        EventManager.Connected -= ChaneColorToConnectedColor;
        EventManager.UnConnected -= ChangeColorToDefaultColor;
    }

    private void ChangeColorToDefaultColor()
    {
        _lr.startColor = defaultColor;
        _lr.endColor = defaultColor;
    }

    private void ChaneColorToConnectedColor()
    {
        _lr.startColor = connectedColor;
        _lr.endColor = connectedColor;  
    }
    
    
}
