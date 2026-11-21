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
        if (player.isMoving == false)
        {
            player.ChangeState(new IdleState());
        }
    }
    public void ExitState(PlayerController player)
    {
        Debug.Log("MoveState çýktý");
    }

}
