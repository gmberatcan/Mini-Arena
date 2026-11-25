using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private PlayerController playerController;
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
    }
}
