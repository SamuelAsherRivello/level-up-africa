using System.Threading;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UIElements;

// ReSharper disable ArrangeAccessorOwnerBody
namespace RMC.LevelUpAfrica.Examples.More.UIToolkit.Approaches.Example_03_UIToolkit_Wrap
{
    //  Namespace Properties ------------------------------

    //  Class Attributes ----------------------------------
    public class StatsBarWrapperUnityEvent : UnityEvent<StatsBarWrapper>{}

    /// <summary>
    /// Demonstrates a **RAW** approach to using UIToolkit.
    /// </summary>
    public class StatsBarWrapper 
    {
        //  Events ----------------------------------------
        public StatsBarWrapperUnityEvent OnClicked = new StatsBarWrapperUnityEvent();

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
        private VisualElement _visualElement;

        private Label _titleLabel;
        private Label _detailsLabel;
        private VisualElement _icon;
        private VisualElement _barFill;
        private float _value;
        private CancellationTokenSource _cancellationToken;

        //  Initialization  ---------------------------------
        public StatsBarWrapper(VisualElement visualElement)
        {
            _visualElement = visualElement;
            
            // Query
            _titleLabel = _visualElement.Q<Label>("TitleLabel");
            _detailsLabel = _visualElement.Q<Label>("DetailsLabel");
            _icon = _visualElement.Q<VisualElement>("Icon");
            _barFill = _visualElement.Q<VisualElement>("BarFill");
            
            // Events
            _icon.RegisterCallback<ClickEvent>(Icon_OnClicked);
            
            // Defaults
            SetValueAsync(50);

            //Instructions
            Debug.Log("Click the StatsBar Icon to change health.");

        }

        //  Methods ---------------------------------------
        public async void SetValueAsync(float nextValue)
        {
            float fromValue = Value;
            float toValue = fromValue + 10;
            int durationMilliseconds = 500; 
            
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
            await LerpHelper.LerpValueAsync(
                fromValue, 
                toValue, 
                durationMilliseconds, 
                _cancellationToken,
                (nextValue2) =>
                {
                    Value = nextValue2;
                    Title = $"Health";
                    Details = $"{Value:0}%";   
                });
        }

        
        //  Event Handlers --------------------------------
        private async void Icon_OnClicked(ClickEvent evt)
        {
            OnClicked.Invoke(this);

        }
    }
}