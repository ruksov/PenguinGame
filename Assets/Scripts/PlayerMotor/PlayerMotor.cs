using System;
using UnityEngine;

public class PlayerMotor : MonoBehaviour
{
    [HideInInspector] public Vector3 moveVector;
    [HideInInspector] public float verticalVelocity;
    [HideInInspector] public bool isGrounded;
    [HideInInspector] public int currentLane;

    public float distanceInBetweenLanes = 3.0f;
    public float baseRunSpeed = 5.0f;
    public float baseSideSpeed = 10.0f;
    public float gravity = 14.0f;
    public float terminalVelocity = 20.0f;

    public CharacterController controller;
    public Animator animator;

    private BaseState m_state;
    [SerializeField] private BaseState m_firstState;

    [SerializeField] private int m_deathLayer;

    private void Start()
    {
        controller = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();

        m_state = m_firstState;
        m_state.Enter();
    }

    private void Update()
    {
        UpdateMotor();
    }

    private void UpdateMotor()
    {
        isGrounded = controller.isGrounded;

        m_state.ProcessMotion(ref moveVector);
        m_state.UpdateState();

        // Feed our animator some values
        animator.SetBool("IsGrounded", isGrounded);
        animator.SetFloat("Speed", Mathf.Abs(moveVector.z));
        
        controller.Move(moveVector * Time.deltaTime);  
    }

    public void ApplyGravity()
    {
        if(isGrounded && verticalVelocity < 0.0f)
        {
            verticalVelocity = -gravity;
            return;
        }

        verticalVelocity -= gravity * Time.deltaTime;
    }

    public float ActualSideSpeed()
    {
        float distanceToLane = CurrentLanePositionX() - transform.position. x;

        if (Mathf.Abs(distanceToLane) < (baseSideSpeed * Time.deltaTime))
            return distanceToLane / Time.deltaTime;

        return distanceToLane > 0 ? baseSideSpeed : -baseRunSpeed;
    }

    private float CurrentLanePositionX()
    {
        return currentLane * distanceInBetweenLanes;
    }

    public void ChangeLane(int direction)
    {
        currentLane = Mathf.Clamp(currentLane + direction, -1, 1);
    }

    public void ResetLane()
    {
        currentLane = 0;
    }

    public void ChangeState(BaseState state)
    {
        m_state.Exit();
        m_state = state;
        m_state.Enter();  
    }

    public void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if(m_deathLayer == hit.gameObject.layer)
            ChangeState(GetComponent<DeathState>());
        
    }

    public void RespawnPlayer()
    {
        ChangeState(GetComponent<RespawnState>());
    }
}
