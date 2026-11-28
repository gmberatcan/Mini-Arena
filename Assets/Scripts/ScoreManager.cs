using System;
using System.Collections;
using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    private const string HighScoreKey = "HighScore";

    private int score;
    public int _score
    {
        get { return score; }
        set { score = value; }
    }
    private int highScore;
    public int HighScore => highScore;
    public bool IsNewHighScore { get; private set; }

    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI highScoreText;
    private void OnEnable()
    {
        MoveEnemy.OnEnemyDeath += AddScore;
    }
    private int addScoreValue;
    private float ScoreTime;
    private void OnDisable()
    {
        MoveEnemy.OnEnemyDeath -= AddScore;
    }
    void Start()
    {
        ScoreTime = 5f;
        addScoreValue = 10;
        _score = 0;
        IsNewHighScore = false;
        highScore = PlayerPrefs.GetInt(HighScoreKey, 0);
        UpdateScoreUI();
        StartCoroutine(TimeAddScore());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void AddScore(int AddScore)
    {
        this.score += AddScore;
        if (this.score > highScore)
        {
            highScore = this.score;
            IsNewHighScore = true;
            PlayerPrefs.SetInt(HighScoreKey, highScore);
            PlayerPrefs.Save();
        }
        UpdateScoreUI();
    }
    private void UpdateScoreUI()
    {
        if (scoreText != null)
            scoreText.text = "Score: " + score;
        if (highScoreText != null)
            highScoreText.text = "High Score: " + highScore;
    }
    IEnumerator TimeAddScore()
    {
        while (true)
        {
            yield return new WaitForSeconds(ScoreTime);
            if (ScoreTime>=1f) ScoreTime -= 0.1f;
            AddScore(addScoreValue);
        }
    }
}
