using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class customToggle : MonoBehaviour
{
    [SerializeField] RectTransform hadleRT;

        Toggle toggle;

    Vector2 handlePosition;

    void Awake()
    {
        toggle = GetComponent<Toggle>();

        handlePosition = hadleRT.anchoredPosition;
        toggle.onValueChanged.AddListener(OnSwitch);

        if (toggle.isOn)
            OnSwitch(true);
    }

    /// <summary>
    /// Изменяет положение переключателя
    /// </summary>
    /// <param name="on"></param>
    void OnSwitch(bool on)
    {
        hadleRT.anchoredPosition = on ? handlePosition * -1 : handlePosition;        
    }

    void OnDestroy()
    {
        toggle.onValueChanged.RemoveListener(OnSwitch);
    }
}
