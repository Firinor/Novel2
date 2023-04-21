using UnityEngine;

[RequireComponent(typeof(Animation))]
public class AnimationOperator : MonoBehaviour
{
    private Animation startAnimation;

    //void Awake()
    //{
    //    if(startAnimation == null)
    //    {
    //        startAnimation = GetComponent<Animation>();
    //    }
    //}

    public void PlayStart()
    {
        if (startAnimation == null)
        {
            startAnimation = GetComponent<Animation>();
        }
        startAnimation.Play();
    }
}
