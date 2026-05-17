using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InspectHover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    [SerializeField] private Animator _animator;
    [SerializeField] private GameObject _description;
    [SerializeField] private GameObject _errorDescription;
    [SerializeField] private Image _Circle;

    private Color _originalColor; 

    private void Awake()
    {
        _originalColor = _Circle.color;
        if (_description != null) _description.gameObject.SetActive(false);
        if (_errorDescription != null) _errorDescription.gameObject.SetActive(false);
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        if (_description != null) _description.SetActive(!_description.activeInHierarchy);
        
        if (_errorDescription != null) _errorDescription.SetActive(!_errorDescription.activeInHierarchy);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        Debug.Log("Enter");
        _Circle.color = Color.yellow;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        Debug.Log("Exit");
        _Circle.color = _originalColor;

        if (_description != null) _description.gameObject.SetActive(false);
        if (_errorDescription != null) _errorDescription.gameObject.SetActive(false);
    }
}
