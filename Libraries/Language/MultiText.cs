using System;

[Serializable]
public class MultiText
{
    public string[] Text;

    public MultiText(int count)
    {
        Text = new string[count];
    }

    public static implicit operator string[](MultiText multiText)
    {
        return multiText.Text;
    }

    public string this[int index]
    {
        get => Text[index];
        set => Text[index] = value;
    }

    public int Length => Text.Length;
}
