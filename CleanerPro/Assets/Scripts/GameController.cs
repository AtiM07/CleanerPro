using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameController : MonoBehaviour
{
    public static GameController Instance;
    public static int Points { get; private set; }

    [SerializeField]
    private TextMeshProUGUI pointsText;
    [SerializeField]
    private TextMeshProUGUI resultText;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
    }

    public void StartGame()
    {
        SetPoints(0);

        Field.Instance.GenerateField();
    }
    private void SetPoints( int points)
    {
        Points = points;
        pointsText.text = Points.ToString();
    }
    public void AddPoints(int points)
    {
        SetPoints(Points + points);
    }

    public void Win()
    {
        resultText.text = "You win!";
    }

    public void CheckGameResult()
    {

    }
}
