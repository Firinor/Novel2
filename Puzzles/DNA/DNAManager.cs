using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Puzzle.DNA
{
    public class DNAManager : PuzzleOperator
    {
        private BeastParts[] beastParts;

        private Tail[] tails;
        private Ears[] ears;
        private BodyShape[] bodyShapes;
        private Color[] colors;
        private EyeColor[] eyeColors;

        private void GenerateNewDNACode(int numberOfPairs)
        {
            beastParts = DNACodeGenerator.NewCode<BeastParts>(5);

            tails = DNACodeGenerator.NewCode<Tail>(4);
            ears = DNACodeGenerator.NewCode<Ears>(4);
            bodyShapes = DNACodeGenerator.NewCode<BodyShape>(4);
            colors = DNACodeGenerator.NewCode<Color>(4);
            eyeColors = DNACodeGenerator.NewCode<EyeColor>(4);
        }
     }
}
