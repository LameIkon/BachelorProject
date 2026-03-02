using UnityEngine;

public class CanvasUIModule
{
    private readonly GameObject _canvas;

    public bool ActiveState { get; private set; }

    public CanvasUIModule(GameObject canvas)
    {
        _canvas = canvas;
        //_canvas.SetActive(false);
    }

    public void Toggle() => _canvas.SetActive(!_canvas.activeSelf);
}