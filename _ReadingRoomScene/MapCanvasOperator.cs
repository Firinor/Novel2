using FirEnum;
using FirUnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class MapCanvasOperator : MonoBehaviour
{
    [SerializeField, NullCheck]
    private GameObject socialBackground;
    private RectTransform viewPort;
    private float width;
    
    [SerializeField, NullCheck]
    private Scrollbar horizontalScrollbar;
    

    void Awake()
    {
        viewPort = socialBackground.GetComponent<RectTransform>();
        width = viewPort.sizeDelta.x;
    }

    public void Options() => ReadingRoomManager.SwitchPanels(ReadingRoomMarks.options);
    public void Exit() => SceneManager.SwitchPanel(SceneDirection.exit);

    public void CorrectScrollbarPosition(RectTransform dialogButtonRectTransform)
    {
        float dialogButtonXPosition = GetRecurrenceXPosition(dialogButtonRectTransform);
        horizontalScrollbar.value = dialogButtonXPosition / (width-Screen.width/2);
    }

    private float GetRecurrenceXPosition(RectTransform dialogButtonRectTransform)
    {
        if(dialogButtonRectTransform != viewPort)
        {
            return dialogButtonRectTransform.offsetMin.x 
                + GetRecurrenceXPosition(dialogButtonRectTransform.parent as RectTransform);
        }
        return 0;
    }
}
