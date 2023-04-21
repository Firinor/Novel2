using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public enum DialogProgressStatus { Open, Closed, Hiden }

namespace Dialog
{
    public class DialogNode : MonoBehaviour
    {
        [SerializeField]
        private string[] Header = new string[2] { "русское название", "english name" };
        [SerializeField]
        protected List<DialogNode> Choices;
        private DialogProgressStatus dialogProgressStatus = DialogProgressStatus.Hiden;

        protected static DialogOperator dialogOperator => (DialogOperator)DialogHUB.DialogOperator;
        
        protected StoryInformator.Characters Characters;
        protected StoryInformator.Backgrounds Backgrounds;

        protected void Awake()
        {
            StoryInformator storyInformator = StoryInformator.instance;
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
            DialogManager.ActivateDialog(GetComponent<RectTransform>());
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
