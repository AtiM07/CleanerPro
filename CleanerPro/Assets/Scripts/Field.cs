using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Field : MonoBehaviour
{
    public static Field Instance;

    private float CellSize;
    public int FieldSize;
    public int WallCount;


    [SerializeField]
    private Cell cellPref;
    [SerializeField]
    private RectTransform rectTr;

    public Cell[,] field;
    public Person person;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
    }
    private void Start()
    {
        GenerateField();
        GeneratePerson();
    }
    private void CreateField()
    {             
        field = new Cell[FieldSize,FieldSize];

        //float fieldWidth = FieldSize * CellSize  ;
        float fieldWidth = (float) (Instance.rectTr.rect.width - 0.6);

        CellSize = (float)(Instance.rectTr.rect.width - 0.6) / FieldSize;

        rectTr.sizeDelta = new Vector2(fieldWidth, fieldWidth);

        float startX = -(fieldWidth / 2) + (CellSize / 2);
        float startY = (fieldWidth / 2) - (CellSize / 2);

        for (int x = 0; x < FieldSize; x++)
        {
            for (int y = 0; y < FieldSize; y++)
            {
                var cell = Instantiate(cellPref, transform, false);
                var position = new Vector2(startX + (x * (CellSize)), startY - (y * (CellSize)));
                cell.transform.localPosition = position;
                cell.transform.localScale = new Vector3(CellSize, CellSize, CellSize);

                field[x, y] = cell;

                cell.SetValue(x, y, Cell.CellType.Trash);
            }
        }

    }

    public void GenerateField()
    {
        if (field == null)
            CreateField();

        for (int x = 0; x < FieldSize; x++)
            for (int y = 0; y < FieldSize; y++)
                field[x, y].SetValue(x, y, Cell.CellType.Trash);

        for (int i = 0; i < WallCount; i++)
            GenerateRandomCell();
    }
            
    private void GenerateRandomCell()
    {
        var emptyCells = new List<Cell>();

        for (int x = 0; x < FieldSize; x++)
            for (int y = 0; y < FieldSize; y++)
                if (field[x, y].Type == Cell.CellType.Trash)
                    emptyCells.Add(field[x, y]);

        if (emptyCells.Count == 0)
            throw new System.Exception("There is no eny empty cell!");

        var value = Random.Range(0, 1) == 0 ? Cell.CellType.Wall : Cell.CellType.Trash;

        var cell = emptyCells[Random.Range(0, emptyCells.Count)];
        cell.SetValue(cell.X, cell.Y, value);
    }

    public void GeneratePerson()
    {       
        bool flag = true;
        int i = 0, j = 0;
        while (flag)
        {
            i = Random.Range(0, FieldSize-1);
            j = Random.Range(0, FieldSize-1);
            flag = field[i,j].Type == Cell.CellType.Trash ? false : true;
        }

        person.SetValue(field[i, j].X, field[i, j].Y);
        field[i, j].UpdateType(Cell.CellType.None);
        var position = new Vector2(field[i, j].transform.position.x, field[i, j].transform.position.y);
        person.transform.localPosition = (Vector2)position;
        person.transform.localScale = new Vector2(CellSize, CellSize);

    }
}
