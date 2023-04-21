using UnityEngine;

namespace FirMath
{
    public static class GameTransform
    {
        public static Vector3 GetGlobalPoint(Transform transform)
        {
            //return Input.mousePosition / CanvasManager.ScaleFactor - transform.localPosition;
            return Camera.main.WorldToScreenPoint(transform.position);
        }
    }
}
