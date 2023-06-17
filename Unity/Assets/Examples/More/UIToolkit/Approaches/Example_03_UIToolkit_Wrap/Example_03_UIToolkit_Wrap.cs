using UnityEngine;
using UnityEngine.UIElements;

// ReSharper disable ArrangeAccessorOwnerBody
namespace RMC.LevelUpAfrica.Examples.More.UIToolkit.Approaches.Example_03_UIToolkit_Wrap
{
    //  Namespace Properties ------------------------------

    //  Class Attributes ----------------------------------

    /// <summary>
    /// Demonstrates a **RAW** approach to using UIToolkit.
    /// </summary>
    public class Example_03_UIToolkit_Wrap : MonoBehaviour
    {
        //  Events ----------------------------------------

        
        //  Properties ------------------------------------

        
        //  Fields ----------------------------------------
        [SerializeField]
        private UIDocument _uiDocument;
        private StatsBarWrapper _statsBarWrapper;

        
        //  Unity Methods ---------------------------------
        protected void Start()
        {
            // Query
            VisualElement statsBar = _uiDocument.rootVisualElement.Q<VisualElement>("StatsBar");

            _statsBarWrapper = new StatsBarWrapper(statsBar);
            
            // Events
            _statsBarWrapper.OnClicked.AddListener(StatsBarWrapper_OnClicked);
            
            // Defaults
            _statsBarWrapper.Value = 50; //Change value instantly

            //Instructions
            Debug.Log("Click the StatsBar Icon to change health.");

        }

        
        //  Methods ---------------------------------------

        
        //  Event Handlers --------------------------------
        private void StatsBarWrapper_OnClicked(StatsBarWrapper statsBarWrapper)
        {
            float nextValue = _statsBarWrapper.Value + 10;
            if (nextValue >= 100)
            {
                nextValue = 0;
            }
            
            _statsBarWrapper.SetValueAsync(nextValue); //Change value gradually
        }
    }
}