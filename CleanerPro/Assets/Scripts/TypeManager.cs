using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TypeManager : MonoBehaviour
{

    public static TypeManager Instance;
        
    public Sprite None;
    public Sprite Wall;
    public Sprite Trash;

    public Background[] background;
    private void Awake()
    {
        if (Instance == null)
            Instance = this;

        
    }

    [System.Serializable]
    public class Background
    {
        public Sprite imgBgrd;
        public Color colorBgrd;
    }
}
