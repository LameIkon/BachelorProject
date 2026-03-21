using UnityEngine;
using UnityEngine.InputSystem;
using System;

public class InputReader : ScriptableObject, Inputs.IGameActions
{
    private static Inputs _input;
    public static InputState s_State;

    public static event Action<InputState> s_OnInputStateChangedEvent;

    public static event Action<Vector2> s_OnMoveEvent;
    public static event Action<Vector2> s_OnInteractEvent;
    public static event Action<Vector2> s_OnLookEvent;
    public static event Action s_OnUseEvent;
    public static event Action s_ToggleCompendium;
    public static event Action s_ToggleEscape;

    private Vector2 MousePos;
   
    
    #region Unity Methods
    private void OnEnable()
    {
        if (_input == null)
        {
            _input = new Inputs();
            _input.Game.SetCallbacks(this);
            SetState(InputState.Game);
            Debug.Log("Inputs started");
        }
    }

    #endregion

    #region InputState
    public static void SetState(InputState state)
    {
        // Disable previous
        switch (s_State)
        {
            case InputState.Game:
                _input.Game.Disable();
                break;
            case InputState.UI:
                _input.UI.Disable();
                break;
        }

        // Enable new
        switch (state)
        {
            case InputState.Game:
                _input.Game.Enable();
                break;
            case InputState.UI:
                _input.UI.Enable();
                break;
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
        if (context.started) s_ToggleCompendium?.Invoke();
    }

    #endregion
}

public enum InputState : byte
{
   Game,
   UI
}