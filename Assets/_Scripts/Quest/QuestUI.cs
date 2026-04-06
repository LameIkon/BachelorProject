using TMPro;
using UnityEngine;
using static Quest;

public class QuestUI : MonoBehaviour
{
    [SerializeField] private QuestGiveProviderSO _questProvider;
    [SerializeField] private ActionEventSO _updatedUIEventSO;

    private TextMeshProUGUI _gui;

    #region Unity Methods
    void Start()
    {
        _gui = GetComponentInChildren<TextMeshProUGUI>(true);
    }

    private void OnEnable()
    {
        _updatedUIEventSO.OnRaise += UpdateUI;
    }

    private void OnDisable()
    {
        _updatedUIEventSO.OnRaise -= UpdateUI;
    }
    #endregion

    #region Own Methods
    private void UpdateUI() 
    {
        _gui.text = string.Empty;
        foreach (Part p in _questProvider.GetQuest().Parts)
        {
            if (!p.IsPartComplete)
            {
                _gui.text += p.Description + "\n";
            }
            else 
            {
                _gui.text += "<s>" + p.Description + "</s>\n" ; 
            }
        }
    }

    #endregion

}
