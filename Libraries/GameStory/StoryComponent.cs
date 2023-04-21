using System;
using System.Collections.Generic;
using UnityEngine;

namespace FirStory
{
    [Serializable]
    public class StoryComponent
    {
        public int Scene;
        public string Function;
        public Sprite Background;
        public CharacterInformator Character;
        public List<CharacterStatus> Characters;
        public MultiText Text;
        public StoryComponent(int scene, string function, Sprite background, CharacterInformator character,
            List<CharacterStatus> characters, MultiText text)
        {
            Scene = scene;
            Function = function;
            Background = background;
            Character = character;
            Characters = new List<CharacterStatus>(characters.ToArray());
            Text = text;
        }

        public void AddCharacter(CharacterStatus characterStatus)
        {
            if (Characters == null)
            {
                Characters = new List<CharacterStatus>();
            }

            int characterIndex = -1;
            for (int i = 0; i < Characters.Count; i++)
            {
                if (Characters[i].Character == characterStatus.Character)
                {
                    characterIndex = i;
                    break;
                }
            }

            if (characterIndex != -1)
            {
                Characters[characterIndex] = characterStatus;
            }
            else
            {
                Characters.Add(characterStatus);
            }
        }

        public void RemoveCharacter(CharacterInformator Character)
        {
            for (int i = 0; i < Characters.Count; i++)
            {
                if (Characters[i].Character == Character)
                {
                    Characters.RemoveAt(i);
                    break;
                }
            }
        }
    }
}