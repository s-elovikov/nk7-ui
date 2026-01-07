using Cysharp.Threading.Tasks;
using Unity.VisualScripting;
using System.Threading;
using UnityEngine.UI;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Nk7.UI
{
    [RequireComponent(typeof(GraphicRaycaster))]
    public sealed partial class Popup : AnimatedComponent
    {
        [SerializeField] private Container _overlay;
        [SerializeField] private bool _destroyAfterHide;

        public async override UniTask HideAsync(CancellationToken cancellationToken = default)
        {
            await base.HideAsync(cancellationToken);

            if (!_destroyAfterHide)
            {
                return;
            }

            Destroy(gameObject);
        }

        public override void HideInstantly()
        {
            base.HideInstantly();

            if (!_destroyAfterHide)
            {
                return;
            }

            Destroy(gameObject);
        }
    }


#if UNITY_EDITOR
    public sealed partial class Popup : IInitializable
    {
        public void Initialize()
        {
            if (_overlay == null)
            {
                _overlay = transform.GetChild(0).GetComponent<Container>();
            }

            if (_animatedContainer == null)
            {
                _animatedContainer = transform.GetChild(1).GetComponent<Container>();
            }
        }

        private static void CreateOverlay(GameObject parentObject)
        {
            var overlayContainer = Container.CreateComponent(parentObject, "Overlay");
            var overlayImage = overlayContainer.AddComponent<Image>();

            overlayImage.color = new Color(0, 0, 0, 205f / 255f);
            overlayImage.raycastTarget = false;

            overlayContainer.Initialize();

            overlayContainer.RectTransform.ResetLocalPosition();
            overlayContainer.RectTransform.SetFullScreen(true);
        }

        private static void CreateContainer(GameObject parentObject)
        {
            var container = Container.CreateComponent(parentObject);

            container.AddComponent<Image>();
            container.Initialize();

            container.RectTransform.ResetLocalPosition();
            container.RectTransform.SetToCenter(true);

            container.RectTransform.sizeDelta = new Vector2(640, 640);
        }

        [MenuItem(Utils.POPUP_PATH, false, Utils.COMPONENT_PRIORITY)]
        private static void CreateComponent(MenuCommand menuCommand)
        {
            var popupObject = new GameObject(Utils.POPUP, typeof(RectTransform), typeof(Popup));
            var parentObject = Utils.GetCanvasAsParent(menuCommand.context as GameObject);

            GameObjectUtility.SetParentAndAlign(popupObject, parentObject);
            Undo.RegisterCreatedObjectUndo(popupObject, "Create " + popupObject.name);

            var popupRectTransform = popupObject.GetComponent<RectTransform>();
            var popup = popupObject.GetComponent<Popup>();

            popupRectTransform.SetFullScreen(true);

            CreateOverlay(popupObject);
            CreateContainer(popupObject);

            popup.Initialize();
        }
    }
#endif
}