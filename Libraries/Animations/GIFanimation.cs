using FirUnityEditor;
using FirConsts;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class GIFanimation : MonoBehaviour
{
    [SerializeField, NullCheck]
    private Image image;
    [SerializeField]
    private Sprite[] sprites;
    [SerializeField]
    private float framePerSecond = 6;
    [SerializeField]
    private float vanishingRate = 5f;

    private bool isAnimationEnabled = false;
    private bool isAnimationDisabled = false;
    private float timer;
    private float frameIndex = 0;

    private void Awake()
    {
        image.canvasRenderer.SetAlpha(alpha: 0f);
    }

    public void OnEnable()
    {
        timer = 0;
        isAnimationEnabled = true;

        image.canvasRenderer.SetAlpha(alpha: 0f);
        image.CrossFadeAlpha(alpha: 1f, vanishingRate, ignoreTimeScale: false);
    }

    private void Update()
    {
        timer += Time.deltaTime;
        if (timer > 1 / framePerSecond)
        {
            timer -= 1 / framePerSecond;
            frameIndex++;
            frameIndex = frameIndex % sprites.Length;
        }

        image.sprite = sprites[(int)frameIndex];

        if (!isAnimationEnabled && !isAnimationDisabled)
        {
            image.canvasRenderer.SetAlpha(alpha: 1f);
            image.CrossFadeAlpha(alpha: 0f, vanishingRate, ignoreTimeScale: false);
            DisableGIF();
        }
    }

    private async void DisableGIF()
    {
        int delay = (int)(vanishingRate * FirConst.MILISECONDS_COEFFICIENT);

        isAnimationDisabled = true;
        await Task.Delay(delay);
        isAnimationDisabled = false;

        enabled = false;
    }

    public void SetGIF(Sprite[] sprites)
    {
        this.sprites = sprites;
    }

    public void StartAnimation()
    {
        enabled = true;
    }

    public void StopAnimation()
    {
        isAnimationEnabled = false;
    }
}