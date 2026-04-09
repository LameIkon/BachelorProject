using System;
using UnityEngine;

public class MouseHandler : IDisposable
{
    public MouseHandler()
    {
        // Crosshair related
        InputReader.s_OnInputStateChangedEvent += SetCursorState;
        SetCursorState(InputReader.s_State);
    }

    /// <summary>
    /// Set input reading state. Can change between Game and UI mode
    /// </summary>
    private void SetCursorState(InputState state)
    {
        //Debug.Log(state);
        switch (state)
        {
            case InputState.Game:
                Cursor.lockState = CursorLockMode.Locked;
                break;
            case InputState.UI:
            case InputState.None:
                Cursor.lockState = CursorLockMode.None;
                break;
        }
    }

    public void Dispose()
    {
        InputReader.s_OnInputStateChangedEvent -= SetCursorState;
    }
}
