using System;
using System.Collections.Generic;

namespace FirStory
{
    [Serializable]
    public class Act
    {
        public List<Episode> Scenes;
        public Episode this[int index]
        {
            get => Scenes[index];
            set => Scenes[index] = value;
        }
        public int Count { get => Scenes.Count; }

        public static implicit operator Act(List<List<StoryComponent>> scenesFromComponents)
        {
            List<Episode> scenes = new List<Episode>();
            foreach (var scene in scenesFromComponents)
            {
                scenes.Add(scene);
            }
            return new Act() { Scenes = scenes };
        }
        public static implicit operator Act(List<Episode> scenes)
        {
            return new Act() { Scenes = scenes };
        }
    }
}
