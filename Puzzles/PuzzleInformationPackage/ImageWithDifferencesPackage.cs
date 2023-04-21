using System;
using UnityEngine;
using UnityEngine.Events;

namespace Puzzle
{

    [Serializable]
    public class ImageWithDifferencesPackage : InformationPackage
    {
        [SerializeField]
        private ImageWithDifferences imageWithDifferences;

        [SerializeField]
        [Range(3, 1024)]
        private int differenceCount = 3;
        [SerializeField]
        [Range(3, 1024)]
        private int trashCount = 3;
        [SerializeField]
        [Range(0, 1024)]
        private float allottedTime = 0;

        public int DifferenceCount { get => differenceCount; }
        public int TrashCount { get => trashCount; }
        public float AllottedTime { get => allottedTime; }
        public ImageWithDifferences ImageWithDifferences { get => imageWithDifferences; }

        public ImageWithDifferencesPackage(ImageWithDifferences imageWithDifferences, int differenceCount, float allottedTime,
            Sprite puzzleBackground, UnityAction successPuzzleDialog = null, UnityAction failedPuzzleDialog = null, int trashCount = 0)
            : base(puzzleBackground, successPuzzleDialog, failedPuzzleDialog)
        {
            if (differenceCount > 1024)//max difference on image
                differenceCount = 1024;

            if (differenceCount < 3)//min difference on image
                differenceCount = 3;

            if(trashCount < differenceCount)
                trashCount = differenceCount;

            this.differenceCount = differenceCount;
            this.allottedTime = Math.Max(allottedTime, 0);
            this.imageWithDifferences = imageWithDifferences;
            this.trashCount = trashCount;
        }
    }
}
