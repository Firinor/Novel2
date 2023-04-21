using System;

public enum Languages { RU, EN, CN }

public static class LanguageManager
{
    public static Action OnLanguageChange;

    public static void ChangeLanguage()
    {
        OnLanguageChange?.Invoke();
    }
}
