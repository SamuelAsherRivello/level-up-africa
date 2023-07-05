using UnityEngine;
using UnityEngine.UIElements;

// ReSharper disable ArrangeAccessorOwnerBody
namespace RMC.LevelUpAfrica.Examples.More.UIToolkit.Approaches.Example_05_UIToolkit_HybridComponent
{
    //  Namespace Properties ------------------------------

    //  Class Attributes ----------------------------------

    /// <summary>
    /// Demonstrates a **RAW** approach to using UIToolkit.
    /// </summary>
    public class Example_05_UIToolkit_HybridComponent : MonoBehaviour
    {
        //  Events ----------------------------------------

        
        //  Properties ------------------------------------

        
        //  Fields ----------------------------------------
        [SerializeField]
        private UIDocument _uiDocument;
        private StatsBar _statsBar;

        
        //  Unity Methods ---------------------------------
        protected void Start()
        {
            // Query
            _statsBar = _uiDocument.rootVisualElement.Q<StatsBar>("StatsBar");

            // Events
            _statsBar.OnClicked.AddListener(StatsBar_OnClicked);
            
            // Defaults
            _statsBar.Value = 50; //Change value instantly

            //Instructions
            Debug.Log("Click the StatsBar Icon to change health.");

        }

        
        //  Methods ---------------------------------------

        
        //  Event Handlers --------------------------------
        private void StatsBar_OnClicked(StatsBar statsBar)
        {
            float nextValue = _statsBar.Value + 10;
            if (nextValue >= 100)
            {
                nextValue = 0;
            }
            
            _statsBar.SetValueAsync(nextValue); //Change value gradually
        }
    }
}