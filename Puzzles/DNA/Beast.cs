using System;
using System.Linq;
using UnityEngine;

namespace Puzzle.DNA
{
    public class Beast : MonoBehaviour
    {
        [SerializeField]
        private DNAInformator informator;

        private Tail tail { get; set; }
        private Ears ears { get; set; }
        private BodyShape bodyShape { get; set; }
        private Color color { get; set; }
        private EyeColor eyeColor { get; set; }

        public void GenerateNewBeast(Nucleotide[] nucleotides)
        {
            var tailSequence = new Nucleotide[4];
            Array.Copy(nucleotides, 0, tailSequence, 0, 4);
            if (tailSequence.SequenceEqual(new Nucleotide[] { Nucleotide.A, Nucleotide.C, Nucleotide.T, Nucleotide.G }))
                {
                tail = Tail.Short;
                }
                else if (tailSequence.SequenceEqual(new Nucleotide[] { Nucleotide.T, Nucleotide.G, Nucleotide.A, Nucleotide.C }))
                {
                tail = Tail.Medium;
                }
                else
                {
                tail = Tail.Long;
                }

            var earsSequence = new Nucleotide[4];
            Array.Copy(nucleotides, 4, earsSequence, 0, 4);
            if (earsSequence.SequenceEqual(new Nucleotide[] { Nucleotide.G, Nucleotide.T, Nucleotide.A, Nucleotide.C }))
                {
                    ears = Ears.Pointed;
                }
                else
                {
                    ears = Ears.Round;
                }
            }
        }
    }

