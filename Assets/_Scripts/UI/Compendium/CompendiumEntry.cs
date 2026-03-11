using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CompendiumEntry : MonoBehaviour
{
    [Header("button")]
    [SerializeField] private Button _button;
    [SerializeField] private TextMeshProUGUI _buttonTitle;

    [Header("Data")]
    public CompendiumPage compendiumPage;

    private void OnEnable()
    {
        //compendiumPage.OnTitleChanged += UpdateButtonText;
    }

    private void OnDisable()
    {
        //compendiumPage.OnTitleChanged -= UpdateButtonText;
    }

    private void UpdateButtonText(string title)
    {
        _buttonTitle.text = (title);
    }

    public void ToggleButton(bool state)
    {
        _button.gameObject.SetActive(state);
    }
}
