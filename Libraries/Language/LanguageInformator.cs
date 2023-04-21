using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LanguageInformator : SinglBehaviour<LanguageInformator>
{
    [SerializeField]
    private TMP_FontAsset RU_Front;
    [SerializeField]
    private TMP_FontAsset EN_Front;
    [SerializeField]
    private TMP_FontAsset CN_Front;

    void Awake()
    {
        SingletoneCheck(this);
    }

    // 0 - Русский язык
    // 1 - English language
    // 2 - ??
    private static Dictionary<string, string[]> Texts
        = new Dictionary<string, string[]>()
        {
            ["Play"] = 
            Words( "Старт", "Start"),

            ["Puzzles"] =
            Words("Пазлы", "Puzzles"),

            ["Options"] = 
            Words( "Настройки", "Options" ),

            ["Credits"] = 
            Words( "Создатели" , "Credits" ),

            ["Exit"] = 
            Words("Выход" , "Exit" ),

            ["Return"] = 
            Words( "Возврат" , "Return" ),

            ["Creators"] = 
            Words("Разработчик: сир Фиринор Хисимеон\n\n" +
                "Сценарий: Марина Кнышенко, Rawen Black, Дарья Лукас\n\n" +
                "Композитор: Евгений Пугачев" +
                "Художник: Александра Олеговна Иваницкая",
                "Developer: sir Firinor Hisimeon\n\n" +
                "Narrative: Marina Knyshenko"),

            ["OptionsFullScreen"] = 
            Words( "Весь экран" , "Full screen"),

            ["OptionsScreenResolution"] = 
            Words( "Расширение экрана" , "Screen resolution" ),

            ["OptionsVolume"] = 
            Words( "Звук" , "Volume" ),

            ["OptionsMouseSensitivity"] = 
            Words( "Чувствительность мыши" , "Mouse sensitivity" ),

            ["OptionsLanguage"] = 
            Words( "Язык" , "Language" ),

            ["Empty"] =
            Words("Пусто", "Empty"),

            ["RestoreDefaults"] =
            Words("Сбросить по умолчанию", "Restore defaults"),
        };

    private static string[] Words(string ru, string en)
    {
        return new string[] { ru, en };
    }

    public static string GetText(string key)
    {
        return GetText(key, PlayerManager.Language);
    }

    private static string GetText(string key, Languages language)
    {
        if(Texts.ContainsKey(key))
            return Texts[key][(int)language];

        throw new Exception($"No text value found by key {key}!");
    }

    public static TMP_FontAsset GetFont()
    {
        switch (PlayerManager.Language)
        {
            case Languages.RU:
                return instance.RU_Front;
            case Languages.EN:
                return instance.EN_Front;
            case Languages.CN:
                return instance.CN_Front;
            default:
                throw new Exception("");
        }
    }
}
