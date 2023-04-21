using System.Collections.Generic;
using UnityEngine;

namespace Puzzle.DNA
{
    public class DNAOperator : MonoBehaviour
    {
        [SerializeField]
        private DNAInformator informator;

        private List<Nucleotide> chains;
    }
}
