using System;
using System.Collections;
using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    private int score;
    public int _score
    {
        get { return score; }
        set { score = value; }
    }
    [SerializeField] private TextMeshProUGUI scoreText;
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
        scoreText.text = "Score: " + score.ToString();
        StartCoroutine(TimeAddScore());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void AddScore(int AddScore)
    {
        this.score += AddScore;
        scoreText.text = "Score: " + score.ToString();
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
