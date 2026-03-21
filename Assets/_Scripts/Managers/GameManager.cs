using UnityEngine;

public class GameManager : Singleton<GameManager>
{

    [SerializeField] private InputReader _input;

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
