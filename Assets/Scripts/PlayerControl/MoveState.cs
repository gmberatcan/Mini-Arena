using UnityEngine;

public class MoveState:IState
{
    public void EnterState(PlayerController player)
    {
        Debug.Log("MoveState");
    }
    public void UpdateState(PlayerController player)
    {
        player.Move();
        if (player._health <= 0)
        {
            player.ChangeState(new DeadState());
        }
        else if (player.isMoving == false)
        {
            player.ChangeState(new IdleState());
        }
        
    }
    public void ExitState(PlayerController player)
    {
        Debug.Log("MoveState çýktý");
    }

}
