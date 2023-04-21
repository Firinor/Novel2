using FirEnum;
using FirStory;
using HelpBook;
using UnityEngine;

namespace Dialog
{
    public class StoryNode : DialogNode
    {
        [SerializeField, Min(1)]
        private int Act;
        [SerializeField, Min(1)]
        private int Scene;

        private Episode episode;
        private int incidentIndex;
        private int incidentCount;

        private IHelpBook helpBook => (IHelpBook)HelpBookHUB.HelpBookManager;

        protected new void Awake()
        {
            base.Awake();
            episode = StoryInformator.instance.GetScene(Act, Scene);
            incidentCount = episode.Count;
        }

        public override void StartDialog()
        {
            base.StartDialog();
            StartScene();
        }

        public async void StartScene()
        {
            StopDialogSkip();

            incidentIndex = 0;
            for (; incidentIndex < incidentCount;)
            {
                dialogOperator.CleareAll();
                dialogOperator.SetBackground(episode[incidentIndex].Background);
                dialogOperator.SetCharacters(episode[incidentIndex].Characters);
                if (IsInstantFunction(episode[incidentIndex].Function))
                {
                    //await nothing
                }
                else if (episode[incidentIndex].Function == "SplitScreen")
                {
                    await dialogOperator.AwaitPlayerInput();
                }
                else if (episode[incidentIndex].Function == StoryInformator.instance.characters.Narrator)
                {
                    await dialogOperator.NarratorText(episode[incidentIndex].Text);
                }
                else if (episode[incidentIndex].Function == StoryInformator.instance.characters.Silently)
                {
                    await dialogOperator.Say(episode[incidentIndex].Text);
                }
                else
                {
                    await dialogOperator.Say(episode[incidentIndex].Character, episode[incidentIndex].Text);
                }
                incidentIndex++;
            }

            Fork();
        }

        private bool IsInstantFunction(string function)
        {
            if(string.IsNullOrEmpty(function)) 
                return false;

            bool result = false;

            switch (function)
            {
                case "HelpBook":
                    helpBook.AddBookButton();
                    helpBook.AddPage(HelpBookPages.Title);
                    helpBook.AddPage(HelpBookPages.Exam);
                    result = true;
                    break;
                case "HelpBookPigpen":
                    helpBook.AddPage(HelpBookPages.Pigpen);
                    result = true;
                    break;
            }

            return result;
        }

        #region Non-removable garbage
        //private async Task IncidentAction()
        //{
        //    dialogOperator.CleareAll();
        //    dialogOperator.SetBackground(episode[incidentIndex].Background);
        //    dialogOperator.SetCharacters(episode[incidentIndex].Characters);

        //    string function = episode[incidentIndex].Function.ToLower();

        //    string[] splitFunction = function.Split(':');

        //    function = splitFunction[0];
        //    if (string.IsNullOrEmpty(function))
        //    {
        //        function = "say";
        //    }

        //    string additionalParameter = "";

        //    if (splitFunction.Length > 1)
        //    {
        //        additionalParameter = splitFunction[1];
        //    }

        //    MethodInfo[] methods = typeof(DialogOperator).GetMethods();
        //    //{
        //    //CleareAll
        //    //ShowImage
        //    //ShowText
        //    //SetBackground
        //    //ShowCharacter
        //    //HideCharacter
        //    //HideAllCharacters
        //    //Say
        //    //SayByName:Name
        //    //Choise:Number
        //    //}

        //    MethodInfo method = null;
        //    object[] _params = null;

        //    foreach (MethodInfo m in methods)
        //    {
        //        if(m.Name.ToLower() == function)
        //        {
        //            method = m;
        //            _params = GetParams(m, additionalParameter);
        //            break;
        //        }
        //    }

        //    if(method == null)
        //    {
        //        throw new Exception($"Function \"{function}\" not found");
        //    }

        //    await ReflectionMethods.InvokeAsync(method, dialogOperator, _params);
        //}
        //private object[] GetParams(MethodInfo method, string additionalParameter = "")
        //{
        //    ParameterInfo[] parameters = method.GetParameters();
        //    //{
        //    //string nameOnPlague
        //    //Sprite background
        //    //CharacterInformator character
        //    //CharacterEmotion emotion
        //    //PositionOnTheStage position
        //    //ViewDirection viewDirection
        //    //string[] text
        //    //}

        //    object[] result = null;
        //    if (parameters.Length > 0)
        //    {
        //        result = new object[method.GetParameters().Length];
        //    }

        //    for(int i = 0; i < parameters.Length; i++)
        //    {
        //        string parameterName = parameters[i].Name;
        //        switch (parameterName){
        //            case "nameOnPlague":
        //                result[i] = additionalParameter;
        //                break;
        //            case "background":
        //                result[i] = episode[incidentIndex].Background;
        //                break;
        //            case "character":
        //                result[i] = episode[incidentIndex].Character;
        //                break;
        //            case "emotion":
        //                result[i] = episode[incidentIndex].Background;
        //                break;
        //            case "position":
        //                result[i] = episode[incidentIndex].Background;
        //                break;
        //            case "viewDirection":
        //                result[i] = episode[incidentIndex].Background;
        //                break;
        //            case "text":
        //                result[i] = episode[incidentIndex].Text;
        //                break;
        //            default:
        //                throw new Exception($"parameterName \"{parameterName}\" not found");
        //        }
        //    }
        //    return result;
        //}
        #endregion

        public void Fork()
        {
            if (DialogManager.IsCancellationRequested)
            {
                StopDialogSkip();
                return;
            }

            if (Choices == null || Choices.Count < 1)
            {
                StopDialogSkip();
                dialogOperator.DialogExit();
                return;
            }

            if (episode != null && episode.Choices != null && episode.Choices.Count > 0)
            {
                StopDialogSkip();
                for (int i = 0; i < episode.Choices.Count; i++)
                {
                    dialogOperator.CreateWayButton(Choices[i], episode.Choices[i]);
                }
                return;
            }

            Choices[0].StartDialog();
        }

        [ContextMenu("Reset game object")]
        public override void ResetGameObject()
        {
            base.ResetGameObject();
            episode = FindObjectOfType<StoryInformator>().GetScene(Act, Scene);
            //Choices = new List<DialogNode>();
            for (int i = 0; i < episode.Choices.Count - Choices.Count; i++)
            {
                Choices.Add(null);
            }
        }
    }
}
