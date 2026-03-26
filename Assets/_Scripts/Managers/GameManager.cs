using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    private CrosshairHandler _crosshairHandler;

    [SerializeField] private InputReader _input;

    protected override void Awake()
    {
        base.Awake();
        _crosshairHandler = new CrosshairHandler();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {     
        if (_input == null)
        {
            //_input = ScriptableObject.CreateInstance<InputReader>();
        } 
    }

    // Update is called once per frame
    void Update()
    {

    }
}
