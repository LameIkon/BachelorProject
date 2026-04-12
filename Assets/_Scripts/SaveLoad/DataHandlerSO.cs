using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="Data Handler SO", menuName = "ScriptableObject/SaveLoad/Data Handler")]
public class DataHandlerSO : ScriptableObject
{
    [SerializeField] private RegisterSaveDataEventSO _registerSaveDataEvent;
    [SerializeField] private StoreDataEventSO _getDataEvent;
    [SerializeField] private List<EventRecordBuilderSO> _recordBuilders;


    private DataHandler _dataHandler;

    public DataHandler DataHandler => _dataHandler;
        
    public void Initialize()
    {
        if (_registerSaveDataEvent == null || _getDataEvent == null) return;
        _dataHandler = new DataHandler(_registerSaveDataEvent, _getDataEvent, _recordBuilders);
    }
}