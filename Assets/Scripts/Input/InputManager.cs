using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    public enum ESwipeDir
    {
        Up,
        Down,
        Right,
        Left
    }
    
    // Singleton members
    private static InputManager m_instance;
    public static InputManager Instance => m_instance;
    
    // Public events
    public event Action OnTap;
    public event Action<ESwipeDir> OnSwipe;

    // Swipe config
    [SerializeField] private float m_minSwipeDistance = 50f;
    [SerializeField] private float m_swipeDirectionThreshold = 0.9f;
    
    // General private members
    private RunnerInputActions m_inputActions;
    private Vector2 m_startTouchPosition;

    private void Awake()
    {
        if (m_instance)
        {
            Destroy(gameObject);
            return;
        }
        
        m_instance = this;
        DontDestroyOnLoad(gameObject);
        
        SetupControls();
    }

    private void OnEnable()
    {
        m_inputActions.Enable();
    }

    private void OnDisable()
    {
        m_inputActions.Disable();
    }

    private void SetupControls()
    {
        m_inputActions = new();

        m_inputActions.Gameplay.PrimaryTouch.performed += OnPrimaryTouchAction;
        m_inputActions.Gameplay.PrimaryTouch.started += OnStartPrimaryTouchAction;
        m_inputActions.Gameplay.PrimaryTouch.canceled += OnEndPrimaryTouchAction;
    }

    private void OnPrimaryTouchAction(InputAction.CallbackContext ctx)
    {
        OnTap?.Invoke();
    }

    private void OnStartPrimaryTouchAction(InputAction.CallbackContext ctx)
    {
        m_startTouchPosition = m_inputActions.Gameplay.TouchPosition.ReadValue<Vector2>();
    }
    
    private void OnEndPrimaryTouchAction(InputAction.CallbackContext ctx)
    {
        Vector2 endTouchPosition = m_inputActions.Gameplay.TouchPosition.ReadValue<Vector2>();

        DetectSwipe(endTouchPosition - m_startTouchPosition);
    }

    private void DetectSwipe(Vector2 dir)
    {
        if (dir.magnitude < m_minSwipeDistance)
        {
            return;
        }

        float upDot = Vector2.Dot(Vector2.up, dir.normalized);
        if (Mathf.Abs(upDot) > m_swipeDirectionThreshold)
        {
            OnSwipe?.Invoke(upDot > 0 ? ESwipeDir.Up : ESwipeDir.Down);
            return;
        }

        float rightDot = Vector2.Dot(Vector2.right, dir.normalized);
        if (Mathf.Abs(rightDot) > m_swipeDirectionThreshold)
        {
            OnSwipe?.Invoke(rightDot > 0 ? ESwipeDir.Right : ESwipeDir.Left);
        }
    }
}
