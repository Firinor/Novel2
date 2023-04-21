using System;
using UnityEngine;

namespace Puzzle.PortalBuild
{
    [Serializable]
    public class AtomComponent
    {
        [SerializeField]
        private Atom designation;
        [SerializeField]
        private Color color;
        [SerializeField]
        private Sprite sprite;

        public AtomComponent(string designation, Color color, Sprite sprite):this(color, sprite)
        {
            if (!Enum.TryParse(designation, true, out this.designation))
                Debug.Log(designation + " is not an underlying value of the Atom enumeration.");
        }
        public AtomComponent(Atom atom, Color color, Sprite sprite) : this(color, sprite)
        {
            designation = atom;
        }
        private AtomComponent(Color color, Sprite sprite)
        {
            this.color = color;
            this.sprite = sprite;
        }

        public string Designation { get => designation.ToString(); }
        public int Number { get => (int)designation - 1; }
        public Color Color { get => color; }
        public Sprite Sprite { get => sprite; }

    }
}
