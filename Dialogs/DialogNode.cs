using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public enum DialogProgressStatus { Open, Closed, Hiden }

namespace Dialog
{
    public class DialogNode : MonoBehaviour
    {
        [SerializeField]
        private string[] Header = new string[2] { "������� ��������", "english name" };
        [SerializeField]
        protected List<DialogNode> Choices;
        private DialogProgressStatus dialogProgressStatus = DialogProgressStatus.Hiden;

        [Inject]
        protected DialogManager dialogManager;
        [Inject]
        protected DialogOperator dialogOperator;
        [Inject]
        protected StoryInformator storyInformator;

        protected StoryInformator.Characters Characters;
        protected StoryInformator.Backgrounds Backgrounds;

        protected void Awake()
        {
            Characters = storyInformator.characters;
            Backgrounds = storyInformator.backgrounds;
            GetComponent<Button>().onClick.AddListener(StartDialog);
        }
        public void SetChoice(DialogNode dialogNode)
        {
            if (dialogNode == null)
                return;

            Choices = new List<DialogNode>() { dialogNode };
        }
        public string GetHeader()
        {
            return Header[(int)PlayerManager.Language];
        }
        public virtual void StartDialog()
        {
            dialogManager.ActivateDialog(GetComponent<RectTransform>());
            dialogOperator.CleareAll();
            dialogOperator.SetSceneName(name);
        }

        public void StartDialog(int index)
        {
            if (Choices != null && Choices.Count > index)
                Choices[index]?.StartDialog();
        }

        protected void StopDialogSkip()
        {
            dialogOperator.StopDialogSkip();
        }

        public virtual void ResetGameObject()
        {
            GetComponentInChildren<TextMeshProUGUI>().text = gameObject.name;
        }
    }
}
