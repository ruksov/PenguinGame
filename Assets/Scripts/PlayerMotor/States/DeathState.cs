using UnityEngine;

public class DeathState : BaseState
{
    [SerializeField] private Vector3 m_knockbackForce = new Vector3(0.0f, 4.0f, -3.0f);

    public override void Enter()
    {
        m_playerMotor.animator.SetTrigger("Death");
        m_playerMotor.moveVector = m_knockbackForce;
    }

    public override void ProcessMotion(ref Vector3 moveVector)
    {
        if(moveVector.z < 0.0f)
        {
            moveVector.z += m_playerMotor.baseRunSpeed * 0.5f * Time.deltaTime;
        }
        else if(moveVector.z > 0.0f)
        {
            moveVector.z = 0.0f;
            GameFlow.Instance.ChangeState(GameFlow.Instance.GetComponent<GameStateDeath>());
        }
    }
}
