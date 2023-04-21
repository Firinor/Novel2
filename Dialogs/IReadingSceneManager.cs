using Puzzle;
using UnityEngine;

public interface IReadingSceneManager
{
    public void CheckMap(RectTransform dialogButtonRectTransform);
    public void SwitchPanelsToOptions();
    public void SwithToPuzzle(InformationPackage puzzleInformationPackage, string additional = "");
}
