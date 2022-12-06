using System;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BaseUI : MonoBehaviour
{
    [Header("Title")]
    [SerializeField] private TMP_Text titleTMP;
    public string titleText = "";

    [Header("Button")]
    [SerializeField] private List<ButtonVariables> buttonVariables = new List<ButtonVariables>();


    private void Start()
    {
        
        InitializeTitle();
        InitializeButton();
    }

    private void OnEnable()
    {
        OpenAnimation();
    }

    private void OpenAnimation()
    {
        transform.localScale = Vector3.zero * .5f;
        transform.DOScale(Vector3.one, .5f).SetEase(Ease.OutBounce);
    }

    private void InitializeButton()
    {
        foreach (var buttonVariable in buttonVariables)
        {
            buttonVariable.InitializeButton();
        }
    }

    private void InitializeTitle()
    {
        titleTMP.text = titleText;
    }

    public virtual void ButtonAction() {}
}

[Serializable]
public class ButtonVariables
{
    [SerializeField] private Button button;
    [SerializeField] private TMP_Text buttonTextTMP;
    [SerializeField] private Color buttonColor;
    [SerializeField] private string buttonText;

    public void InitializeButton()
    {
        buttonTextTMP.text = buttonText;
        button.image.color = buttonColor;
    }
}
