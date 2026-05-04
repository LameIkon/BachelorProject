using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Light))]
public class ButtonLight : MonoBehaviour
{

    [SerializeField] private ColorReferece _onColor;
    [SerializeField] private float _intensity;

    private Light _light;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _light = GetComponent<Light>();   
        //_meshRenderer = GetComponent<MeshRenderer>();
        _light.color = _onColor;
        TurnLight(false);
        _waitForSeconds = new WaitForSeconds(3);
        //StartCoroutine(TurnOnOff());
        //_meshRenderer.material.color = new Color(_onColor.GetColor.r, _onColor.GetColor.g, _onColor.GetColor.b, 0.2f);
    }


    public void TurnLight(bool on)
    {
        if (on)
        {
            _light.intensity = _intensity;
            _light.enabled = true;
        }
        else 
        {
            _light.intensity = 0;
            _light.enabled = false;
        }

    }

    private WaitForSeconds _waitForSeconds;

    private IEnumerator TurnOnOff() 
    {
        bool isOn = false;
        for (int i = 0; i < 100; i++) 
        {
            isOn = !isOn;
            TurnLight(isOn);
            yield return _waitForSeconds;
        }
    
    }

}
