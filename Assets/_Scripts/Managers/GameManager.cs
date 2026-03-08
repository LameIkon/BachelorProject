using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("Events")]
    [SerializeField] private SwitchLanguageSO _switchLanguageSO;
    

    private InputReader _input;

    void Awake()
    {
        
    }
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {     
        if (_input == null)
        {
            _input = ScriptableObject.CreateInstance<InputReader>();
        } 

        _switchLanguageSO.Raise(Language.English);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
