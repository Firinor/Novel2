using System.Collections.Generic;
using UnityEngine;

namespace Puzzle.PortalBuild
{
    public class AtomsInformator : MonoBehaviour
    {
        public List<AtomComponent> Atoms;

        public int AtomCount { get { return Atoms.Count; } }
    }
}
