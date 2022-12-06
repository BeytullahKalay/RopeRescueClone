using System;
using TMPro;
using UnityEngine;

public class SignText : MonoBehaviour
{
    [SerializeField] private LevelManager levelManager;
    [SerializeField] private TMP_Text signTemp;


    private void OnEnable()
    {
        EventManager.UpdateSignText += UpdateText;
    }

    private void OnDisable()
    {
        EventManager.UpdateSignText -= UpdateText;
    }

    private void Start()
    {
       UpdateText();
    }

    private void UpdateText()
    {
        signTemp.text = levelManager.EscapedRobberNumber.ToString() + "/" +
                        levelManager.DesEscapedRobberNumber.ToString();
    }
}
