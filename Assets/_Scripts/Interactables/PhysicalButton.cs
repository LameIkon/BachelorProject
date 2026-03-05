using UnityEngine;

public sealed class PhysicalButton : MonoBehaviour, IInteractable
{

    #region Unity Methods
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    #endregion

    #region Interact interface
    public void Interact()
    {
        throw new System.NotImplementedException();
    }

    #endregion
}
