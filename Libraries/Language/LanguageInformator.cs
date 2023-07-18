using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LanguageInformator : MonoBehaviour
{
    [SerializeField]
    private TMP_FontAsset RU_Front;
    [SerializeField]
    private TMP_FontAsset EN_Front;
    [SerializeField]
    private TMP_FontAsset CN_Front;

    // 0 - ������� ����
    // 1 - English language
    // 2 - ??
    private Dictionary<string, string[]> Texts
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

    public string GetText(string key)
    {
        return GetText(key, PlayerManager.Language);
    }

    private string GetText(string key, Languages language)
    {
        if(Texts.ContainsKey(key))
            return Texts[key][(int)language];

        throw new Exception($"No text value found by key {key}!");
    }

    public TMP_FontAsset GetFont()
    {
        switch (PlayerManager.Language)
        {
            case Languages.RU:
                return RU_Front;
            case Languages.EN:
                return EN_Front;
            case Languages.CN:
                return CN_Front;
            default:
                throw new Exception("");
        }
    }
}
