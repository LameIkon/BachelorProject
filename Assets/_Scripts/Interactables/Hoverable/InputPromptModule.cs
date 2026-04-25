using System.Collections.Generic;

public class InputPromptModule
{
    private readonly List<InputPromptDataSO> _inputPromptData;
    public InputPromptModule(List<InputPromptDataSO> promptData)
    {
        _inputPromptData = promptData;
    }


    public void OnHoverEnter()
    {


    }
    public void OnHoverExit()
    {

    }

    private InteractionData GetInteractionData()
    {
		InteractionData data = new InteractionData
		{
			//icon = _interactionDescriptionEvent?.ActionSymbol,
			//description = _interactionDescriptionEvent?.actionDescription,
		};

        return data;
    }

}
