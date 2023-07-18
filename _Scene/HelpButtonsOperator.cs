using UnityEngine;
using Zenject;

public class HelpButtonsOperator : MonoBehaviour
{
    [Inject]
    private SceneManager sceneManager;

    public void SkipButton()
    {
        sceneManager.SkipButton();
    }

    public void OptionsButton()
    {
        sceneManager.SwitchPanel(SceneDirection.options);
    }

    public void ExitButton()
    {
        sceneManager.SwitchPanel(SceneDirection.exit);
    }
}
