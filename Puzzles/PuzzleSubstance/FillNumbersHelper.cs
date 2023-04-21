#if UNITY_EDITOR
using UnityEditor;
using UnityEditor.U2D.Sprites;
#endif
using UnityEngine;
using static Puzzle.ImageWithDifferences;

namespace Puzzle
{
#if UNITY_EDITOR
    public static class FillNumbersHelper
    {
        public static DifferencesStruct[] FillNumbers(Texture2D texture, DifferencesStruct[] differences)
        {
            if (texture == null)
                return null;

            SpriteDataProviderFactories factory = new SpriteDataProviderFactories();
            factory.Init();
            ISpriteEditorDataProvider dataProvider = factory.GetSpriteEditorDataProviderFromObject(texture);
            dataProvider.InitSpriteEditorDataProvider();

            SpriteRect[] spriteRects = dataProvider.GetSpriteRects();

            DifferencesStruct[] result = new DifferencesStruct[spriteRects.Length];

            for (int i = 0; i < spriteRects.Length; i++)
            {
                result[i].xShift = (int)spriteRects[i].rect.position.x;
                result[i].yShift = (int)spriteRects[i].rect.position.y;
                if(differences.Length > i)
                    result[i].sprite = differences[i].sprite;
            }
            return result;
        }

    }
#endif
}
