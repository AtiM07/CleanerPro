using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuPanel : MonoBehaviour
{
    public static MenuPanel Instance;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
    }

    public Animator contentPanel;
    public Animator panel;
    public GameObject nextLvlPanel;

    /// <summary>
    /// Для запуска анимации боковой панели
    /// </summary>
    public void ToggleMenu()
    {
        bool isHidden = contentPanel.GetBool("isHidden");
        contentPanel.SetBool("isHidden", !isHidden);        
        panel.SetBool("isH", !isHidden);
    }

    public void animNextLvl()
    {
        nextLvlPanel.SetActive(!nextLvlPanel.activeSelf);
    }


    public void nextLvl()
    {
        animNextLvl();
        GameController.Instance.StartnextLvl();
    }


}
