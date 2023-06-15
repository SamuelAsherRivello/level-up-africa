using UnityEngine;
using UnityEngine.UIElements;

namespace RMC.LevelUpAfrica.Examples.More.UIToolkit.Approaches.Example_01_UIToolkit_Raw
{
    //  Namespace Properties ------------------------------

    //  Class Attributes ----------------------------------

    /// <summary>
    /// Replace with comments...
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
                return _title;
            }
            set
            {
                 _title = value;
                
                _titleLabel.text = _title;
            }
        }


        //  Fields ----------------------------------------
        [SerializeField]
        private UIDocument _uiDocument;

        private Label _titleLabel;
        private VisualElement _icon;
        private VisualElement _barFill;
        private float _value;
        private string _title;

        //  Unity Methods ---------------------------------
        protected void Start()
        {
            _titleLabel = _uiDocument.rootVisualElement.Q<Label>("TitleLabel");
            _icon = _uiDocument.rootVisualElement.Q<VisualElement>("Icon");
            _barFill = _uiDocument.rootVisualElement.Q<VisualElement>("BarFill");
            
            //
            Value = 0;

        }

        protected void Update()
        {
            Value += 0.25f;
            
            if (Value >= 100)
            {
                Value = 0;
            }
            Title = $"Health: {Value:000}";
        }
        
        //  Methods ---------------------------------------
        public string SamplePublicMethod(string message)
        {
            return message;
        }


        //  Event Handlers --------------------------------
        public void Target_OnCompleted(string message)
        {

        }
    }
}