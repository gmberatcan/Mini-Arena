using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR;

public class PlayerController : MonoBehaviour
{
    [SerializeField]private float speed=10;
    Rigidbody rb;
    private IState currentState;
    public bool isMoving = false;
    void Start()
    {
        rb= GetComponent<Rigidbody>();
        currentState = new IdleState();
        currentState.EnterState(this);
    }
    public void ChangeState(IState newState)
    {
        currentState?.ExitState(this);
        currentState = newState;
        currentState.EnterState(this);

    }
    // Update is called once per frame
    void Update()
    {
        CheckMovementInput();
    }
    private void FixedUpdate()
    {
        currentState.UpdateState(this);
    }

    public void CheckMovementInput()
    {
        isMoving =
            Keyboard.current.aKey.isPressed ||
            Keyboard.current.dKey.isPressed ||
            Keyboard.current.wKey.isPressed ||
            Keyboard.current.sKey.isPressed;
    }
    public void Move()
    {
        float horizontalInput = 0;
        float verticalInput = 0;
        if (Keyboard.current.aKey.isPressed)
        {
            horizontalInput = -1;
        }else if (Keyboard.current.dKey.isPressed)
        {
            horizontalInput=1;
        }
        if (Keyboard.current.wKey.isPressed)
        {
            verticalInput = 1;
        }else if (Keyboard.current.sKey.isPressed)
        {
            verticalInput = -1;
        }
        Vector3 MoveDirection=new Vector3 (horizontalInput, 0,verticalInput).normalized;
        rb.linearVelocity = new Vector3(MoveDirection.x * speed, 0, MoveDirection.z * speed);
    }
}
