using UnityEngine;
using UnityEngine.InputSystem;
using System;

public class InputReader : ScriptableObject, Inputs.IGameActions
{
    private static Inputs _input;
    public static InputState s_State;

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

    private static void SetState(InputState state)
    {
        switch (state)
        {
            case InputState.Game:
                _input.Game.Enable();
                return;
            case InputState.UI:
                _input.UI.Enable();
                return;
        }
        
    }

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
        s_ToggleCompendium?.Invoke();
    }

    #endregion
}

public enum InputState : byte
{
   Game,
   UI
}