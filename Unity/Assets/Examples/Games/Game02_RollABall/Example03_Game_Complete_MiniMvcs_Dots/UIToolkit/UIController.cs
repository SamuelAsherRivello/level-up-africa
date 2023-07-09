using Unity.Entities;
using Unity.Scenes;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

namespace TMG.RollABallDOTS
{
    /// <summary>
    /// This is the UIController that receives events from the ECS side and updates the UI accordingly.
    /// </summary>
    public class UIController : MonoBehaviour
    {
        // Reference to the SubScene for restarting
        [SerializeField] private SubScene _rollABallScene;

        // Our UI toolkit elements
        private VisualElement _documentRoot;
        private Button _restartButton;
        private Button _confirmButton;
        private Button _cancelButton;
        private Label _statusLabel;
        private Label _scoreLabel;
        private VisualElement _dialogView;
        
        // The default ECS world
        private World _ecsWorld;
        
        private bool _isGameOver;
        
        // Initialization
        private void OnEnable()
        {
            _isGameOver = false;
            
            _documentRoot = GetComponent<UIDocument>().rootVisualElement;

            _dialogView = _documentRoot.Q<VisualElement>("DialogView");
            
            _scoreLabel = _documentRoot.Q<Label>("ScoreLabel");
            _statusLabel = _documentRoot.Q<Label>("StatusLabel");
            _cancelButton = _documentRoot.Q<Button>("CancelButton");
            _confirmButton = _documentRoot.Q<Button>("ConfirmButton");
            _restartButton = _documentRoot.Q<Button>("RestartButton");

            _restartButton.clicked += OpenRestartDialog;
            _cancelButton.clicked += OnButtonCancel;
            _confirmButton.clicked += OnButtonConfirm;
            
            _ecsWorld = World.DefaultGameObjectInjectionWorld;
            
            var pickupCountUISystem = _ecsWorld.GetExistingSystemManaged<PickupCountUISystem>();
            pickupCountUISystem.OnIncrementPickupCount += SetPickupCount;

            var gameOverSystem = _ecsWorld.GetExistingSystemManaged<GameOverSystem>();
            gameOverSystem.OnGameOver += OnGameOver;
            
            OnGameStart();
        }

        // Called when the restart button is pressed
        private void OpenRestartDialog()
        {
            _dialogView.visible = true;
            
            TogglePause(true);
        }

        // Restart the game
        private void OnButtonConfirm()
        {
            SceneSystem.UnloadScene(_ecsWorld.Unmanaged, _rollABallScene.SceneGUID);
            SceneManager.LoadScene("RollABallDOTS");
            
            TogglePause(false);
        }

        // Return to the game
        private void OnButtonCancel()
        {
            _dialogView.visible = false;

            if (_isGameOver) return;

            TogglePause(false);
        }
        
        // Update the status text at the start of the game
        private void OnGameStart()
        {
            _statusLabel.text = "Use Arrow Keys";
        }

        // Update the status text at the end of the game
        private void OnGameOver()
        {
            _statusLabel.text = "You Win!";
            _isGameOver = true;
        }

        // Update the pickup counter UI
        private void SetPickupCount(PickupCounter pickupCounter)
        {
            _scoreLabel.text = $"Score: {pickupCounter.CurrentCount}/{pickupCounter.TargetCount}";
        }

        // Toggle between paused and unpaused.
        private void TogglePause(bool shouldPauseGame)
        {
            var simulationSystemGroup = _ecsWorld.GetExistingSystemManaged(typeof(SimulationSystemGroup));
            simulationSystemGroup.Enabled = !shouldPauseGame;
        }
    }
}