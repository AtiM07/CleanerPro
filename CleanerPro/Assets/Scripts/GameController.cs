using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameController : MonoBehaviour
{
    public static GameController Instance;
    public static int Points { get; private set; }
    public static int Level { get; private set; } = 1;

    private int point = 10;

    [SerializeField]
    public TextMeshProUGUI pointsText;
    [SerializeField]
    private TextMeshProUGUI lvlTxt;
    [SerializeField]
    private SpriteRenderer imgBgrd; 
    [SerializeField]
    private Camera colorBgrd;


    private void Awake()
    {
        if (Instance == null)
            Instance = this;
    }

    private void Update()
    {
        
    }

    /// <summary>
    /// ????????? ???????? ??????????? ????
    /// </summary>
    private void GenerateBackground()
    {
        int i;
        i = Random.Range(0, TypeManager.Instance.background.Length - 1);
        imgBgrd.sprite = TypeManager.Instance.background[i].imgBgrd;
        colorBgrd.backgroundColor = TypeManager.Instance.background[i].colorBgrd;
    }
    public void StartGame()
    {
        //if (Level != 1)
        //    AddPoints(point);
        //else
        //    AddPoints(0);
        Person.Instance.isWinner = false;
        Field.Instance.GenerateField();
        GenerateBackground();
    }
    private void SetPoints( int points)
    {
        Points = points;
        pointsText.text = Points.ToString();
    }

    /// <summary>
    /// ?????????? ????? ? ????? ????
    /// </summary>
    /// <param name="points">???????????? ????</param>
    public void AddPoints(int points)
    {
        SetPoints(Points + points);
    }

    /// <summary>
    /// ??????? ?? ????????? ???????
    /// </summary>
    public void NextLvl()
    {

        Vibration.Instance.PlayVibration();
        MenuPanel.Instance.animNextLvl();
    }

    public void StartnextLvl()
    {
        Level++;
        lvlTxt.text = "??????? " + Level;
        AddPoints(point);
        pointsText.text = Points.ToString();
        StartGame();

    }
    

}
