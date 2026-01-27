using UnityEngine;

namespace Nk7.UI
{
    [RequireComponent(typeof(RectTransform), typeof(CanvasGroup), typeof(Canvas))]
    public abstract partial class BaseComponent : MonoBehaviour
    {
        [field: SerializeField] public RectTransform RectTransform { get; private set; }
        [field: SerializeField] public CanvasGroup CanvasGroup { get; private set; }
        [field: SerializeField] public Canvas Canvas { get; private set; }
    }


    public abstract partial class BaseComponent : IInitializable
    {
        public void Initialize()
        {
            if (RectTransform == null)
            {
                RectTransform = GetComponent<RectTransform>();
            }

            if (CanvasGroup == null)
            {
                CanvasGroup = GetComponent<CanvasGroup>();
            }

            if (Canvas == null)
            {
                Canvas = GetComponent<Canvas>();
            }
        }
    }
}