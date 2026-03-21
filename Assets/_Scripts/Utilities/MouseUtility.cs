using UnityEngine;

/// <summary>
/// A class designed to contain all logic related to the mouse. It depends on InputState and can change between Game and UI InputState
/// If InputState is Game it will lock cursor to the center of screen. If the InputState is UI it will free the cursor to move wherever on the screen.
/// </summary>
public class MouseUtility
{
    private readonly GameObject _crosshairUI;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="staticCursor"></param>
    public MouseUtility(GameObject staticCursor)
    {
        _crosshairUI = staticCursor;

        InputReader.s_OnInputStateChangedEvent += SetCursorState;
    }

    /// <summary>
    /// Call this method from the coupled script to disable events, when we don't need to listen anymore
    /// </summary>
    public void Dispose()
    {
        InputReader.s_OnInputStateChangedEvent -= SetCursorState;
    }

    /// <summary>
    /// Set input reading state. Can change between Game and UI mode
    /// </summary>
    private void SetCursorState(InputState state)
    {
        Debug.Log(state);
        switch (state)
        {
            case InputState.Game:
                Cursor.lockState = CursorLockMode.Locked;
                _crosshairUI.SetActive(true);
                break;
            case InputState.UI:
                Cursor.lockState = CursorLockMode.None;
                _crosshairUI.SetActive(false);
                break;
        }
    }
}
