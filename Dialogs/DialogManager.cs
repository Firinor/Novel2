using Puzzle;
using System.Threading;
using UnityEngine;

namespace Dialog
{
    public class DialogManager : MonoBehaviour
    {
        [SerializeField]
        private int foregroundSortingOrder;
        [SerializeField]
        private int backgroundSortingOrder;

        private static DialogManager instance => (DialogManager)DialogHUB.DialogManager;
        private static GameObject dialog => DialogHUB.DialogObject;
        private static DialogOperator dialogOperator => (DialogOperator)DialogHUB.DialogOperator;
        private static IReadingSceneManager sceneManager => (IReadingSceneManager)ReadingRoomHUB.ReadingRoomManager;
        private static Canvas backgroundCanvas => BackgroundHUB.Canvas;
        

        private static CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();

        public static bool IsCancellationRequested { get => cancellationTokenSource.IsCancellationRequested; }

        public static void ActivateDialog(RectTransform dialogButtonRectTransform)
        {
            backgroundCanvas.sortingOrder = instance.foregroundSortingOrder;
            //SceneManager.Add
            dialog.SetActive(true);
            cancellationTokenSource = new CancellationTokenSource();
            sceneManager.CheckMap(dialogButtonRectTransform);
        }

        public static void StopDialog()
        {
            backgroundCanvas.sortingOrder = instance.backgroundSortingOrder;
            cancellationTokenSource.Cancel();
        }

        public static void Options()
        {
            sceneManager.SwitchPanelsToOptions();
        }
        public static void SwithToPuzzle(InformationPackage informationPackage, string additional = "")
        {
            sceneManager.SwithToPuzzle(informationPackage, additional);
        }
    }
}
