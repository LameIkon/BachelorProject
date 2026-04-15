using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIHoverHandler : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private UIToggleEventSO _toggleEvent;
    [SerializeField] private float _exitDelay = 0.5f; // Default delay for UI exit

    private UIModule _uIModule;
    private Slider _slider;
    private Coroutine _exitCoroutine;

    private void Awake()
    {
        _uIModule = GetComponentInParent<UIModule>();
        _slider = GetComponentInChildren<Slider>();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        // Set coroutine
        if (_exitCoroutine != null)
        {
            StopCoroutine(_exitCoroutine);
            _exitCoroutine = null;
        }

        // Reset Slider
        if (_slider != null) _slider.value = 0f;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        _exitCoroutine = StartCoroutine(DelayedExit());
    }


    private IEnumerator DelayedExit()
    {
        float timer = 0f;

        while (timer < _exitDelay)
        {
            timer += Time.deltaTime;

            if (_slider != null)
            {
                float value = timer / _exitDelay;
                _slider.value = Mathf.Clamp(value, 0f, 1f); // Fill slider based on timer
            }

            yield return null;
        }

        // Trigger 
        if (_uIModule != null) _toggleEvent?.Raise(new UIRequest(_uIModule.UIType, UIInteractionSource.UIInternal));

        // Reset
        _exitCoroutine = null;
    }
}