using System.Threading;
using InstAnime;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UIElements;
using InstantAnimator = RMC.Core.Animator.InstantAnimator;

namespace RMC.LevelUpAfrica.Examples.More.UIToolkit.Approaches.Example_05_UIToolkit_HybridComponent
{
    
    
    /// <summary>
    /// 
    /// </summary>
    public sealed class StatsBar : VisualElement
    {
        //  Internal Classes ----------------------------------
        public class StatsBarUnityEvent : UnityEvent<StatsBar>{}
        
        
        /// <summary>
        /// 
        /// </summary>
        public new class UxmlTraits : VisualElement.UxmlTraits {}
    
        /// <summary>
        /// 
        /// </summary>
        public new class UxmlFactory : UxmlFactory<StatsBar, UxmlTraits>
        {
            public override VisualElement Create(IUxmlAttributes bag, CreationContext cc)
            {
                VisualTreeAsset visualTreeAsset = Resources.Load<VisualTreeAsset>("StatsBar/Uxml/StatsBarLayout");
                VisualElement rootVisualElement = base.Create(bag, cc);
                visualTreeAsset.CloneTree(rootVisualElement);

                StatsBar statsBar = rootVisualElement.Q<StatsBar>();
                statsBar.Initialize(statsBar);
                
                return rootVisualElement;
            }
        }



        //  Events ----------------------------------------
        public readonly StatsBarUnityEvent OnClicked = new StatsBarUnityEvent();

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
        private VisualElement _visualElement;
        private Label _titleLabel;
        private Label _detailsLabel;
        private VisualElement _icon;
        private VisualElement _barFill;
        private float _value;
        private CancellationTokenSource _cancellationToken;

        //  Initialization --------------------------------
        public void Initialize (VisualElement visualElement)
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
            Value = 0;
            Title = $"Health";
            Details = $"{Value:0}%";   

        }

        //  Methods ---------------------------------------
        public void SetValueAsync(float newValue)
        {
            float fromValue = Value;
            float durationSeconds = 0.5f; 

            //LERP: If we are already changing the value, cancel
            //the old change and use only the new change.
            if (_cancellationToken != null)
            {
                _cancellationToken.Cancel();
            }
            _cancellationToken = new CancellationTokenSource();
            
            //Update the value over time for demonstration purposes
            InstantAnimator.AnimateAsync(
                fromValue,
                newValue,
                durationSeconds,
                (nextValue) =>
                {
                    Value = nextValue;
                    Title = $"Health";
                    Details = $"{Value:0}%";   
                },
                _cancellationToken.Token, 
                UpdateMode.Unscaled);
        }
        
        
        //  Event Handlers --------------------------------
        private void Icon_OnClicked(ClickEvent evt)
        {
            OnClicked.Invoke(this);
        }
    }
}