using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Cell : MonoBehaviour
{
    public enum CellType { None = 0, Wall, Trash };

    public int X { get; private set; }

    public int Y { get; private set; }

    public CellType Type { get; private set; }

    [SerializeField]
    private Sprite Sprite;

    public void SetValue(int x, int y, CellType type)
    {
        X = x;
        Y = y;
        Type = type;

        UpdateType(type);
    }

    /// <summary>
    /// ћен€ет картинку €чейки и ее тип
    /// </summary>
    /// <param name="type">тип €чейки</param>
    public void UpdateType(CellType type)
    {        
        Sprite = (type == CellType.Trash) ? TypeManager.Instance.Trash : ((type == CellType.Wall) ? TypeManager.Instance.Wall : TypeManager.Instance.None);
        GetComponent<SpriteRenderer>().sprite = Sprite;

        if (this.Type != type)
            SetValue(this.X, this.Y, type);
    }

}
