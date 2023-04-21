using UnityEngine;

namespace FirStory
{
    public interface ICharacters
    {
        public CharacterInformator None { get; }
        public CharacterInformator Main { get; }

        public string Narrator { get; }
        public string Silently { get; }

        public CharacterInformator[] Characters { get; }
    }
}
