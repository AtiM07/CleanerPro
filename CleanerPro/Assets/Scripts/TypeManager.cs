using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TypeManager : MonoBehaviour
{

    public static TypeManager Instance;
        
    //картинки для определенного типа ячеек
    public Sprite None;
    public Sprite Wall;
    public Sprite Trash;

    //массив с возможными вариантами фона
    public Background[] background;
    private void Awake()
    {
        if (Instance == null)
            Instance = this;        
    }

    /// <summary>
    /// Сочетание цвета фона и картинки внизу
    /// </summary>
    [System.Serializable]
    public class Background
    {
        public Sprite imgBgrd;
        public Color colorBgrd;
    }
}
