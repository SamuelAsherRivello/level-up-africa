using DG.Tweening;
using RMC.Core.Audio;
using RMC.LevelUpAfrica.Examples.Shared;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

namespace RMC.LevelUpAfrica.Examples.Game.Example02_Game_Complete
{
    /// <summary>
    /// Replace with comments...
    /// </summary>
    public class Example05_Game_Complete : MonoBehaviour
    {
        //  Events ----------------------------------------


        //  Properties ------------------------------------


        //  Fields ----------------------------------------
        
        [Header("Settings")] 
        [SerializeField] 
        [Range(0.05f, 0.2f)]
        private float _shipMovementSpeed = 0.2f; 

        [SerializeField] 
        [Range(0.5f, 2)]
        private float _shipMovementSpeedMax = 1; 
        
        [SerializeField] 
        [Range(0.005f, 0.2f)]
        private float _shipMovementFriction = .01f; 
        
        [SerializeField] 
        [Range(1f, 2)]
        private float _rocketMovementSpeed = 1.5f; 
        
        [SerializeField] 
        [Range(1.001f, 1.01f)]
        private float _rocketMovementAcceleration = 1.001f; 


        [Header("Input")]
        [SerializeField] 
        private InputActionReference _movementInputActionReference;

        [SerializeField] 
        private InputActionReference _shootInputActionReference;
        
        [Header("UI (Scene)")]
        [SerializeField] 
        private UIToolkitView _uiToolkitView;

        [Header("Graphics (Scene)")]
        [SerializeField] 
        private Ship _ship;

        [SerializeField] 
        private Background _background;

        [Header("Graphics (Project)")]
        [SerializeField] 
        private Rocket _rocketPrefab;
        
        private Vector2 _movement = new Vector2();
        private int _points = 0;
        private int _rocketSpawnpointIndex = 0;
        
        //  Unity Methods ---------------------------------
        
        /// <summary>
        /// Unity calls this once automatically (when Scene starts running)
        /// </summary>
        protected void Start()
        {
            Debug.Log($"{GetType().Name}.Start()");
            
            _uiToolkitView.RestartButton.clicked += RestartButton_OnClicked;
        }



        /// <summary>
        /// Unity calls this repeatedly automatically (while Scene is running)
        /// </summary>
        protected void Update()
        {
            UpdateMovement();
            UpdateShooting();
        }


        //  Methods ---------------------------------------
        private void UpdateMovement()
        {
            // Take Input
            Vector2 movementInput = _movementInputActionReference.action.ReadValue<Vector2>();

            // Add Input To Movement
            _movement = _movement + (movementInput * (Time.deltaTime * _shipMovementSpeed));
            
            // Add Friction To Movement
            _movement = _movement * (1 - _shipMovementFriction);
            
            // Limit Movement
            _movement = Vector2.ClampMagnitude(_movement, _shipMovementSpeedMax);
            
            // Use Movement
            _ship.transform.Translate(_movement);
            
        }

        private void UpdateShooting()
        {
            // Take Input
            bool isShootingInput = _shootInputActionReference.action.WasPerformedThisFrame();

            if (isShootingInput)
            {
                // Play Sound
                AudioManager.Instance.PlayAudioClip("Rocket"); 
                
                // Launch Rocket from left wing or right wing
                if (++_rocketSpawnpointIndex > _ship.RocketSpawnpoints.Count-1)
                {
                    _rocketSpawnpointIndex = 0;
                }
                    
                // Position new rocket
                Quaternion newRotation = Quaternion.Euler(-0, 0, 0);
                RocketSpawnpoint rocketSpawnpoint = _ship.RocketSpawnpoints[_rocketSpawnpointIndex];
                Vector3 newPosition = rocketSpawnpoint.transform.position;
                
                // Create new rocket
                Rocket rocket = Instantiate(_rocketPrefab, newPosition, newRotation);
                
                // Move new rocket
                rocket.RocketMovementSpeed = _rocketMovementSpeed + _movement.y;
                rocket.RocketMovementAcceleration = _rocketMovementAcceleration;
                
                // Programmatic Animation - Scale Down now 
                rocket.gameObject.transform.localScale = 
                    new Vector3(0.1f, 0.1f, 0.1f);
                
                // Programmatic Animation - Scale Up soon
                rocket.gameObject.transform.DOScale(
                    new Vector3(1, 1, 1), 1f).SetEase(Ease.OutExpo);
                
                // Programmatic Animation - Rotate soon
                rocket.gameObject.transform.DORotate(
                    new Vector3(0, 90, 0), 3f).SetEase(Ease.OutExpo);
                
                // Observe events
                rocket.OnDestroyed.AddListener(Rocket_OnDestroyed);
                
                // Reward Points
                _points += 1;
                _uiToolkitView.PointsLabel.text = $"Points: {_points:000}";
       
            }
        }

        //  Event Handlers --------------------------------
        private void Rocket_OnDestroyed(Rocket rocket)
        {
            // Do something?
        }
        
        private void RestartButton_OnClicked()
        {
            // Play Sound
            AudioManager.Instance.PlayAudioClip("Click"); 
            
            // Reload current scene
            SceneManager.LoadScene(0);
        }
    }
}