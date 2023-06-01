using UnityEngine;
using UnityEngine.UIElements;

namespace RMC.LevelUpAfrica.Examples
{
    public class Example_UIToolkit_HelloWorld : MonoBehaviour
    {
        [SerializeField] 
        private UIDocument _uiDocument;
        private int _count = 0;
        
        protected void Start()
        {
            Debug.Log($"{GetType().Name}.Start()");
            
            // UI Toolkit - Observe Event
            Button clickMeButton = _uiDocument.rootVisualElement.Q<Button>("clickMeButton");
            clickMeButton.clicked += ClickMeButton_OnClicked;
        }
        
        private void ClickMeButton_OnClicked()
        {
            // UI Toolkit - Update Layout
            Label statusText = _uiDocument.rootVisualElement.Q<Label>("statusText");
            
            // Update count
            _count = _count + 1;
            string message = $"You clicked the '{statusText.name}' button {_count} times.";
            statusText.text = $"{message}";
        }
    }
}


