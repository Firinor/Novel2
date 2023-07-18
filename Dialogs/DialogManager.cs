using Puzzle;
using System.Threading;
using UnityEngine;
using Zenject;

namespace Dialog
{
    public class DialogManager : MonoBehaviour, ISceneController
    {
        [SerializeField]
        private int foregroundSortingOrder;
        [SerializeField]
        private int backgroundSortingOrder;

        [Inject]
        private GameObject dialog;
        [Inject]
        private DialogOperator dialogOperator;
        [Inject]
        private IReadingSceneManager sceneManager;
        [Inject]
        private Canvas backgroundCanvas;
        

        private CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();

        public bool IsCancellationRequested { get => cancellationTokenSource.IsCancellationRequested; }

        public void ActivateDialog(RectTransform dialogButtonRectTransform)
        {
            backgroundCanvas.sortingOrder = foregroundSortingOrder;
            //SceneManager.Add
            dialog.SetActive(true);
            cancellationTokenSource = new CancellationTokenSource();
            sceneManager.CheckMap(dialogButtonRectTransform);
        }

        public void StopDialog()
        {
            backgroundCanvas.sortingOrder = backgroundSortingOrder;
            cancellationTokenSource.Cancel();
        }

        public void SwithToPuzzle(InformationPackage informationPackage, string additional = "")
        {
            sceneManager.SwithToPuzzle(informationPackage, additional);
        }

        public void SetAllInstance()
        {
            throw new System.NotImplementedException();
        }

        public void Skip()
        {
            dialogOperator.DialogSkip();
        }

        public void Options()
        {
            sceneManager.SwitchPanelsToOptions();
        }

        public void Exit()
        {
            dialogOperator.DialogExit();
        }
    }
}
