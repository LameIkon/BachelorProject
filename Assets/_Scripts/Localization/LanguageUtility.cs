using System;

/// <summary>
/// Static class to handle language in the game. It contains a variable for currentLanguage and a method to set Language.
/// When language changes it will send an event to tell language change.
/// </summary>
public static class LanguageUtility
{
    public static Language CurrentLanguage { get; private set; } = Language.Dansk; // Default set to english

    public static event Action<Language> OnLanguageChanged;

    public static void SetLanguage(Language language)
    {
        if (language == CurrentLanguage) return;

        CurrentLanguage = language;
        OnLanguageChanged?.Invoke(language);
    }
}
