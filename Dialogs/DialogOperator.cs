using FirStory;
using FirUnityEditor;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Image = UnityEngine.UI.Image;

namespace Dialog
{
    public class DialogOperator : MonoBehaviour
    {
        #region Fields
        [SerializeField, NullCheck]
        private GameObject speakerPrefab;
        [SerializeField, NullCheck]
        private GameObject buttonPrefab;
        [SerializeField, Min(10)]
        private int textButtonCapacity;

        [SerializeField, NullCheck]
        private GameObject buttonParent;
        [SerializeField, NullCheck]
        private GameObject plaqueWithTheName;

        [Space]
        [SerializeField, NullCheck]
        private RectTransform rectTransform;

        private static DialogOperator instance => (DialogOperator)DialogHUB.DialogOperator;
        private static Canvas canvas => DialogHUB.Canvas;
        private static Button backgroundButton => BackgroundHUB.Button;
        private static Image background => BackgroundHUB.Image;

        [SerializeField, NullCheck]
        private Sprite defaultSprite;

        [SerializeField, NullCheck]
        private GameObject leftSpeaker;
        [SerializeField, NullCheck]
        private GameObject centerSpeaker;
        [SerializeField, NullCheck]
        private GameObject rightSpeaker;

        [SerializeField, NullCheck]
        private TextMeshProUGUI sceneName;
        [SerializeField, NullCheck]
        private TextMeshProUGUI speakerName;
        [SerializeField, NullCheck]
        private TextMeshProUGUI textMeshPro;
        [SerializeField, Min(10)]
        private int textCapacity;
        [SerializeField, NullCheck]
        private TextMeshProUGUI textInCenterOfScreen;
        [SerializeField, Min(10)]
        private int textOfScreenCapacity;

        [SerializeField, NullCheck]
        private GameObject textCanvas;
        //[SerializeField, NullCheck]
        //private MapCanvasOperator mapCanvasOperator;
        [SerializeField]
        private float lettersDelay;
        //Delay after full line output
        private int fullLineDelay = 7;
        private int secondsMultipler = 1000;
        [SerializeField, NullCheck]
        private Image nextArrow;

        private StringBuilder strindBuilder = new StringBuilder();
        private bool SwichLanguage;
        private string[] PrintableText;
        private Dictionary<CharacterInformator, SpeakerOperator> characters
            = new Dictionary<CharacterInformator, SpeakerOperator>();
        private SpeakerOperator activeCharacter;

        private static bool skipText;
        private static bool nextInput;

        public static void NextInput() => nextInput = true;
        public static float RectTransformHeight { get { return instance.rectTransform.rect.height; } }
        public static int OrderLayer { get { return canvas.sortingOrder; } }
        public static GameObject Left { get { return instance.leftSpeaker; } }
        public static GameObject Center { get { return instance.centerSpeaker; } }
        public static GameObject Right { get { return instance.rightSpeaker; } }
        #endregion

        #region Monobehaviour
        void Awake()
        {
            backgroundButton.onClick.RemoveAllListeners();
            backgroundButton.onClick.AddListener(NextInput);
            LanguageManager.OnLanguageChange += ResetText;
        }
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.RightArrow)
                || Input.GetKeyDown(KeyCode.Space)
                || Input.GetKeyDown(KeyCode.KeypadEnter)
                || Input.GetKeyDown(KeyCode.Return))
            {
                NextInput();
            }
        }
        #endregion

        public void CleareAll()
        {
            ClearAllText();
            OffBackground();
            HideAllCharacters();
            HideAllWays();
            //music off
        }
        public void SetSceneName(string name)
        {
            sceneName.text = name;
        }

        #region Text
        private void HideText()
        {
            textCanvas.SetActive(false);
        }
        private void ShowText()
        {
            textCanvas.SetActive(true);
        }
        private void ClearAllText()
        {
            textMeshPro.text = "";
            textInCenterOfScreen.text = "";
        }
        private void ResetText()
        {
            SwichLanguage = true;
            ResetPlaqueName();
        }
        public void SetLettersDelay(float delta)
        {
            lettersDelay = delta;
        }
        public async Task PrintText(MultiText text, bool senterScreen = false)
        {
            int textComponentCapacity = senterScreen ? textOfScreenCapacity : textCapacity;

            PrintableText = TextByLanguage(text, textComponentCapacity);

            TextMeshProUGUI textComponent = senterScreen ? textInCenterOfScreen : textMeshPro;

            for (int textPart = 0; textPart < PrintableText.Length; textPart++)
            {
                nextInput = false;
                nextArrow.enabled = false;

                strindBuilder.Clear();

                if (senterScreen)
                {
                    ContentSizeFitter sizeFitter = textComponent.GetComponent<ContentSizeFitter>();
                    if (sizeFitter != null)
                    {
                        textComponent.text = PrintableText[textPart];
                        sizeFitter.SetLayoutVertical();
                    }
                }

                textComponent.text = strindBuilder.ToString();

                for (int i = 0; i < PrintableText[textPart].Length + fullLineDelay; i++)
                {
                    if (i < PrintableText[textPart].Length)
                    {
                        strindBuilder.Append(PrintableText[textPart][i]);
                        textComponent.text = strindBuilder.ToString();
                    }
                    if (skipText)
                    {
                        break;
                    }
                    if (nextInput)
                    {
                        i = PrintableText[textPart].Length + fullLineDelay;
                        nextInput = false;
                    }
                    if (DialogManager.IsCancellationRequested)
                        break;
                    if (SwichLanguage)
                    {
                        SwichLanguage = false;
                        textPart = 0;
                        i = 0;
                        PrintableText = TextByLanguage(text, textComponentCapacity);
                    }
                    await Task.Delay((int)(lettersDelay * secondsMultipler));
                }
                textComponent.text = PrintableText[textPart];
                nextArrow.enabled = true;
                while (!nextInput)
                {
                    if (skipText)
                    {
                        break;
                    }
                    if (SwichLanguage)
                    {
                        SwichLanguage = false;
                        string[] newText = TextByLanguage(text, textComponentCapacity);
                        textComponent.text = newText[newText.Length - 1];
                    }
                    if (DialogManager.IsCancellationRequested)
                        break;
                    await Task.Yield();
                }
            }
            //ClearAllText();
        }
        private static string[] TextByLanguage(MultiText text, int textCapacity)
        {
            string fullText = FullTextByLanguage(text);

            return GameStory.AlignTheStoryLengthwise(fullText, textCapacity);
        }
        private static string FullTextByLanguage(MultiText text)
        {
            if (text == null)
                throw new Exception("The link to the text was not found!");
            if (text.Length == 0)
                throw new Exception("The text to output is empty!");
            if (text.Length <= (int)PlayerManager.Language)
                throw new Exception("Not all languages are filled in the text!");

            return text[(int)PlayerManager.Language];
        }
        #endregion

        #region Speakers
        public Task Say(CharacterInformator character, MultiText text)
        {
            if (DialogManager.IsCancellationRequested)
                return Task.CompletedTask;

            SetPlaqueName(character);
            SetActiveCharacter(character);
            return PrintText(text);
        }
        public Task Say(MultiText text)
        {
            if (DialogManager.IsCancellationRequested)
                return Task.CompletedTask;

            SetPlaqueName();
            return PrintText(text);
        }
        private Task PrintText(MultiText text)
        {
            if (DialogManager.IsCancellationRequested)
                return Task.CompletedTask;

            return PrintText(text, senterScreen: false);
        }
        public void HideCharacter(CharacterInformator character)
        {
            SpeakerOperator speakerOperator = null;

            if (characters.ContainsKey(character))
            {
                speakerOperator = characters[character];
                characters.Remove(character);
            }

            if (speakerOperator != null)
                Destroy(speakerOperator.gameObject);
        }
        public void HideAllCharacters()
        {
            plaqueWithTheName.SetActive(false);
            characters.Clear();

            List<SpeakerOperator> gameObjectToDelete = new List<SpeakerOperator>();
            AllSpeakersIn(gameObjectToDelete, leftSpeaker);
            AllSpeakersIn(gameObjectToDelete, centerSpeaker);
            AllSpeakersIn(gameObjectToDelete, rightSpeaker);

            foreach (SpeakerOperator speaker in gameObjectToDelete)
            {
                Destroy(speaker.gameObject);
            }
        }
        private void AllSpeakersIn(List<SpeakerOperator> gameObjectToDelete, GameObject side)
        {
            foreach (SpeakerOperator gameObject in side.GetComponentsInChildren<SpeakerOperator>())
            {
                gameObjectToDelete.Add(gameObject);
            }
        }
        public void ShowCharacter(CharacterInformator character)
        {
            if (character == null)
                return;

            if (!characters.ContainsKey(character))
            {
                var speakerOperator = Instantiate(speakerPrefab, GetSpeakerParent()).GetComponent<SpeakerOperator>();
                speakerOperator.SetCharacter(character);
                characters.Add(character, speakerOperator);
            }
        }
        public void SetCharacters(List<CharacterStatus> characters)
        {
            if (characters == null)
                return;

            for (int i = 0; i < characters.Count; i++)
            {
                ShowCharacter(characters[i].Character);
                SetPosition(characters[i].Character, characters[i].Position, characters[i].ViewDirection);
            }
        }
        public void SetPosition(CharacterInformator character, PositionOnTheStage position, ViewDirection viewDirection)
        {
            if (character == null)
                return;

            if (!characters.ContainsKey(character))
                return;

            if (viewDirection == ViewDirection.Default)
                viewDirection = GetDefaultViewDirection(position);

            characters[character].transform.SetParent(GetSpeakerParent(position));
            characters[character].transform.localScale = new Vector3((int)viewDirection * (int)character.ViewDirection, 1, 1);
        }
        private ViewDirection GetDefaultViewDirection(PositionOnTheStage position)
        {
            if (position == PositionOnTheStage.Right)
                return ViewDirection.Left;

            return ViewDirection.Right;
        }
        public void SetActiveCharacter(CharacterInformator character)
        {
            if (character == null || !characters.ContainsKey(character))
                SetActiveCharacter((SpeakerOperator)null);
            else
                SetActiveCharacter(characters[character]);
        }
        public void SetActiveCharacter(SpeakerOperator character)
        {
            if (activeCharacter == character)
                return;
            DeactiveCharacter();
            if (character == null)
                return;
            if (!characters.ContainsValue(character))
                return;

            activeCharacter = character;
            activeCharacter.ToTheForeground();
        }
        public void DeactiveCharacter()
        {
            if (activeCharacter != null)
            {
                activeCharacter.ToTheBackground();
                activeCharacter = null;
            }
        }
        public void SetPlaqueName(CharacterInformator character = null)
        {
            if (character == null)
            {
                plaqueWithTheName.SetActive(false);
                return;
            }

            plaqueWithTheName.SetActive(true);
            speakerName.text = character.Name;
        }
        protected void ResetPlaqueName()
        {
            if (activeCharacter != null)
            {
                SetPlaqueName(activeCharacter.characterInformator);
            }
        }
        private Transform GetSpeakerParent(PositionOnTheStage position = PositionOnTheStage.Center)
        {
            return position switch
            {
                PositionOnTheStage.Left => leftSpeaker.transform,
                PositionOnTheStage.Right => rightSpeaker.transform,
                PositionOnTheStage.Center => centerSpeaker.transform,
                _ => centerSpeaker.transform,
            };
        }
        #endregion

        #region Background
        public void SetBackground(Sprite background)
        {
            if (background != null)
            {
                DialogOperator.background.enabled = true;
                DialogOperator.background.sprite = background;
            }

        }
        public async Task NarratorText(MultiText text = null)
        {
            StopDialogSkip();
            nextInput = false;
            HideAllCharacters();
            HideText();
            while (!nextInput)
            {
                if (skipText)
                    break;

                if (DialogManager.IsCancellationRequested)
                    break;

                await Task.Yield();
            }

            if (text != null && !String.IsNullOrWhiteSpace(text[0]))
            {
                await PrintText(text, senterScreen: true);
            }
            OffBackground();
            ShowText();
        }
        public void OffBackground()
        {
            if (background != null)
                background.enabled = false;
        }
        #endregion

        #region Ways
        public void HideAllWays()
        {
            var childs = buttonParent.GetComponentsInChildren<DialogButtonOperator>();

            foreach (DialogButtonOperator child in childs)
            {
                Destroy(child.gameObject);
            }
        }
        internal void CreateWayButton(DialogNode dialogNode, MultiText multiText)
        {
            GameObject button = Instantiate(buttonPrefab, buttonParent.transform);
            button.GetComponent<DialogButtonOperator>()
                .SetWay(dialogNode, FullTextByLanguage(multiText));
        }
        #endregion

        public void StopDialogSkip() => skipText = false;
        public void DialogSkip() => skipText = true;
        public void Options() => DialogManager.Options();
        public void DialogExit()
        {
            DialogManager.StopDialog();
            OffBackground();
            gameObject.SetActive(false);
        }

        public async Task AwaitPlayerInput()
        {
            await NarratorText();
        }
    }
}
