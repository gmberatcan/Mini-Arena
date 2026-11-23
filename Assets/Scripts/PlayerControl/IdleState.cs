using UnityEngine;

public class IdleState:IState
{
    public void EnterState(PlayerController player)
    {
        Debug.Log("idleState");
    }public void UpdateState(PlayerController player) 
    {
        if (player._health <= 0)
        {
            player.ChangeState(new DeadState());
        }
        else if (player.isMoving == true)
        {
            player.ChangeState(new MoveState());
        }
        
    }public void ExitState(PlayerController player)
    {
        Debug.Log("IdleState çıktı");
    }
}
