using UnityEngine;

public class ConveyorBeltStopper : MonoBehaviour
{

    [SerializeField] private bool _hasStoped;
    [SerializeField] private TerminalStateEventSO _terminalStateEvent;
    [SerializeField] private QuestGiveEventSO _questGiveEvent;
    [SerializeField] private Rigidbody _rb;

    [SerializeField] private Quest _quest;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _hasStoped = false;
    }


	private void OnCollisionEnter(Collision collision)
	{
        Debug.Log("Stop Conveyor Enter");
		collision.collider.TryGetComponent<Rigidbody>(out Rigidbody rb);
        _rb = rb;
        if (rb != null && !_hasStoped) 
        {
            Debug.Log("Stop Conveyor");
            _questGiveEvent.Raise(_quest);
            _hasStoped = true;
        }
	}

	private void OnCollisionExit(Collision collision)
	{
        collision.collider.TryGetComponent<Rigidbody>(out Rigidbody rb);
        if(rb != null && rb == _rb) _rb = null;

	}

}
