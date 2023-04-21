using Dialog;
using UnityEngine;

public class LanguageDialogButtonOperator : LanguageOperator
{
    [SerializeField]
    private DialogNode dialogNode;

    void Awake()
    {
        dialogNode = GetComponentInParent<DialogNode>();
    }

    protected override void SetText()
    {
        Text.text = dialogNode.GetHeader();
    }
}
