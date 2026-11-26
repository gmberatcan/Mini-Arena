using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private PlayerController playerController;
    [SerializeField] private GameObject GameOverMenu;
    private bool isGameOver = false;
    public bool _isGameOver
    {
        get { return isGameOver; }
        set { isGameOver = value; }
    }
    void Start()
    {
        
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

}
