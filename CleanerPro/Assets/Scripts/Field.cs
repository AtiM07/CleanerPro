using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Field : MonoBehaviour
{
    public static Field Instance;

    private float CellSize; //размер ячейки на поле
    public int FieldSize; //количество ячеек по ширине/высоте 
    public int WallCount;//количество стен на всем поле

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
        //GeneratePerson();
    }
    /// <summary>
    /// Создает поле с пустыми по умолчанию ячейками заданного размера с определенной позицией на поле
    /// </summary>
    private void CreateField()
    {             
        field = new Cell[FieldSize,FieldSize];

        //float fieldWidth = FieldSize * CellSize  ;
        float fieldWidth = (float) (Instance.rectTr.rect.width - 1.2);

        CellSize = (float)(Instance.rectTr.rect.width - 1.2) / FieldSize;

        rectTr.sizeDelta = new Vector2(fieldWidth, fieldWidth);

        float startX = -(fieldWidth / 2) + (CellSize / 2);
        float startY = (fieldWidth / 2) - (CellSize / 2);

        for (int x = 0; x < FieldSize; x++)
        {
            for (int y = 0; y < FieldSize; y++)
            {
                var cell = Instantiate(cellPref, transform, false);
                var position = new Vector2(startX + (x * (CellSize)), startY - (y * (CellSize)));
                cell.transform.position = position;
                cell.transform.localScale = new Vector2(CellSize, CellSize);

                field[x, y] = cell;

                cell.SetValue(x, y, Cell.CellType.Trash);
            }
        }

    }
    /// <summary>
    /// Генерирует поле, заполненное определенными типами ячеек
    /// </summary>
    public void GenerateField()
    {
        if (field == null)
            CreateField();

        for (int x = 0; x < FieldSize; x++)
            for (int y = 0; y < FieldSize; y++)
                field[x, y].SetValue(x, y, Cell.CellType.Trash);

        //for (int i = 0; i < WallCount; i++)
        //    GenerateRandomCell();

        Generate();
    }
    
    /// <summary>
    /// Добавляет на поле ячейки-стены
    /// </summary>
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

    /// <summary>
    /// Добавляет на поле персонажа в trash-ячейку и очищает ее
    /// </summary>
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
        //person.transform.localPosition = (Vector2)position;

        person.transform.position = field[i, j].transform.position;
        person.transform.localScale = new Vector2(CellSize, CellSize);

    }

    void restartPosition(out int x, out int y)
    {
         x = person.X;
        y = person.Y;
    }

    /// <summary>
    /// Демо-версия генерации лабиринта со стенами
    /// </summary>
    public void Generate()
    {
        int[,] wayCell = new int[FieldSize,FieldSize];
        int i, j;
        i = 0; //Random.Range(0, FieldSize - 1);
        j = 0; //Random.Range(0, FieldSize - 1);
        person.SetValue(field[i, j].X, field[i, j].Y);
        person.transform.position = field[i, j].transform.position;
        person.transform.localScale = new Vector2(CellSize, CellSize);


        int startPosX = person.X, startPosY = person.Y;
        wayCell[startPosX, startPosY] = 1;
        //if (startPosX == 0 && startPosY == 0)
        //{
       if (Random.Range(0, 100) % 2 ==0 )
        {
            startPosX = Random.Range(1, FieldSize - 1);
            field[startPosX, startPosY].UpdateType(Cell.CellType.Wall);
            wayCell[startPosX, startPosY] = 1;
        }
       else
        {
            startPosY = Random.Range(1, FieldSize - 1);
            field[startPosX, startPosY].UpdateType(Cell.CellType.Wall);
            wayCell[startPosX, startPosY] = 1;
        }

        restartPosition(out startPosX, out startPosY);
        //}

        //проверяем пути вправо вниз
        #region

         i = FieldSize * FieldSize;
        while (startPosX != FieldSize && startPosY != FieldSize && i != 0)
        {
            i--;
            while (startPosX != FieldSize && field[startPosX, startPosY].Type != Cell.CellType.Wall)
            {
                wayCell[startPosX, startPosY] = 1;
                startPosX++;
            }
            startPosX--;

            while (startPosY != FieldSize && field[startPosX, startPosY].Type != Cell.CellType.Wall)
            {
                wayCell[startPosX, startPosY] = 1;
                startPosY++;
            }
            startPosY--;

            if (startPosX == FieldSize - 1 && startPosY == FieldSize - 1 && field[startPosX, startPosY].Type != Cell.CellType.Wall)
            {
                wayCell[startPosX, startPosY] = 1;
                startPosX++;
                startPosY++;
            }
           
        }
        Debug.Log("Вышли" + i);
        restartPosition(out startPosX, out startPosY);

        #endregion
        //проверяем пути вниз вправо
        #region
        i = FieldSize * FieldSize;
        
        while (startPosX != FieldSize && startPosY != FieldSize && i !=0)
        {
            i--;
            while (startPosY != FieldSize && field[startPosX, startPosY].Type != Cell.CellType.Wall)
            {
                wayCell[startPosX, startPosY] = 1;
                startPosY++;
            }
            startPosY--;

            

            while (startPosX != FieldSize && field[startPosX, startPosY].Type != Cell.CellType.Wall)
            {
                wayCell[startPosX, startPosY] = 1;
                startPosX++;
            }
            startPosX--;
            
            if (startPosX == FieldSize - 1 && startPosY == FieldSize - 1)
            {
               
                startPosX = FieldSize;
                startPosY = FieldSize;
            }
           
        }
        Debug.Log("Вышли" + i);
        startPosX = FieldSize - 1;
        startPosY = FieldSize - 1;


        i = FieldSize * FieldSize;

        #endregion
        //проверяем пути влево вверх
        #region
        while (startPosX != -1 && startPosY != -1 && i != 0)
        {
            i--;
            while (startPosX != -1 && field[startPosX, startPosY].Type != Cell.CellType.Wall)
            {
                wayCell[startPosX, startPosY] = 1;
                startPosX--;
            }
            startPosX++;

            while (startPosY != -1 && field[startPosX, startPosY].Type != Cell.CellType.Wall)
            {
                wayCell[startPosX, startPosY] = 1;
                startPosY--;
            }
            startPosY++;

            if (startPosX == 0 && startPosY == 0 && field[startPosX, startPosY].Type != Cell.CellType.Wall)
            {
                wayCell[startPosX, startPosY] = 1;
                startPosX = -1; startPosY = -1;
            }

            if (startPosX != -1 && startPosY != -1 )
            if ((startPosY != 0 && field[startPosX, startPosY - 1].Type == Cell.CellType.Wall && startPosX == 0) ||
                (startPosX != 0 && field[startPosX - 1, startPosY].Type == Cell.CellType.Wall && startPosX <= FieldSize - 1) ||
                   startPosY != 0 && startPosX != 0 && field[startPosX - 1, startPosY - 1].Type == Cell.CellType.Wall)
            {
                startPosX = -1; startPosY = -1;
            }

           

        }
        Debug.Log("Вышли" + i);
        startPosX = FieldSize - 1;
        startPosY = FieldSize - 1;

        #endregion
        //проверяем пути вверх влево
        #region
        while (startPosX != -1 && startPosY != -1 && i != 0)
        {
            i--;
            while (startPosY != -1 && field[startPosX, startPosY].Type != Cell.CellType.Wall)
            {
                wayCell[startPosX, startPosY] = 1;
                startPosY--;
            }
            startPosY++;

            while (startPosX != -1 && field[startPosX, startPosY].Type != Cell.CellType.Wall)
            {
                wayCell[startPosX, startPosY] = 1;
                startPosX--;
            }
            startPosX++;

            if (startPosX == 0 && startPosY == 0 && field[startPosX, startPosY].Type != Cell.CellType.Wall)
            {
                wayCell[startPosX, startPosY] = 1;
                startPosX = -1; startPosY = -1;
            }

            if (startPosX != -1 && startPosY != -1)
                if ((startPosY != 0 && field[startPosX, startPosY - 1].Type == Cell.CellType.Wall && startPosX == 0) || 
                (startPosX != 0 && field[startPosX - 1, startPosY].Type == Cell.CellType.Wall && startPosX == FieldSize-1) ||
                   startPosY != 0 &&  startPosX != 0  &&  field[startPosX - 1, startPosY-1].Type == Cell.CellType.Wall)
            {
                startPosX = -1; startPosY = -1;
            }
            
        }
        Debug.Log("Вышли" + i);
        #endregion

        for (int x = 0; x < FieldSize; x++)
            for (int y = 0; y < FieldSize; y++)
                if (wayCell[x, y] == 0)
                     field[x, y].UpdateType(Cell.CellType.Wall);

    }


}
