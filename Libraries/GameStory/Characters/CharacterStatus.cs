using FirParser;
using System;

namespace FirStory
{
    [Serializable]
    public class CharacterStatus
    {
        public CharacterInformator Character;
        public CharacterEmotion Emotion;
        public ViewDirection ViewDirection;
        public PositionOnTheStage Position;

        public CharacterStatus(CharacterInformator character, string position, string direction,
            string emotion)
        {
            Character = character;
            Position = StringParser.ParseTo<PositionOnTheStage>(position);
            ViewDirection = StringParser.ParseTo<ViewDirection>(direction);
            Emotion = StringParser.ParseTo<CharacterEmotion>(emotion);
        }
    }
}
