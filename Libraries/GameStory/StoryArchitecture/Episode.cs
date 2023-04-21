using System;
using System.Collections.Generic;

namespace FirStory
{
    [Serializable]
    public class Episode
    {
        public List<StoryComponent> Replicas;
        public List<MultiText> Choices;
        public int Count { get => Replicas.Count; }
        public StoryComponent this[int index]
        {
            get => Replicas[index];
            set => Replicas[index] = value;
        }

        public static implicit operator Episode(List<StoryComponent> replicas)
        {
            return new Episode() { Replicas = replicas };
        }

        internal void AddReplica(StoryComponent newReplica)
        {
            if (Replicas == null)
            {
                Replicas = new List<StoryComponent>();
            }
            Replicas.Add(newReplica);
        }

        internal void AddChoice(MultiText texts)
        {
            if (Choices == null)
            {
                Choices = new List<MultiText>();
            }
            Choices.Add(texts);
        }
    }
}
