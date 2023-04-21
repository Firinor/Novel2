using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Random = UnityEngine.Random;
using Unity.Mathematics;

namespace Puzzle.FindObject
{
    public class AlchemicalIngredientOperator : MonoBehaviour, IOptionsSwitchHandler,
    IBeginDragHandler, IDragHandler, IEndDragHandler
    {
        [SerializeField]
        private float mass;
        [SerializeField]
        private Image image;
        [SerializeField]
        private int keyIngredientNumber;

        private Camera MainCamera => SceneHUB.Camera;
        private FindObjectManager puzzleManager => (FindObjectManager)PuzzleHUB.FindObjectManager;

        private bool drag = false;
        private float timer;
        private const float FIFTH_SEC = 0.2f;
        private const int ERROR_FORCE = 10;

        private Vector3 impulse;
        private Vector3 lastPosition;
        private Vector3 startMousePosition;
        private bool ingredientDrag = false;
        private static int screenHeight;
        private static int screenWidth;

        void Awake()
        {
            //this.FixedUpdateAsObservable()
            //        .Where(_ => !ingredientDrag && impulse != Vector3.zero)
            //        .Subscribe(_ => ForseToIngredient())
            //        .AddTo(disposables);
            //this.FixedUpdateAsObservable()
            //        .Where(_ => !ingredientDrag && CheckOutOfBounce(transform.localPosition))
            //        .Subscribe(_ => BackToScreen())
            //        .AddTo(disposables);
            //this.UpdateAsObservable()
            //        .Where(_ => ingredientDrag && EveryFifthSec())
            //        .Subscribe(_ => CheckLastPosition())
            //        .AddTo(disposables);

            ResetOptions();
        }

        public void ResetOptions()
        {
            screenHeight = (int)(CanvasManager.ScreenHeight / 2);
            screenWidth = (int)(CanvasManager.ScreenWidth / 2);
        }

        void FixedUpdate()
        {
            if (!ingredientDrag)
            {
                if (impulse != Vector3.zero)
                    ForseToIngredient();
                if (CheckOutOfBounce(transform.localPosition))
                    BackToScreen();
            }
        }
        void Update()
        {
            if (ingredientDrag && EveryFifthSec())
                CheckLastPosition();
        }
        private bool EveryFifthSec()
        {
            timer += Time.deltaTime;
            if (timer > FIFTH_SEC)
            {
                timer -= FIFTH_SEC;
                return true;
            }
            return false;
        }
        private void CheckLastPosition()
        {
            lastPosition = transform.localPosition;
        }
        public void OnBeginDrag(PointerEventData eventData)
        {
            image.raycastTarget = false;
            ingredientDrag = true;
            CheckLastPosition();
            startMousePosition = Input.mousePosition/CanvasManager.ScaleFactor - transform.localPosition;
            if (eventData.button == PointerEventData.InputButton.Left)
            {
                drag = true;
            }
            else
            {
                eventData.pointerDrag = null;
            }
        }
        public void OnDrag(PointerEventData eventData)
        {
            if (drag && eventData.button == PointerEventData.InputButton.Left)
            {
                transform.localPosition 
                    = Input.mousePosition / CanvasManager.ScaleFactor - startMousePosition;
            }
        }
        public void OnEndDrag(PointerEventData eventData)
        {
            ingredientDrag = false;
            image.raycastTarget = true;
            if (puzzleManager != null && puzzleManager.PointerOnRecipe)
            {
                if (keyIngredientNumber > 0)
                {
                    puzzleManager.ActivateIngredient(keyIngredientNumber);
                    puzzleManager.RemoveIngredient(this);
                    Destroy(gameObject);
                }
                else
                {
                    puzzleManager.Particles(Input.mousePosition / CanvasManager.ScaleFactor, success: false);
                    SetRandomImpulse(puzzleManager.ForseToIngredient * ERROR_FORCE, randomForse: false);
                }
                return;
            }
            impulse = (transform.localPosition - lastPosition) / mass;
            //Debug.Log(impulse + " " + impulse.x + " " + impulse.y);
        }
        private void ForseToIngredient()
        {
            Vector3 pos = transform.localPosition;
            CheckScreenBorders(pos);
            pos += impulse;
            impulse = puzzleManager.CheckImpulse(ref pos, ref impulse);

            transform.localPosition = pos;
        }
        private bool CheckOutOfBounce(Vector3 pos)
        {
            return pos.x < -screenWidth || pos.x > screenWidth || pos.y < -screenHeight || pos.y > screenHeight;
        }
        private void BackToScreen()
        {
            if (transform.localPosition.x < -screenWidth)
            {
                transform.localPosition = new Vector3(-screenWidth, transform.localPosition.y, transform.localPosition.z);
            }
            if (transform.localPosition.x > screenWidth)
            {
                transform.localPosition = new Vector3(screenWidth, transform.localPosition.y, transform.localPosition.z);
            }
            if (transform.localPosition.y < -screenHeight)
            {
                transform.localPosition = new Vector3(transform.localPosition.x, -screenHeight, transform.localPosition.z);
            }
            if (transform.localPosition.y > screenHeight)
            {
                transform.localPosition = new Vector3(transform.localPosition.x, screenHeight, transform.localPosition.z);
            }
        }
        private void CheckScreenBorders(Vector3 pos)
        {
            float x = pos.x + impulse.x;
            if (x < -screenWidth || x > screenWidth)
            {
                impulse.x = -impulse.x;
            }
            float y = pos.y + impulse.y;
            if (y < -screenHeight || y > screenHeight)
            {
                impulse.y = -impulse.y;
            }
        }
        internal void SetSprite(Sprite sprite)
        {
            image.sprite = sprite;
            image.SetNativeSize();
        }
        internal void SetRandomImpulse(float forse, bool randomForse = true)
        {
            forse *= randomForse ? Random.value : 1;
            float randomDirection = Random.value * 360 * Mathf.Deg2Rad;

            impulse = new Vector3(math.cos(randomDirection), math.sin(randomDirection), 0) * forse;
        }
        internal void SetImpulse(Vector3 impulse)
        {
            this.impulse = impulse;
        }
        internal void SetImpulse(float force, bool toZeroPoint = true)
        {
            if (force == 0)
            {
                SetImpulse(Vector3.zero);
                return;
            }

            if (!toZeroPoint)
                force *= -1;

            Vector3 resultVector = (Vector3.zero - transform.localPosition).normalized * force;
            SetImpulse(resultVector);
        }
        internal void SetRecipeSprite(Sprite sprite)
        {
            SetSprite(sprite);
            image.color = Color.black;
            image.raycastTarget = false;
            enabled = false;
        }
        internal void AddToRecipe(int number)
        {
            keyIngredientNumber = number;
        }
        internal void Success()
        {
            puzzleManager.Particles(
                MainCamera.WorldToScreenPoint(transform.position) / CanvasManager.ScaleFactor,
                success: true);
            image.color = Color.white;
        }
        internal bool OnBox(float border)
        {
            Vector3 pos = transform.localPosition;
            return Mathf.Abs(pos.x) < border && Mathf.Abs(pos.y) < border;
        }
    }
}