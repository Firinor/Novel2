using FirUnityEditor;
using UnityEngine;

namespace Dialog
{
    public class DialogHUBInformator : MonoBehaviour
    {
        [SerializeField, NullCheck]
        private GameObject DialogObject;
        [SerializeField, NullCheck]
        private MonoBehaviour DialogManager;
        [SerializeField, NullCheck]
        private MonoBehaviour DialogOperator;
        [SerializeField, NullCheck]
        private Canvas Canvas;

        public void Awake()
        {
            DialogHUB.DialogObjectCell.SetValue(DialogObject);
            DialogHUB.DialogManagerCell.SetValue(DialogManager);
            DialogHUB.DialogOperatorCell.SetValue(DialogOperator);
            DialogHUB.CanvasCell.SetValue(Canvas);
            Destroy(this);
        }
    }
}
