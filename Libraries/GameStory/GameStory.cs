using FirUtilities;
using System;
using System.Collections.Generic;

namespace FirStory
{
    public static class GameStory
    {
        public static string[] AlignTheStoryLengthwise(string fullText, int textCapacity)
        {
            if (fullText == null || string.IsNullOrEmpty(fullText))
                throw new Exception("Story data could not be found!");

            if (textCapacity <= 0)
                throw new Exception("Text capacity must not be less than or equal to zero!");

            if (fullText.Length < textCapacity)
                return new string[1] { fullText };

            string[] straws = ArrayUtilities.StringArraySlicing(fullText, textCapacity);

            if (straws == null || straws.Length == 0)
                throw new Exception("To align the story, it should not be empty!");

            int minTextCapacity = textCapacity / 3;
            //int preferredTextCapacity = textCapacity * 2 / 3;

            int phase = 1;
            bool phaseСompleted;

            while (true)
            {
                straws = ArrayUtilities.СleanArray(straws);

                List<bool> needsProcessingStraw = new List<bool>();

                for (int i = 0; i < straws.Length; i++)
                {
                    needsProcessingStraw.Add(straws[i].Length < minTextCapacity);
                }

                switch (phase)
                {
                    case 1:
                        phaseСompleted = Phase1(textCapacity, straws, needsProcessingStraw);
                        if (phaseСompleted)
                        {
                            phase++;
                        }
                        continue;
                    case 2:
                        phaseСompleted = Phase2(textCapacity, straws, needsProcessingStraw);
                        if (phaseСompleted)
                        {
                            phase++;
                        }
                        continue;
                    case 3:
                        phaseСompleted = Phase3(textCapacity, straws, needsProcessingStraw);
                        if (phaseСompleted)
                        {
                            phase++;
                        }
                        continue;
                    default:
                        break;
                }

                break;
            }

            return straws;
        }
        private static bool Phase1(int textCapacity, string[] straws, List<bool> needsProcessingStraw)
        {
            bool newPhase = true;

            for (int i = straws.Length - 2; i > 0; i--)
            {
                if (needsProcessingStraw[i - 1] == true
                    && needsProcessingStraw[i] == true
                    && needsProcessingStraw[i + 1] == true)
                {
                    FindingPlaceForStraw(textCapacity, straws, ref newPhase, ref i);
                }
            }

            return newPhase;
        }
        private static bool Phase2(int textCapacity, string[] straws, List<bool> needsProcessingStraw)
        {
            bool newPhase = true;

            for (int i = straws.Length - 2; i > 0; i--)
            {
                if (needsProcessingStraw[i] == true &&
                    (needsProcessingStraw[i - 1] == true
                    || needsProcessingStraw[i + 1] == true))
                {
                    FindingPlaceForStraw(textCapacity, straws, ref newPhase, ref i);
                }
            }

            return newPhase;
        }
        private static bool Phase3(int textCapacity, string[] straws, List<bool> needsProcessingStraw)
        {
            bool newPhase = true;

            for (int i = straws.Length - 2; i > 0; i--)
            {
                if (needsProcessingStraw[i] == true)
                {
                    FindingPlaceForStraw(textCapacity, straws, ref newPhase, ref i);
                }
            }

            return newPhase;
        }
        private static void FindingPlaceForStraw(int textCapacity, string[] straws, ref bool newPhase, ref int i)
        {
            if (straws[i].Length + straws[i + 1].Length < textCapacity)
            {
                straws[i + 1] = straws[i] + straws[i + 1];
                straws[i] = "";
                i--;
                newPhase = false;
            }
            else if (straws[i].Length + straws[i - 1].Length < textCapacity)
            {
                straws[i - 1] += straws[i];
                straws[i] = "";
                i--;
                newPhase = false;
            }
        }
    }
}
