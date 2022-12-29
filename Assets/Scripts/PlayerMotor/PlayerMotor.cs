using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerMotor : MonoBehaviour
{
    [HideInInspector] public Vector3 moveVector;
    [HideInInspector] public float verticalVelocity;
    [HideInInspector] public bool isGrounded;
    [HideInInspector] public int currentLane;

    public float distanceInBetweenLanes = 3.0f;
    public float baseRunSpeed = 5.0f;
    public float baseSidewaySpeed = 10.0f;
    public float gravity = 14.0f;
    public float terminalVelocity = 20.0f;

    public CharacterController controller;

    private BaseState m_state;
    [SerializeField] private BaseState m_firstState;

    private void Start()
    {
        controller = GetComponent<CharacterController>();

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

        moveVector = m_state.ProcessMotion();
        m_state.Update();
        
        controller.Move(moveVector * Time.deltaTime);  
    }

    public void ChangeLane(int direction)
    {
        currentLane = Mathf.Clamp(currentLane + direction, -1, 1);
    }

    public void ChangeState(BaseState state)
    {
        m_state.Exit();
        m_state = state;
        m_state.Enter();  
    }
}
