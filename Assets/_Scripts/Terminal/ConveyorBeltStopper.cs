using UnityEngine;

[RequireComponent(typeof(Collider))]
public class ConveyorBeltStopper : MonoBehaviour
{

    [SerializeField] private bool _hasStoped;
    [SerializeField] private TerminalStateEventSO _terminalStateEvent;
    [SerializeField] private QuestGiveEventSO _questGiveEvent;
    [SerializeField] private Collider _collider;
    [SerializeField] private Rigidbody _rb;

    [SerializeField] private Quest _quest;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _hasStoped = false;
    }


	private void OnTriggerEnter(Collider other)
	{
        Debug.Log("Stop Conveyor Enter");
		other.TryGetComponent<Rigidbody>(out Rigidbody rb);
        _rb = rb;
        if (rb != null && !_hasStoped) 
        {
            Debug.Log("Stop Conveyor");
            _questGiveEvent.Raise(_quest);
            _hasStoped = true;
        }
	}

	private void OnTriggerExit(Collider other)
	{
        other.TryGetComponent<Rigidbody>(out Rigidbody rb);
        if(rb != null && rb == _rb) _rb = null;

	}

	private void Reset()
	{
        _collider = GetComponent<Collider>();
        _collider.isTrigger = true;
	}

}
