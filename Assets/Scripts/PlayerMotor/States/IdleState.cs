using UnityEngine;

public class IdleState : BaseState
{
    public override void ProcessMotion(ref Vector3 moveVector)
    {
        moveVector = Vector3.zero;
    }
}
