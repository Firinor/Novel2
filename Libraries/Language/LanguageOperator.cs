using TMPro;
using UnityEngine;
using Zenject;

[RequireComponent(typeof(TextMeshProUGUI))]
public class LanguageOperator : MonoBehaviour
{
    [SerializeField]
    private string Key;
    protected TextMeshProUGUI Text;

    [Inject]
    private LanguageInformator languageInformator;

    void Awake()
    {
        if(Text == null)
            Text = GetComponent<TextMeshProUGUI>();

        LanguageManager.OnLanguageChange += SetText;
        SetText();
    }

    void OnDestroy()
    {
        LanguageManager.OnLanguageChange -= SetText;
    }

    protected virtual void SetText()
    {
        Text.font = languageInformator.GetFont();
        string text = languageInformator.GetText(Key);
        if(text != null)
            Text.text = text;
    }
}
