using System;
using UnityEngine;

namespace Puzzle.DNA
{
    #region enums

    public enum BeastParts { Tail, Ears, BodyShape, Color, EyeColor}
    public enum Tail { Short, Medium, Long }
    public enum Ears { Pointed, Round }
    public enum BodyShape { Slim, Stocky }
    public enum Color { Black, White, Grey, Orange }
    public enum EyeColor { Green, Blue, Brown }
    #endregion
    public enum Nucleotide
    {
        A,//Аденин - Adenine
        C,//Цитозин - Cytosine
        G,//Гуанин - Guanine
        T// Тимин - Timin
    }

    public class DNAInformator : MonoBehaviour
    {
        [SerializeField]
        private Sprite[] Tails;
        [SerializeField]
        private Sprite[] Ears;
        [SerializeField]
        private Sprite[] BodyShape;
        [SerializeField]
        private Sprite[] Colors;
        [SerializeField]
        private Sprite[] EyeColors;

        public Nucleotide Pair(Nucleotide nucleotide)
        {
            switch (nucleotide)
            {
                case Nucleotide.A:
                    return Nucleotide.T;
                case Nucleotide.T:
                    return Nucleotide.A;
                case Nucleotide.C:
                    return Nucleotide.G;
                case Nucleotide.G:
                    return Nucleotide.C;
                default:
                    throw new Exception("There is no pair for the specified nucleotide!");
            }
        }
    }
}
