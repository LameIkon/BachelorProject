using UnityEngine;

public enum UIRuleType
{
    [Tooltip("Can stack with other stackables")]
    Stackable, 
    [Tooltip("Will close any stackables or solo ui elements and open this")]
    Solo, 
    [Tooltip("Will try to open when no stackable or solo elements are on the screen")]
    Overlay, 
    [Tooltip("Static Background. Will never be changed unless decided to hide hud")]
    HUD, 
    [Tooltip("Overrules all ui to close and show itself disabling all gameplay")]
    GameBlocking,
    [Tooltip("An ui that can pop up without closing other ui types but closes when in UI mode")]
    PopUp
}
