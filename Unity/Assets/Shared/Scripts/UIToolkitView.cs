using UnityEngine;
using UnityEngine.UIElements;

namespace RMC.LevelUpAfrica
{
    //  Namespace Properties ------------------------------


    //  Class Attributes ----------------------------------


    /// <summary>
    /// Replace with comments...
    /// </summary>
    public class UIToolkitView : MonoBehaviour
    {
        //  Events ----------------------------------------


        //  Properties ------------------------------------
        public Label PointsLabel 
        {
            get
            {
                return _uiDocument.rootVisualElement.Query<Label>("PointsLabel");
            }
        }
        
        public Button RestartButton 
        {
            get
            {
                return _uiDocument.rootVisualElement.Query<Button>("RestartButton");
            }
        }

        //  Fields ----------------------------------------
        [SerializeField] 
        private UIDocument _uiDocument;

        
        //  Unity Methods ---------------------------------
        
        /// <summary>
        /// Unity calls this once automatically (when Scene starts running)
        /// </summary>
        protected void Start()
        {
            Debug.Log($"{GetType().Name}.Start()");
            
            RestartButton.RegisterCallback<NavigationSubmitEvent>(
                RestartButton_NavigationSubmitEvent, TrickleDown.TrickleDown);
        }

        //  Methods ---------------------------------------

        //  Event Handlers --------------------------------
        
        /// <summary>
        /// Unity automatically links clicking the Spacebar
        /// to clicking the runtime buttons. We don't want that.
        /// Disable that here.
        /// </summary>
        private void RestartButton_NavigationSubmitEvent(NavigationSubmitEvent evt)
        {
           evt.StopPropagation();
        }
    }
}