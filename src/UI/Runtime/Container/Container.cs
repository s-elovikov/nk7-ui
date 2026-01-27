using Nk7.UI.Animations;
using UnityEngine;
using System;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Nk7.UI
{
    [Serializable]
    public sealed partial class Container : BaseComponent
    {
        public Vector3 StartPosition { get; private set; } = AnimatorConstants.START_POSITION;
        public Vector3 StartRotation { get; private set; } = AnimatorConstants.START_ROTATION;
        public Vector3 StartScale { get; private set; } = AnimatorConstants.START_SCALE;
        public float StartAlpha { get; private set; } = AnimatorConstants.START_ALPHA;

        public void SetGameObjectState(bool state)
        {
            gameObject.SetActive(state);
        }

        public void SetCanvasState(bool state)
        {
            Canvas.enabled = state;
        }

        public void ResetPosition()
        {
            RectTransform.anchoredPosition3D = StartPosition;
        }

        public void ResetRotation()
        {
            RectTransform.localEulerAngles = StartRotation;
        }

        public void ResetScale()
        {
            RectTransform.localScale = StartScale;
        }

        public void ResetAlpha()
        {
            CanvasGroup.alpha = StartAlpha;
        }

        public void ResetValues()
        {
            SetGameObjectState(true);
            SetCanvasState(true);

            ResetPosition();
            ResetRotation();
            ResetScale();
            ResetAlpha();
        }
    }


    public sealed partial class Container
    {
#if UNITY_EDITOR
        public static Container CreateComponent(GameObject parentObject, string name = "Container")
        {
            var overlayObject = new GameObject(name, typeof(RectTransform), typeof(Container));

            if (parentObject != null)
            {
                GameObjectUtility.SetParentAndAlign(overlayObject, parentObject);
            }

            Undo.RegisterCreatedObjectUndo(overlayObject, "Create " + name);

            return overlayObject.GetComponent<Container>();
        }

        [MenuItem(Utils.CONTAINER_PATH, false, Utils.COMPONENT_PRIORITY)]
        private static void CreateComponent(MenuCommand menuCommand)
        {
            var container = CreateComponent(menuCommand.context as GameObject);

            container.Initialize();
        }
#endif
    }
}