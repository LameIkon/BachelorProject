using UnityEngine;

public class CanvasUIModule
{
    private readonly GameObject _canvas;

    //public bool ActiveState { get; private set; }

    public CanvasUIModule(GameObject canvas, bool state)
    {
        _canvas = canvas;
        Debug.Log(state);
        _canvas.SetActive(state);
    }

    /// <summary>
    /// Set gameobject active if inactive and vice versa
    /// </summary>
    public void SetActive(bool state)
    {
        _canvas.SetActive(state);
    }
}