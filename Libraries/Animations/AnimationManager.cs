using System.Collections.Generic;
using UnityEngine;

public class AnimationManager : MonoBehaviour
{
    [SerializeField]
    private List<AnimationOperator> animationOperator;

    public void PlayStart()
    {
        if(animationOperator != null)
        {
            foreach (AnimationOperator animation in animationOperator)
            {
                animation.PlayStart();
            }
        }
    }
}
