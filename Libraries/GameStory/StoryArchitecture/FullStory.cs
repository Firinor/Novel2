using System;
using System.Collections.Generic;

namespace FirStory
{
    [Serializable]
    public class FullStory
    {
        public static string Narrator = "Narrator";
        public static string Silently = "InessaSilently";

        public List<Act> Acts;
        public Act this[int index]
        {
            get => Acts[index];
            set => Acts[index] = value;
        }

        public static implicit operator FullStory(List<List<List<StoryComponent>>> storyFromComponents)
        {
            List<Act> acts = new List<Act>();
            foreach (var act in storyFromComponents)
            {
                List<Episode> scenes = new List<Episode>();
                foreach (var scene in act)
                {
                    scenes.Add(scene);
                }
                acts.Add(scenes);
            }
            return new FullStory { Acts = acts };
        }
        public static implicit operator FullStory(List<List<Episode>> storyFromScenes)
        {
            List<Act> acts = new List<Act>();
            foreach (var act in storyFromScenes)
            {
                acts.Add(act);
            }
            return new FullStory() { Acts = acts };
        }
        public static implicit operator FullStory(List<Act> acts)
        {
            return new FullStory() { Acts = acts };
        }
    }
}
