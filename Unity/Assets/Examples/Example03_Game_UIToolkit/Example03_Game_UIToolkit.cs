using UnityEngine;

namespace RMC.LevelUpAfrica.Examples
{
    /// <summary>
    /// Replace with comments...
    /// </summary>
    public class Example03_Game_UIToolkit : MonoBehaviour
    {
        //  Events ----------------------------------------


        //  Properties ------------------------------------


        //  Fields ----------------------------------------
        
        [Header("UI (Scene)")]
        [SerializeField] 
        private UIToolkitView _uiToolkitView;

        private int _points = 0;
        
        //  Unity Methods ---------------------------------
        
        /// <summary>
        /// Unity calls this once automatically (when Scene starts running)
        /// </summary>
        protected void Start()
        {
            Debug.Log($"{GetType().Name}.Start()");
            
            // UI Toolkit - Observe Event
            _uiToolkitView.RestartButton.clicked += RestartButton_OnClicked;
 
        }

        //  Methods ---------------------------------------


        //  Event Handlers --------------------------------
        private void RestartButton_OnClicked()
        {
            // UI Toolkit - Update Layout
            _points++;
            _uiToolkitView.PointsLabel.text = $"Points: {_points:000}";
        }
    }
}