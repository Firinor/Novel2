using TMPro;
using UnityEngine;

[RequireComponent(typeof(TextMeshProUGUI))]
public class LanguageOperator : MonoBehaviour
{
    [SerializeField]
    private string Key;
    protected TextMeshProUGUI Text;

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
        Text.font = LanguageInformator.GetFont();
        string text = LanguageInformator.GetText(Key);
        if(text != null)
            Text.text = text;
    }
}
