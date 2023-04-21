using FirUnityEditor;
using Puzzle.StarMap;
using UnityEngine;

public class StarMapAutoTest : MonoBehaviour
{
    [SerializeField, NullCheck]
    private StarMapInformator starMapInformator;

    [ContextMenu("Start auto test")]
    public void StartAutoTest()
    {
        string report = "";

        var hemi = starMapInformator.Hemispheres;
        for (int i = 0; i < hemi.Length; i++)
        {
            string constellationReport = "";
            var cons = hemi[i].Answers;
            for (int j = 0; j < cons.Length; j++)
            {
                Sprite sprite = starMapInformator[cons[j].constellation];
                if (sprite == null)
                    constellationReport += "\t" + cons[j].constellation.ToString() + "\n";
            }

            if (!string.IsNullOrEmpty(constellationReport))
                report += hemi[i].hemisphere + ": \n" + constellationReport;
        }

        if (string.IsNullOrEmpty(report))
        {
            Debug.Log("Test OK!");
        }
        else
            Debug.LogWarning(report);
    }
}
