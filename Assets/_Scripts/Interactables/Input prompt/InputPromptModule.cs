using System.Collections.Generic;

public class InputPromptModule
{
    private readonly List<InputPromptDataSO> _inputPromptData;
    private readonly UIToggleEventSO _uiToggleEvent;
    private InputPromptProvideEventSO _hoverEvent;

    public InputPromptModule(List<InputPromptDataSO> promptData, InputPromptModuleConfigSO _inputPromptConfig)
    {
        _inputPromptData = promptData;
        _hoverEvent = _inputPromptConfig.inputPromptProvideEvent;
        _uiToggleEvent = _inputPromptConfig.uiToggleEvent;
    }

    public void OnHoverEnter()
    {
        _hoverEvent.Raise(GetInteractionData());
        _uiToggleEvent.Raise(new UIRequest(UIType.ActionGuide, UIInteractionSource.UIInternal, UIAction.Open));
    }

    public void OnHoverExit()
    {
        _uiToggleEvent.Raise(new UIRequest(UIType.ActionGuide, UIInteractionSource.UIInternal, UIAction.Close));
    }

    private IEnumerable<InteractionData> GetInteractionData()
    {
        foreach (InputPromptDataSO prompt in _inputPromptData)
        {
            yield return new InteractionData
            {
                icon = prompt.ActionSymbol,
                description = prompt.textContent.Get(LanguageUtility.CurrentLanguage).description
            };
        }
    }

}
