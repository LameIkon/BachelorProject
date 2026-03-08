using UnityEngine;

public class GameManager : MonoBehaviour
{
    private InputReader _input;
    public static Language s_language = Language.English;

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
    }

    // Update is called once per frame
    void Update()
    {

    }
}
