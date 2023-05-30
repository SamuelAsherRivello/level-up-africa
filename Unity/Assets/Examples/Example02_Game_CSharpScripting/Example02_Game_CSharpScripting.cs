using DG.Tweening;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

namespace RMC.LevelUpAfrica
{
    /// <summary>
    /// Replace with comments...
    /// </summary>
    public class Example02_Game_CSharpScripting : MonoBehaviour
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
        private int _points = 100;
        
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
                // Position new rocket
                Quaternion newRotation = Quaternion.Euler(-90, 0, 0);
                Vector3 newPosition = _ship.RocketSpawnpoint.transform.position;
                
                // Create new rocket
                Rocket rocket = Instantiate(_rocketPrefab, newPosition, newRotation);
                
                // Move new rocket
                rocket.RocketMovementSpeed = _rocketMovementSpeed;
                rocket.RocketMovementAcceleration = _rocketMovementAcceleration;
                
                // Programmatic Animation - Scale Down now 
                rocket.gameObject.transform.localScale = 
                    new Vector3(0.1f, 0.1f, 0.1f);
                
                // Programmatic Animation - Scale Up soon
                rocket.gameObject.transform.DOScale(
                    new Vector3(1, 1, 1), 0.5f).SetEase(Ease.OutExpo);
                
                // Observe events
                rocket.OnDestroyed.AddListener(Rocket_OnDestroyed);
       
            }
        }

        //  Event Handlers --------------------------------
        private void Rocket_OnDestroyed(Rocket rocket)
        {
            // Reward Points
            _points += 1;
            _uiToolkitView.PointsLabel.text = $"Points: {_points:000}";
        }
        
        private void RestartButton_OnClicked()
        {
            // Reload current scene
            SceneManager.LoadScene(0);
        }
    }
}