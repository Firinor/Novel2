using System;
using UnityEngine;

public enum ViewDirection { Right = 1, Default = 0, Left = -1 }
public enum CharacterEmotion {
    Default, //Спокойствие
    Smile,//Улыбка
    Gloomy,//Глум
    Fear,//Страх
    Dreaming,//Мечтательность
    Helpless, //Безнадёга
    Doubt,//Сомнения
    Surprise,//Удивление
    Bewilderment,//Недоумение
    Uncertainty,//Неопределенность
    Disgust,//Отвращение
    Sullenly,//Угрюмость
    Angry,//Злость
    Irritation,//Раздажение
    Pride,//Гордость
    Tears,//Слёзы
    Sad, //Грусть
    Happiness // Счастье
}

[CreateAssetMenu(menuName = "Character/Character info", fileName = "CharInfo")]
public class CharacterInformator : ScriptableObject
{
    [Tooltip("Name of unit")]
    [SerializeField]
    private string[] unitName;
    public string Name { get { return unitName[(int)PlayerManager.Language]; } }

    [Tooltip("Sprite of unit")]
    [SerializeField]
    private Sprite sprite;
    public Sprite UnitSprite { get { return sprite; } }

    [Tooltip("Sprites of unit emotions")]
    [SerializeField]
    private EmotionSprite[] emotionSprites;
    public Sprite SpriteByEmotion(CharacterEmotion emotion)
    {
        if(emotion == CharacterEmotion.Default)
        {
            return sprite;
        }
        foreach(var pair in emotionSprites)
        {
            if(pair.emotion == emotion)
            {
                return pair.sprite;
            }
        }

        throw new Exception($"The character {unitName} does not have a sprite for the {emotion} emotion!");
    }

    [Tooltip("Image of the portrait of the unit's face")]
    [SerializeField]
    private Sprite face;
    public Sprite unitFace { get { return face; } }

    [Tooltip("The growth coefficient can be adjusted using the prefab SpeakerAncor")]
    [SerializeField]
    [Range(0f, 1f)]
    private float unitScale;
    public float UnitScale { get { return unitScale; } }

    [Tooltip("Default view direction")]
    [SerializeField]
    private ViewDirection viewDirection;
    public ViewDirection ViewDirection { get { return viewDirection; } }

    [Serializable]
    private class EmotionSprite
    {
        public CharacterEmotion emotion;
        public Sprite sprite;
    }
}
