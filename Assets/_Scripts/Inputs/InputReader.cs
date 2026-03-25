using UnityEngine;
using UnityEngine.InputSystem;
using System;

[CreateAssetMenu(fileName ="InputReader SO", menuName = "ScriptableObject/InputReader")]
public class InputReader : ScriptableObject, Inputs.IGameActions, Inputs.IUIActions
{
    [Header("UI Toggles")] // More to be implemented later
    [SerializeField] private UIToggleEventSO _toggleUI;

    private static Inputs _input;
    public static InputState s_State;

    public static event Action<InputState> s_OnInputStateChangedEvent;

    public static event Action<Vector2> s_OnMoveEvent;
    public static event Action<Vector2> s_OnInteractEvent;
    public static event Action<Vector2> s_OnLookEvent;
    public static event Action s_OnUseEvent;
    public static event Action s_ToggleEscape;

    private Vector2 MousePos;
   
    
    #region Unity Methods
    private void OnEnable()
    {
        if (_input == null)
        {
            _input = new Inputs();
            _input.UI.SetCallbacks(this);
            _input.Game.SetCallbacks(this);

            SetState(InputState.Game);
            Debug.Log("Inputs started");
        }
    }

    private void OnDisable()
    {
        _input.Game.Disable();
        _input.UI.Disable();        
    }

    #endregion

    #region InputState
    public static void SetState(InputState state)
    {
        // Always keep UI enabled
        _input.UI.Enable();

        // Enable/disable Game input based on state
        if (state == InputState.Game)
        {
            _input.Game.Enable();
        }
        else
        {
            _input.Game.Disable();
        }

        // Update state and notify listeners
        s_State = state;
        s_OnInputStateChangedEvent?.Invoke(s_State); 
    }
    #endregion

    #region Controls
    public void OnMove(InputAction.CallbackContext context)
    {
        s_OnMoveEvent?.Invoke(context.ReadValue<Vector2>());
    }

    public void OnInteract(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed) s_OnInteractEvent?.Invoke(MousePos);
    }

    public void OnLook(InputAction.CallbackContext context)
    {
        s_OnLookEvent?.Invoke(context.ReadValue<Vector2>());
    }

    public void OnUse(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed) s_OnUseEvent?.Invoke();
    }

    public void OnMousePosition(InputAction.CallbackContext context) 
    {
        MousePos = context.ReadValue<Vector2>();
    }

    public void OnEscape(InputAction.CallbackContext context)
    {
        if (context.started) s_ToggleEscape?.Invoke();
    }

    public void OnCompendium(InputAction.CallbackContext context)
    {
        Debug.Log("try Toggle Compendium");
        if (context.started) _toggleUI.Raise(UIType.Compendium);
    }

    #endregion
}

public enum InputState : byte
{
   Game,
   UI
}
