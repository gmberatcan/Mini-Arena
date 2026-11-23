using UnityEngine;

public class DeadState : IState
{
    public void EnterState(PlayerController player)
    {
        Debug.Log("DeadState");
    }

    public void UpdateState(PlayerController player)
    {
        
    }

    public void ExitState(PlayerController player)
    {
        
    }
}
