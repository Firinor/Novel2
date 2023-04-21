using FirUnityEditor;
using UnityEngine;

namespace HelpBook
{
    public class HelpBookHUBInformator : MonoBehaviour
    {
        [SerializeField, NullCheck]
        private GameObject helpBook;
        [SerializeField, NullCheck]
        private MonoBehaviour helpBookManager;


        void Awake()
        {
            HelpBookHUB.HelpBookCell.SetValue(helpBook);
            HelpBookHUB.HelpBookManagerCell.SetValue(helpBookManager);

            Destroy(this);
        }
    }
}
