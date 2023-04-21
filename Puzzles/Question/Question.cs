using UnityEngine;

namespace Puzzle.TetraQuestion
{
    [CreateAssetMenu(menuName = "Puzzles/Question", fileName = "new question")]
    public class Question : ScriptableObject
    {
        [SerializeField]
        private Sprite sprite;
        public Sprite Sprite { get => sprite; }
        [SerializeField]
        [Multiline(10)]
        private string questionText;
        public string QuestionText { get => questionText; }
        [SerializeField]
        private string correctAnswer;
        public string CorrectAnswer { get => correctAnswer; }
        [SerializeField]
        private string[] wrongAnswers = new string[3];
        public string[] WrongAnswers { get => wrongAnswers; }

        public Question(string questionText, string correctAnswer, string[] wrongAnswers)
        {
            this.questionText = questionText;
            this.correctAnswer = correctAnswer;
            this.wrongAnswers = wrongAnswers;
        }
    }
}
