using System.Collections.Generic;

public static class PlayerManager
{
    public static int Account;

    private static Languages language;
    private static Dictionary<int, bool> progress;
    public static Languages Language
    {
        get
        {
            return language;
        }
        set
        {
            language = value;
            LanguageManager.ChangeLanguage();
        }
    }
    public static Dictionary<int, bool> GetProgress()
    {
        if(progress == null)
        {
            progress = new Dictionary<int, bool>();
        }
        return progress;
    }

    public static void SetProgress()
    {
        //progress.Add(dialogNode.ID, true);
        SaveManager.Save(Account);
    }

    public static void OnLoad()
    {
        Account = SaveManager.PlayerAccount();
    }
}
