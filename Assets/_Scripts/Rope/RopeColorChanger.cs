using UnityEngine;

public class RopeColorChanger : MonoBehaviour
{
    [SerializeField] private Color defaultColor;
    [SerializeField] private Color connectedColor;
    [SerializeField] private Color slidingColor;
    
    private LineRenderer _lr;

    private void Awake()
    {
        _lr = GetComponent<LineRenderer>();
    }

    private void OnEnable()
    {
        EventManager.Connected += ChaneColorToConnectedColor;
        EventManager.UnConnected += ChangeColorToDefaultColor;
        EventManager.RobberStartedToSlide += ChangeColorToSlideColor;
        EventManager.TryChangeColorToConnectedColor += TryChangeColorToConnectedColor;
    }

    private void OnDisable()
    {
        EventManager.Connected -= ChaneColorToConnectedColor;
        EventManager.UnConnected -= ChangeColorToDefaultColor;
        EventManager.RobberStartedToSlide -= ChangeColorToSlideColor;
        EventManager.TryChangeColorToConnectedColor -= TryChangeColorToConnectedColor;
    }
    
    private void ChangeLinerRendererStartAndEndColorTo(Color c)
    {
        _lr.startColor = c;
        _lr.endColor = c;
    }

    private void ChangeColorToDefaultColor()
    {
        ChangeLinerRendererStartAndEndColorTo(defaultColor);
    }

    private void ChaneColorToConnectedColor()
    {
        ChangeLinerRendererStartAndEndColorTo(connectedColor);
    }

    private void ChangeColorToSlideColor(Transform slide)
    {
        ChangeLinerRendererStartAndEndColorTo(slidingColor);
    }

    private void TryChangeColorToConnectedColor(int slidingRobberAmount)
    {
        if (slidingRobberAmount > 0) return;

        ChangeLinerRendererStartAndEndColorTo(connectedColor);
    }
    
}
