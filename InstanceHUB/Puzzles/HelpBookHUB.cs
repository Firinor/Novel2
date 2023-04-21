using FirInstanceCell;
using UnityEngine;

namespace HelpBook
{
    public static class HelpBookHUB
    {
        public static GameObject HelpBook => HelpBookCell.GetValue();
        public static MonoBehaviour HelpBookManager => HelpBookManagerCell.GetValue();

        public static InstanceCell<GameObject> HelpBookCell = new InstanceCell<GameObject>();
        public static InstanceCell<MonoBehaviour> HelpBookManagerCell = new InstanceCell<MonoBehaviour>();
    }
}
