using Unity.Entities;
using Unity.Scenes;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

namespace TMG.RollABallDOTS
{
    public class UIController : MonoBehaviour
    {
        [SerializeField] private SubScene _rollABallScene;

        private VisualElement _documentRoot;
        private Button _restartButton;
        private Button _confirmButton;
        private Button _cancelButton;
        private Label _statusLabel;
        private Label _scoreLabel;
        private VisualElement _dialogView;
        private World _ecsWorld;
        private bool _isGameOver;
        
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

        private void OpenRestartDialog()
        {
            _dialogView.visible = true;
            
            TogglePause(true);
        }

        private void OnButtonConfirm()
        {
            SceneSystem.UnloadScene(_ecsWorld.Unmanaged, _rollABallScene.SceneGUID);
            SceneManager.LoadScene("RollABallDOTS");
            
            TogglePause(false);
        }

        private void OnButtonCancel()
        {
            _dialogView.visible = false;

            if (_isGameOver) return;

            TogglePause(false);
        }
        
        private void OnGameStart()
        {
            _statusLabel.text = "Use Arrow Keys";
        }

        private void OnGameOver()
        {
            _statusLabel.text = "You Win!";
            _isGameOver = true;
        }

        private void SetPickupCount(PickupCounter pickupCounter)
        {
            _scoreLabel.text = $"Score: {pickupCounter.CurrentCount}/{pickupCounter.TargetCount}";
        }

        private void TogglePause(bool shouldPauseGame)
        {
            var simulationSystemGroup = _ecsWorld.GetExistingSystemManaged(typeof(SimulationSystemGroup));
            simulationSystemGroup.Enabled = !shouldPauseGame;
        }
    }
}