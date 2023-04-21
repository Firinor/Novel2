using System;
using UnityEngine;
using static Puzzle.StarMap.StarMapInformator.ConstellationsVariant;
using Random = UnityEngine.Random;

namespace Puzzle.StarMap
{
    public enum Constellation
    {
        Andromeda = 1,//	1	Андромеда
        Antlia, //	2	Насос
        Apus, //	3	Райская Птица
        Aquarius, //	4	Водолей
        Aquila, //	5	Орёл
        Ara, //	6	Жертвенник
        Aries, //	7	Овен
        Auriga, //	8	Возничий
        Bootes, //	9	Волопас
        Caelum, //	10	Резец
        Camelopardalis, //	11	Жираф
        Cancer, //	12	Рак
        CanesVenatici, //	13	Гончие Псы
        CanisMajor, //	14	Большой Пёс
        CanisMinor, //	15	Малый Пёс
        Capricornus, //	16	Козерог
        Carina, //	17	Киль
        Cassiopeia, //	18	Кассиопея
        Centaurus,//	19	Кентавр
        Cepheus,//	20	Цефей
        Cetus, //	21	Кит
        Chamaeleon,//	22	Хамелеон
        Circinus,//	23	Циркуль
        Columba, //	24	Голубь
        ComaBerenices, //	25	Волосы Вероники
        CoronaAustralis, //	26	Южная Корона
        CoronaBorealis, //	27	Северная Корона
        Corvus, //	28	Ворон
        Crater, //	29	Чаша
        Crux,//	30	Южный Крест
        Cygnus, //	31	Лебедь
        Delphinus, //	32	Дельфин
        Dorado, //	33	Золотая Рыба
        Draco, //	34	Дракон
        Equuleus, //	35	Малый Конь
        Eridanus, //	36	Эридан
        Fornax, //	37	Печь
        Gemini, //	38	Близнецы
        Grus, //	39	Журавль
        Hercules, //	40	Геркулес
        Horologium,//	41	Часы
        Hydra, //	42	Гидра
        Hydrus, //	43	Южная Гидра
        Indus, //	44	Индус
        Lacerta, //	45	Ящерица
        Leo, //	46	Лев
        LeoMinor, //	47	Малый Лев
        Lepus, //	48	Заяц
        Libra, //	49	Весы
        Lupus, //	50	Волк
        Lynx, //	51	Рысь
        Lyra, //	52	Лира
        Mensa, //	53	Столовая Гора
        Microscopium, //	54	Микроскоп
        Monoceros, //	55	Единорог
        Musca, //	56	Муха
        Norma, //	57	Наугольник
        Octans, //	58	Октант
        Ophiuchus, //	59	Змееносец
        Orion, //	60	Орион
        Pavo, //	61	Павлин
        Pegasus, //	62	Пегас
        Perseus, //	63	Персей
        Phoenix,//	64	Феникс
        Pictor, //	65	Живописец
        Pisces, //	66	Рыбы
        PiscisAustrinus, //	67	Южная Рыба
        Puppis, //	68	Корма
        Pyxis, //	69	Компас
        Reticulum, //	70	Сетка
        Sagitta, //	71	Стрела
        Sagittarius,//	72	Стрелец
        Scorpius, //	73	Скорпион
        Sculptor, //	74	Скульптор
        Scutum, //	75	Щит
        Serpens, //	76	Змея
        Sextans, //	77	Секстант
        Taurus,//	78	Телец
        Telescopium,//	79	Телескоп
        Triangulum,//	80	Треугольник
        TriangulumAustrale, //	81	Южный Треугольник
        Tucana,//	82	Тукан
        UrsaMajor, //	83	Большая Медведица
        UrsaMinor, //	84	Малая Медведица
        Vela, //	85	Паруса
        Virgo, //	86	Дева
        Volans, //	87	Летучая Рыба
        Vulpecula, //	88	Лисичка
    }

    public class StarMapInformator : MonoBehaviour
    {
        //[SerializeField]
        public Sprite[] Constellations;
        //[SerializeField]
        public ConstellationsVariant[] Hemispheres;

        public ConstellationsVariant this[Hemisphere hemisphere]
        {
            get
            {
                foreach (ConstellationsVariant chosenHemisphere in Hemispheres)
                {
                    if (chosenHemisphere.hemisphere == hemisphere)
                    {
                        return chosenHemisphere;
                    }
                }
                return null;
            }
        }
        public Sprite this[Constellation constellation]
        {
            get
            {
                foreach (Sprite сonstellationSprite in Constellations)
                {
                    if (сonstellationSprite.name.Replace(" ", "") == constellation.ToString())
                    {
                        return сonstellationSprite;
                    }
                }
                return null;
            }
        }

        public Hemisphere ChoseHemisphere()
        {
            int i = Random.Range(0, Hemispheres.Length);
            return Hemispheres[i].hemisphere;
        }

        public AnswerSprite ChoseAnswerSprite()
        {
            return ChoseAnswerSprite(ChoseHemisphere());
        }
        public AnswerSprite ChoseAnswerSprite(Hemisphere hemisphere)
        {
            AnswerSprite[] answers = this[hemisphere].Answers;
            int i = Random.Range(0, answers.Length);
            return answers[i];
        }

        [Serializable]
        public class ConstellationsVariant
        {
            public Hemisphere hemisphere;
            public Sprite[] HemispherePuzzleSprite;
            public AnswerSprite[] Answers;

            [Serializable]
            public class AnswerSprite
            {
                public Sprite sprite;
                public Constellation constellation;
            }
        }
    }
}
