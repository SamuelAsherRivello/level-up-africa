using System.Threading;
using InstAnime;
using UnityEngine;
using UnityEngine.UIElements;

// ReSharper disable ArrangeAccessorOwnerBody
namespace RMC.LevelUpAfrica.Examples.More.UIToolkit.Approaches.Example_01_UIToolkit_Raw
{
    //  Namespace Properties ------------------------------

    //  Class Attributes ----------------------------------

    /// <summary>
    /// Demonstrates a **RAW** approach to using UIToolkit.
    /// </summary>
    public class Example_01_UIToolkit_Raw : MonoBehaviour
    {
        //  Events ----------------------------------------


        //  Properties ------------------------------------
        public float Value
        {
            get
            {
                return _value;
            }
            set
            {
                _value = value;
                _value = Mathf.Clamp(_value, 0, 100);
                _barFill.style.right = new StyleLength(new Length(100-_value, LengthUnit.Percent));
            }
        }
        
        public string Title
        {
            get
            {
                return _titleLabel.text;
            }
            set
            {
                _titleLabel.text = value;
            }
        }

        public string Details
        {
            get
            {
                return _detailsLabel.text;
            }
            set
            {
    
                _detailsLabel.text = value;
            }
        }
        

        //  Fields ----------------------------------------
        [SerializeField]
        private UIDocument _uiDocument;

        private Label _titleLabel;
        private Label _detailsLabel;
        private VisualElement _icon;
        private VisualElement _barFill;
        private float _value;
        private CancellationTokenSource _cancellationToken;

        //  Unity Methods ---------------------------------
        protected void Start()
        {
            // Query
            _titleLabel = _uiDocument.rootVisualElement.Q<Label>("TitleLabel");
            _detailsLabel = _uiDocument.rootVisualElement.Q<Label>("DetailsLabel");
            _icon = _uiDocument.rootVisualElement.Q<VisualElement>("Icon");
            _barFill = _uiDocument.rootVisualElement.Q<VisualElement>("BarFill");
            
            // Events
            _icon.RegisterCallback<ClickEvent>(Icon_OnClicked);
            
            // Defaults
            UpdateValue(50);

            //Instructions
            Debug.Log("Click the StatsBar Icon to change health.");

        }

        //  Methods ---------------------------------------
        private void UpdateValue(float nextValue)
        {
            Value = nextValue;
            Title = $"Health";
            Details = $"{Value:0}%";    
        }

        
        //  Event Handlers --------------------------------
        private async void Icon_OnClicked(ClickEvent evt)
        {
            float fromValue = Value;
            float toValue = fromValue + 10;
            float durationSeconds = 0.5f; 
            
            if (toValue >= 100)
            {
                toValue = 0;
            }

            //LERP: If we are already changing the value, cancel
            //the old change and use only the new change.
            if (_cancellationToken != null)
            {
                _cancellationToken.Cancel();
            }
            _cancellationToken = new CancellationTokenSource();
            
            //Update the value over time for demonstration purposes
            InstantAnimator.AnimateAsync<float>(
                fromValue, 
                toValue, 
                durationSeconds, 
                (nextValue) =>
                {
                    UpdateValue(nextValue);
                },
                _cancellationToken.Token, 
                UpdateMode.Unscaled);
        }
    }
}