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

    // 0 - ������� ����
    // 1 - English language
    // 2 - ??
    private static Dictionary<string, string[]> Texts
        = new Dictionary<string, string[]>()
        {
            ["Play"] = 
            Words( "�����", "Start"),

            ["Puzzles"] =
            Words("�����", "Puzzles"),

            ["Options"] = 
            Words( "���������", "Options" ),

            ["Credits"] = 
            Words( "���������" , "Credits" ),

            ["Exit"] = 
            Words("�����" , "Exit" ),

            ["Return"] = 
            Words( "�������" , "Return" ),

            ["Creators"] = 
            Words("�����������: ��� ������� ��������\n\n" +
                "��������: ������ ��������, Rawen Black, ����� �����\n\n" +
                "����������: ������� �������" +
                "��������: ���������� �������� ���������",
                "Developer: sir Firinor Hisimeon\n\n" +
                "Narrative: Marina Knyshenko"),

            ["OptionsFullScreen"] = 
            Words( "���� �����" , "Full screen"),

            ["OptionsScreenResolution"] = 
            Words( "���������� ������" , "Screen resolution" ),

            ["OptionsVolume"] = 
            Words( "����" , "Volume" ),

            ["OptionsMouseSensitivity"] = 
            Words( "���������������� ����" , "Mouse sensitivity" ),

            ["OptionsLanguage"] = 
            Words( "����" , "Language" ),

            ["Empty"] =
            Words("�����", "Empty"),

            ["RestoreDefaults"] =
            Words("�������� �� ���������", "Restore defaults"),
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
