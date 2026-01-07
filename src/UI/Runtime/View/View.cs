using UnityEngine.UI;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Nk7.UI
{
    [RequireComponent(typeof(Container), typeof(GraphicRaycaster))]
    public sealed partial class View : AnimatedComponent
    {

    }


#if UNITY_EDITOR
    public sealed partial class View : IInitializable
    {
        public void Initialize()
        {
            if (_animatedContainer == null)
            {
                _animatedContainer = GetComponent<Container>();
            }
        }

        [MenuItem(Utils.VIEW_PATH, false, Utils.COMPONENT_PRIORITY)]
        private static void CreateComponent(MenuCommand menuCommand)
        {
            var viewObject = new GameObject(Utils.VIEW, typeof(RectTransform), typeof(View));
            var parentObject = Utils.GetCanvasAsParent(menuCommand.context as GameObject);

            GameObjectUtility.SetParentAndAlign(viewObject, parentObject);
            Undo.RegisterCreatedObjectUndo(viewObject, "Create " + viewObject.name);

            var viewRectTransform = viewObject.GetComponent<RectTransform>();
            var container = viewObject.GetComponent<Container>();
            var view = viewObject.GetComponent<View>();

            viewRectTransform.SetFullScreen(true);

            container.Initialize();
            view.Initialize();
        }
    }
#endif
}