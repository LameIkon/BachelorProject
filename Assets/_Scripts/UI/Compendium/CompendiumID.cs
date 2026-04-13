/// <summary>
/// Uniq id for each page in the compendium
/// </summary>
public enum CompendiumID
{
    None,

    // Oven
    oven,
    StartProtocol,
    EndProtocol,

    // Buttons
    StartButton,
    StopButton,
    ResetButton1,
    ResetButton2,
    ResetButton3,
    SpeedIncreaseButton,
    SpeedDecreaseButton,
    Lever,


    // Terminals & Panels
    MainTerminal,
    EndTerminal,
    LeverTerminal,
    RestartPanel,
    ControlPanel,

    // Errors
    OccurenceOfError,
    OvenError,
    SensorError,

    // Equipment
    Mask,
    Gloves,
    DryiceBlower,
    DryiceHose,
    Airhose,

    // Materials
    Dryice,
    Plank,

}
