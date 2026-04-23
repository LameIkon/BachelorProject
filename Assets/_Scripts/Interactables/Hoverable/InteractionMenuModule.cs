using System;
using UnityEngine;

public class InteractionMenuModule : IHoverable
{
    private readonly InteractionMenuHandler _interactionMenuHandler;
    public InteractionMenuModule(InteractionMenuModuleConfigSO config)
    {

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
