using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class ConveyorBeltStopper : MonoBehaviour
{

    [SerializeField] private bool _hasStoped;
    //[SerializeField] private TerminalStateEventSO _terminalStateEvent;
    //[SerializeField] private QuestGiveEventSO _questGiveEvent;
    [SerializeField] private ActionEventSO _ForceSetNewQuestactionEvent;

    [Header("Next Quest")]
    //[SerializeField] private Quest _quest;
    private BoxCollider _collider;
    private Rigidbody _rb;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _hasStoped = false;
        _collider = GetComponent<BoxCollider>();
        _collider.isTrigger = true;
    }


	private void OnTriggerEnter(Collider other)
	{

        if (other.TryGetComponent(out InteractableEntity interactableEntity))
        {
            if (interactableEntity.InteractionAction is PickupInteraction pickupInteraction)
            {

                if (pickupInteraction.PickableType == PickableType.Plank)
                {
                    Debug.Log("Stop Conveyor Enter");
		            other.TryGetComponent(out Rigidbody rb);
                    _rb = rb;

                    if (rb != null && !_hasStoped) 
                    {
                        Debug.Log("Stop Conveyor");
                        _ForceSetNewQuestactionEvent?.Raise();
                        _hasStoped = true;
                    }
                } 
            }
        }

	}

	//private void OnTriggerExit(Collider other)
	//{
 //       other.TryGetComponent<Rigidbody>(out Rigidbody rb);
 //       if(rb != null && rb == _rb) _rb = null;

	//}

	private void Reset()
	{
        _collider.isTrigger = true;
	}

}
