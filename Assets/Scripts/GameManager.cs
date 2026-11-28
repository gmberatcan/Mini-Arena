using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private PlayerController playerController;
    [SerializeField] private ScoreManager scoreManager;
    [SerializeField] private GameObject StartMenu;
    [SerializeField] private GameObject GameOverMenu;
    [SerializeField] private GameObject newHighScoreText;
    static public event System.Action<int> OnDifficultyLevelUp;
    private static bool skipStartMenu = false;
    private bool isGameOver = false;
    public bool _isGameOver
    {
        get { return isGameOver; }
        set { isGameOver = value; }
    }
    private float difficultyLevelUpTime = 20f;
    private int difficultyLevel = 0;
    public static int CurrentDifficultyLevel { get; private set; }
    void Start()
    {
        CurrentDifficultyLevel = difficultyLevel;
        if (GameOverMenu != null) GameOverMenu.SetActive(false);
        if (newHighScoreText != null) newHighScoreText.SetActive(false);

        if (StartMenu != null && !skipStartMenu)
        {
            StartMenu.SetActive(true);
            Time.timeScale = 0f;
        }
        else
        {
            if (StartMenu != null) StartMenu.SetActive(false);
            Time.timeScale = 1f;
        }

        skipStartMenu = false;
        StartCoroutine(DifficultyLevelUp());
    }

    // Update is called once per frame
    void Update()
    {
        if (!isGameOver && playerController._currentState is DeadState)
        {
            HandleGameOver();
        }
    }
    private void HandleGameOver()
    {
        isGameOver = true;
        Time.timeScale = 0f;
        if (GameOverMenu != null) GameOverMenu.SetActive(true);
        if (newHighScoreText != null && scoreManager != null)
        {
            newHighScoreText.SetActive(scoreManager.IsNewHighScore);
        }
    }
    public void StartGame()
    {
        if (StartMenu != null) StartMenu.SetActive(false);
        Time.timeScale = 1f;
    }
    public void RestartGame()
    {
        skipStartMenu = true;
        Time.timeScale = 1f;
        UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name);
    }
    public void ReturnToMainMenu()
    {
        skipStartMenu = false;
        Time.timeScale = 1f;
        UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name);
    }
    public void difficultyLevelUpper()
    {
            difficultyLevelUpTime-=2;
            difficultyLevel++;
            CurrentDifficultyLevel = difficultyLevel;
            OnDifficultyLevelUp?.Invoke(difficultyLevel);
    }
    IEnumerator DifficultyLevelUp()
    {
        while (difficultyLevel<=5)
        {
            yield return new WaitForSeconds(difficultyLevelUpTime);
            difficultyLevelUpper();
        }
    }
}
