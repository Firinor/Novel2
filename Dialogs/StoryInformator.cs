using FirStory;
using FirUnityEditor;
using System;
using UnityEditor.SceneManagement;
using UnityEngine;

namespace Dialog
{
    public class StoryInformator : SinglBehaviour<StoryInformator>
    {
        public Characters characters;
        public Backgrounds backgrounds;
        [SerializeField]
        private TextAsset[] storyFiles;
        [SerializeField]
        private FullStory Story;

        private const int StoryActCount = 7;

        public Episode GetScene(int act, int scene)
        {
            if (act > StoryActCount || act < 1)
                throw new Exception($"There is no act with number \"{act}\" in the history!");

            if (scene > Story[act - 1].Count || scene < 1)
                throw new Exception($"There is no scene with number \"{scene}\" in the act \"{act}\"!");

            return Story[act - 1][scene - 1];
        }

        [Serializable]
        public class Characters : ICharacters
        {
            public string narrator;
            public string silently;
            public string Narrator => narrator;
            public string Silently => silently;
            [NullCheck]
            public CharacterInformator mainHero;
            CharacterInformator ICharacters.Main => mainHero;

            [NullCheck]
            public CharacterInformator Error;
            CharacterInformator ICharacters.None => Error;

            CharacterInformator[] ICharacters.Characters => characters;

            [NullCheck]
            public CharacterInformator[] characters;
        }

        [Serializable]
        public class Backgrounds : IBackgrounds
        {
            [NullCheck]
            public Sprite none;
            Sprite IBackgrounds.None => none;

            [NullCheck]
            public Sprite[] backgrounds;
            Sprite[] IBackgrounds.Backgrounds => backgrounds;
        }

        void Awake()
        {
            SingletoneCheck(this);
        }

        [ContextMenu("ReadFullStory")]
        private void GetStory()
        {
            Story = StoryReader.GetFullStory(storyFiles, characters, backgrounds);
            EditorSceneManager.MarkSceneDirty(EditorSceneManager.GetActiveScene());
        }
    }
}