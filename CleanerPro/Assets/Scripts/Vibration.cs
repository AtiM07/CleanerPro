using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Vibration : MonoBehaviour
{
    public static Vibration Instance;
    public bool vibration = false;

    Toggle toggle;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;

        toggle = GetComponent<Toggle>();
    }

    private void Update()
    {
        if (toggle.isOn)
            vibration = true;
        else vibration = false;
    }

    public void PlayVibration()
    {
        if (vibration)
        {
            Handheld.Vibrate();
        }
    }
}
