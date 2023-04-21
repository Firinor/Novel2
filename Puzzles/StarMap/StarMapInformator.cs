using System;
using UnityEngine;
using static Puzzle.StarMap.StarMapInformator.ConstellationsVariant;
using Random = UnityEngine.Random;

namespace Puzzle.StarMap
{
    public enum Constellation
    {
        Andromeda = 1,//	1	���������
        Antlia, //	2	�����
        Apus, //	3	������� �����
        Aquarius, //	4	�������
        Aquila, //	5	���
        Ara, //	6	����������
        Aries, //	7	����
        Auriga, //	8	��������
        Bootes, //	9	�������
        Caelum, //	10	�����
        Camelopardalis, //	11	�����
        Cancer, //	12	���
        CanesVenatici, //	13	������ ���
        CanisMajor, //	14	������� ϸ�
        CanisMinor, //	15	����� ϸ�
        Capricornus, //	16	�������
        Carina, //	17	����
        Cassiopeia, //	18	���������
        Centaurus,//	19	�������
        Cepheus,//	20	�����
        Cetus, //	21	���
        Chamaeleon,//	22	��������
        Circinus,//	23	�������
        Columba, //	24	������
        ComaBerenices, //	25	������ ��������
        CoronaAustralis, //	26	����� ������
        CoronaBorealis, //	27	�������� ������
        Corvus, //	28	�����
        Crater, //	29	����
        Crux,//	30	����� �����
        Cygnus, //	31	������
        Delphinus, //	32	�������
        Dorado, //	33	������� ����
        Draco, //	34	������
        Equuleus, //	35	����� ����
        Eridanus, //	36	������
        Fornax, //	37	����
        Gemini, //	38	��������
        Grus, //	39	�������
        Hercules, //	40	��������
        Horologium,//	41	����
        Hydra, //	42	�����
        Hydrus, //	43	����� �����
        Indus, //	44	�����
        Lacerta, //	45	�������
        Leo, //	46	���
        LeoMinor, //	47	����� ���
        Lepus, //	48	����
        Libra, //	49	����
        Lupus, //	50	����
        Lynx, //	51	����
        Lyra, //	52	����
        Mensa, //	53	�������� ����
        Microscopium, //	54	���������
        Monoceros, //	55	��������
        Musca, //	56	����
        Norma, //	57	����������
        Octans, //	58	������
        Ophiuchus, //	59	���������
        Orion, //	60	�����
        Pavo, //	61	������
        Pegasus, //	62	�����
        Perseus, //	63	������
        Phoenix,//	64	������
        Pictor, //	65	���������
        Pisces, //	66	����
        PiscisAustrinus, //	67	����� ����
        Puppis, //	68	�����
        Pyxis, //	69	������
        Reticulum, //	70	�����
        Sagitta, //	71	������
        Sagittarius,//	72	�������
        Scorpius, //	73	��������
        Sculptor, //	74	���������
        Scutum, //	75	���
        Serpens, //	76	����
        Sextans, //	77	��������
        Taurus,//	78	�����
        Telescopium,//	79	��������
        Triangulum,//	80	�����������
        TriangulumAustrale, //	81	����� �����������
        Tucana,//	82	�����
        UrsaMajor, //	83	������� ���������
        UrsaMinor, //	84	����� ���������
        Vela, //	85	������
        Virgo, //	86	����
        Volans, //	87	������� ����
        Vulpecula, //	88	�������
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
                foreach (Sprite �onstellationSprite in Constellations)
                {
                    if (�onstellationSprite.name.Replace(" ", "") == constellation.ToString())
                    {
                        return �onstellationSprite;
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
