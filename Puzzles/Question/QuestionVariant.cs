using System;

namespace Puzzle.TetraQuestion
{
    [Serializable]
    public class QuestionVariant
    {
        public Question[] Variants;

        public int Length => Variants.Length;
    }
}
