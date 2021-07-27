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
    /// �������� ��������� � ������������ �����������
    /// </summary>
    /// <param name="direction">����������� ��������</param>
    public void Move(Vector2Int direction)
    {
        currentPos = new Vector2Int(Instance.X, Instance.Y);
        StartCoroutine(DirectionCoroutine(direction));
    }

    IEnumerator DirectionCoroutine(Vector2Int direction)
    {
        isTravelling = true;
        Vector2Int nextPos = currentPos + direction;

        Cell nextCell = Field.Instance.field[0, 0];

        for (int x = 0; x < Field.Instance.FieldSize; x++)
        {
            for (int y = 0; y < Field.Instance.FieldSize; y++)
            {
                if (Field.Instance.field[x, y].X == nextPos.x && Field.Instance.field[x, y].Y == nextPos.y)
                    nextCell = Field.Instance.field[nextPos.x, nextPos.y];
            }
        }

        if (nextCell.Type == Cell.CellType.Wall)
        {
            isTravelling = false;
            yield break;
        }

        while (nextCell.Type != Cell.CellType.Wall &&
            (nextPos.x > -1 && nextPos.y > -1 && nextPos.x < Field.Instance.FieldSize && nextPos.y < Field.Instance.FieldSize))
        {

            yield return Clear();
            //yield return MoveCoroutine(currentPos, nextPos, direction);
            //if (nextPos.x < Field.Instance.FieldSize && nextPos.y < Field.Instance.FieldSize)
            //{           
                Instance.transform.position = (Vector2)Field.Instance.field[nextPos.x, nextPos.y].transform.position;
                Instance.X = Field.Instance.field[nextPos.x, nextPos.y].X;
                Instance.Y = Field.Instance.field[nextPos.x, nextPos.y].Y;
            
            //}

            currentPos += direction;
            nextPos = currentPos + direction;

            for (int x = 0; x < Field.Instance.FieldSize; x++)
            {
                for (int y = 0; y < Field.Instance.FieldSize; y++)
                {
                    if (Field.Instance.field[x, y].X == nextPos.x && Field.Instance.field[x, y].Y == nextPos.y)
                        nextCell = Field.Instance.field[nextPos.x, nextPos.y];
                }
            }
        }

        isTravelling = false;
    }

    //private IEnumerator MoveCoroutine(Vector2Int startPos, Vector2Int endPos, Vector2Int direction)
    //{
    //    Vector2 currentPlayerPos = startPos;

    //    while (Vector2.Distance(currentPlayerPos, endPos) < 0.01f)
    //    {
    //        Instance.transform.position = currentPlayerPos;
    //        currentPlayerPos += Time.deltaTime * 2f * (Vector2)direction;
    //    }

    //    SetValue(endPos.x, endPos.y);

    //    yield break;
    //}

    /// <summary>
    /// �������� �����, ����������� ��� ����������
    /// </summary>
    /// <returns></returns>
    IEnumerator Clear()
    {
        for (int x = 0; x < Field.Instance.FieldSize; x++)
        {
            for (int y = 0; y < Field.Instance.FieldSize; y++)
            {
                if (Instance.transform.position == Field.Instance.field[x, y].transform.position)
                {
                    Field.Instance.field[x, y].UpdateType(Cell.CellType.None);
                }

            }
        }

        yield return ClearCell();
    }

    /// <summary>
    /// ��������, ��� �� ������ �� ���� �������
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
            yield return new WaitForSeconds(0.05f);
            GameController.Instance.NextLvl();
            yield break;
        }
    }


}
