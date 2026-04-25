using System;
using UnityEngine;

/// <summary>
/// Module to control gameplay logic. Just reacts to inputs
/// </summary>
public class InteractionMenuModule
{
    private readonly InteractionMenuHandler _interactionMenuHandler;
    public InteractionMenuModule(InteractionMenuModuleConfigSO config, CompendiumID id)
    {
        _interactionMenuHandler = new InteractionMenuHandler(config.uiToggleEvent, config.compendiumPageEvent, id);
    }


    public void OnHoverEnter()
    {
        _interactionMenuHandler.OnHoverState(true);
    }

    public void OnHoverExit()
    {
        _interactionMenuHandler.OnHoverState(false);
    }
}

public class InputPromptModule
{
    private readonly InputPromptDataSO _inputPromptData;
    public InputPromptModule(InputPromptDataSO promptData)
    {
        _inputPromptData = promptData;
    }

}
