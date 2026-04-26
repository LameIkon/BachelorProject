using System.Collections.Generic;
using UnityEngine;

public class InputPromptUI : MonoBehaviour
{
    [Header("Events")]
    [SerializeField] private InputPromptProvideEventSO _uiHoverDataEvent;

    [Header("Components")]
    [SerializeField] private Transform _container;
    [SerializeField] private GameObject _uiPromptholderPefab;

    private readonly List<PromptUIElement> _uiPromptPool = new();

    private void OnEnable()
    {
        _uiHoverDataEvent.OnRaise += UpdateDisplay;
    }

    private void OnDisable()
    {
        _uiHoverDataEvent.OnRaise -= UpdateDisplay;
    }

    private void UpdateDisplay(IEnumerable<InteractionData> data)
    {
        int index = 0;

        foreach (InteractionData dataItem in data)
        {
            PromptUIElement element;

            // Reuse old prompt holder
            if (index < _uiPromptPool.Count)
            {
                element = _uiPromptPool[index];
                element.gameObject.SetActive(true);
            }
            else // Else create a new ui prompt holder
            {
                GameObject instance = Instantiate(_uiPromptholderPefab, _container);
                element = instance.GetComponent<PromptUIElement>();

                _uiPromptPool.Add(element);
            }

            Debug.Log(dataItem.icon);
            Debug.Log(dataItem.description);

            // Update content
            element.SetIcon(dataItem.icon);
            element.SetDescription(dataItem.description);

            index++;
        }

        // Disable unused prompt holders
        for (int i = index; i < _uiPromptPool.Count; i++)
        {
            _uiPromptPool[i].gameObject.SetActive(false);
        }
    }
}
