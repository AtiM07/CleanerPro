using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Person : MonoBehaviour
{
    public static Person Instance;
    
    private void Awake()
    {
        if (Instance == null)
            Instance = this;
    }


    public int X { get; private set; }

    public int Y { get; private set; }

    public void SetValue(int x, int y)
    {
        X = x;
        Y = y;
    }

    private bool isTravelling;
    public bool isWinner;
    private Vector2Int currentPos;

    /// <summary>
    /// Проверка, все ли ячейки на поле очищены
    /// </summary>
    /// <returns></returns>
    IEnumerator ClearCell()
    {
        int trashCell = 0;
        for (int x = 0; x<Field.Instance.FieldSize; x++)
        {
            for (int y = 0; y<Field.Instance.FieldSize; y++)
            {
                if (Field.Instance.field[x, y].Type == Cell.CellType.Trash)
                    trashCell++;
            }
        }

        if (trashCell == 0 && !isWinner)
        {
            isWinner = true;
            yield return new WaitForSeconds(0.25f);
            GameController.Instance.NextLvl();
            yield break;
        }
    }


    private void Update()
    {
        Clear2();
    }

    public void Move(Vector2 direction)
    {

        int x=0, y=0;
        if (direction == Vector2.right)
            x = 1;
        else
        if (direction == Vector2.left)
            x = -1;
        else
        if (direction == Vector2.up)
            y = 1;
        else
        if (direction == Vector2.down)
            y = -1;
        StartCoroutine(MoveCoroutine(x, y));
    }

    IEnumerator MoveCoroutine(int dirX, int dirY)
    {

        if (isTravelling)
            yield break;

        isTravelling = true;

        if (dirY == 0) //вправо/влево
        {
            for (int X = Instance.X + dirX; X < Field.Instance.FieldSize && X > -1; X += dirX)
            {
                Cell nextCell = Field.Instance.field[X, Y];
                if (nextCell.Type != Cell.CellType.Wall)
                {
                    Instance.SetValue(X, Y);
                    Instance.transform.position = nextCell.transform.position;
                    Clear2();
                }
                else
                {
                    isTravelling = false;
                    yield break;
                }

            }
        }

        if (dirX == 0) //влево/вправо
        {
            for (int Y = Instance.Y + dirY; Y < Field.Instance.FieldSize && Y > -1; Y += dirY)
            {
                Cell nextCell = Field.Instance.field[X, Y];
                if (nextCell.Type != Cell.CellType.Wall)
                {
                    Instance.SetValue(X, Y);
                    Instance.transform.position = nextCell.transform.position;
                    Clear2();
                }
                else
                {
                    isTravelling = false;
                    yield break;
                }

            }
        }

        isTravelling = false;
    }

    void Clear2()
    {
        if (Field.Instance.field[X, Y].Type == Cell.CellType.Trash)
            Field.Instance.field[X, Y].UpdateType(Cell.CellType.None);
        StartCoroutine(ClearCell());
    }

}
