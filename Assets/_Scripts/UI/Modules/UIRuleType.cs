public enum UIRuleType
{
    Stackable, // Can stack with others but removes single
    Solo, // Will close others and open this
    Overlay, // Will try to open when no stackable or solo elements are on the screen
    HUD, // Static Background. Will never be changed unless decided to hide hud
}
