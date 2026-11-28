using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private PlayerController playerController;
    [SerializeField] private GameObject GameOverMenu;
    static public event System.Action<int> OnDifficultyLevelUp;
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
        StartCoroutine(DifficultyLevelUp());
    }

    // Update is called once per frame
    void Update()
    {
        isGameOver=playerController._currentState is DeadState;
        if (isGameOver)
        {
            Time.timeScale = 0f;
            GameOverMenu.SetActive(true);
        }
    }
    public void RestartGame()
    {
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
