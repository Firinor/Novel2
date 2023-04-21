using UnityEngine;

public class HelpButtonsOperator : MonoBehaviour
{
    public void SkipButton()
    {
        SceneManager.SkipButton();
    }

    public void OptionsButton()
    {
        SceneManager.SwitchPanel(SceneDirection.options);
    }

    public void ExitButton()
    {
        SceneManager.SwitchPanel(SceneDirection.exit);
    }
}
