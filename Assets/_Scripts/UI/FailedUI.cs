using Debug = UnityEngine.Debug;

public class FailedUI : BaseUI
{
    // public override void ButtonAction()
    // {
    //     Debug.Log("Fail Button Action");
    // }

    public void TapToRestartButtonAction()
    {
        Debug.Log("Tap To Restart Button Action");
    }

    public void SkipThisLevelButtonAction()
    {
        Debug.Log("Skip This Level Button Action");
    }
}
