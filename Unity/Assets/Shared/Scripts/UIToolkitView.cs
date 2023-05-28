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
                return _pointsLabel;
            }
        }

        //  Fields ----------------------------------------
        [SerializeField] 
        private UIDocument _uiDocument;

        private Label _pointsLabel;

        
        //  Unity Methods ---------------------------------
        
        /// <summary>
        /// Unity calls this once automatically (when Scene starts running)
        /// </summary>
        protected void Start()
        {
            Debug.Log($"{GetType().Name}.Start()");

            VisualElement root = _uiDocument.rootVisualElement;
            _pointsLabel = root.Query<Label>("PointsLabel");
        }


        //  Methods ---------------------------------------

        //  Event Handlers --------------------------------
    }
}