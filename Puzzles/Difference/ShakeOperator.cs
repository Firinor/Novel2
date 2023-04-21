using System;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Puzzle.FindDifferences
{
    public class ShakeOperator : MonoBehaviour
    {
        private const int ERROR_FORCE = 100;
        private Vector3 impulse;
        [SerializeField]
        private float offset;
        [SerializeField]
        private float ingredientFrictionBraking;
        [SerializeField]
        private float ingredientBorder;

        void FixedUpdate()
        {
            if (impulse != Vector3.zero)
                ForseToIngredient();
            if (CheckOutOfBounce(transform.localPosition))
                BackBehind();
        }

        private void ForseToIngredient()
        {
            Vector3 pos = transform.localPosition;
            CheckScreenBorders(pos);
            pos += impulse;
            impulse = CheckImpulse(impulse);

            transform.localPosition = pos;
        }
        private bool CheckOutOfBounce(Vector3 pos)
        {
            return pos.x < -offset || pos.x > offset || pos.y < -offset || pos.y > offset;
        }
        private void BackBehind()
        {
            if (transform.localPosition.x < -offset)
            {
                transform.localPosition = new Vector3(-offset, transform.localPosition.y, transform.localPosition.z);
            }
            if (transform.localPosition.x > offset)
            {
                transform.localPosition = new Vector3(offset, transform.localPosition.y, transform.localPosition.z);
            }
            if (transform.localPosition.y < -offset)
            {
                transform.localPosition = new Vector3(transform.localPosition.x, -offset, transform.localPosition.z);
            }
            if (transform.localPosition.y > offset)
            {
                transform.localPosition = new Vector3(transform.localPosition.x, offset, transform.localPosition.z);
            }
        }

        private void CheckScreenBorders(Vector3 pos)
        {
            float x = pos.x + impulse.x;
            if (x < -offset || x > offset)
            {
                impulse.x = -impulse.x;
            }
            float y = pos.y + impulse.y;
            if (y < -offset || y > offset)
            {
                impulse.y = -impulse.y;
            }
        }
        private Vector3 CheckImpulse(Vector3 impulse)
        {
            impulse *= ingredientFrictionBraking;
            if (Math.Abs(impulse.x) < ingredientBorder && Math.Abs(impulse.y) < ingredientBorder)
            {
                return Vector3.zero;
            }

            return impulse;
        }

        internal void SetErrorImpulse()
        {
            float randomDirection = Random.value * 360 * Mathf.Deg2Rad;

            impulse = new Vector3(math.cos(randomDirection), math.sin(randomDirection), 0) * ERROR_FORCE;
        }
        internal void SetImpulse(Vector3 impulse)
        {
            this.impulse = impulse;
        }
    }
}
