using FirMath;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Puzzle.TetraQuestion
{
    [Serializable]
    public class QuestionQueue
    {
        [Min(1)]
        public int correctAnswerNeededCount = 1;
        public List<QuestionVariant> questionsQueue;

        public Question First => GetQuestionFromVariant(0);

        public int Length => questionsQueue.Count;

        public Question GetQuestionFromVariant(int index)
        {
            if(index < 0 || index >= questionsQueue.Count)
                throw new ArgumentOutOfRangeException("index");

            QuestionVariant questionVariant = questionsQueue[index];

            return questionVariant.Variants[GameMath.RandomCardFromTheDeck(questionVariant.Length)];
        }
    }
}
