public enum UIRuleType
{
    Stackable, // Can stack with others but removes single
    Solo, // Will close others and open this
    Overlay, // Will open and ignore all previous open ones
    HUD, // Static Background. Will never be changed unless decided to hide hud
}
