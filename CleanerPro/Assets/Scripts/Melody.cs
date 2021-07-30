using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Melody : MonoBehaviour
{
    public static Melody Instance;
    public bool melody = false;

    Toggle toggle;
    AudioSource audS;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;

        toggle = GetComponent<Toggle>();
        audS = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (toggle.isOn)
            melody = true;
        else melody = false;
    }

    public void PlayMelody()
    {
        if (melody)
        {
            if (!audS.isPlaying)
            audS.PlayOneShot(audS.clip);
        }
    }
}
