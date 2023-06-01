using DG.Tweening;
using RMC.Core.Audio;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

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
        
        //  Unity Methods ---------------------------------
        
        /// <summary>
        /// Unity calls this once automatically (when Scene starts running)
        /// </summary>
        protected void Start()
        {
            Debug.Log($"{GetType().Name}.Start()");
            
            // UI Toolkit - Observe Event
            _uiToolkitView.RestartButton.clicked += RestartButton_OnClicked;
            
            // UI Toolkit - Update Layout
            _uiToolkitView.PointsLabel.text = $"Points: {5:000}";
        }

        //  Methods ---------------------------------------


        //  Event Handlers --------------------------------
        private void RestartButton_OnClicked()
        {
            // UI Toolkit - Handle Click
            SceneManager.LoadScene(0);
        }
    }
}