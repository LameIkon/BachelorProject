using UnityEngine;
using UnityEngine.InputSystem;
using System;

public class InputReader : ScriptableObject, Inputs.IGameActions
{
    private static Inputs _input;
    private static InputState _state;

    public static event Action<Vector2> s_OnMoveEvent;
    public static event Action s_OnInteractEvent;
    public static event Action<Vector2> s_OnLookEvent;
    public static event Action s_OnUseEvent;
   
    
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
        if (context.phase == InputActionPhase.Performed) s_OnInteractEvent?.Invoke();
    }

    public void OnLook(InputAction.CallbackContext context)
    {
        s_OnLookEvent?.Invoke(context.ReadValue<Vector2>());
    }

    public void OnUse(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed) s_OnUseEvent?.Invoke();
    }
    
    #endregion
}

public enum InputState
{
   Game,
   UI
}