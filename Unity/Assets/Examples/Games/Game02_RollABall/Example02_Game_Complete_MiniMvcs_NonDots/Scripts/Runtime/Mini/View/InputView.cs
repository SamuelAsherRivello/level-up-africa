using System;
using RMC.Core.Architectures.Mini.Context;
using RMC.Core.Architectures.Mini.Samples.RollABall.WithMini.Mini.Model;
using RMC.Core.Architectures.Mini.View;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

namespace RMC.Core.Architectures.Mini.Samples.RollABall.WithMini.Mini.View
{
    //  Namespace Properties ------------------------------
    public class OnInputUnityEvent : UnityEvent<Vector3> {}
    
    //  Class Attributes ----------------------------------

    /// <summary>
    /// The View handles user interface and user input
    /// </summary>
    public class InputView: MonoBehaviour, IView
    {
        //  Events ----------------------------------------
        [HideInInspector] 
        public readonly OnInputUnityEvent OnInput = new OnInputUnityEvent();

        //  Properties ------------------------------------
        public bool IsInitialized { get { return _isInitialized;} }
        public IContext Context { get { return _context;} }
        
        //  Fields ----------------------------------------
        private bool _isInitialized = false;
        private IContext _context;

        private RollABallInput _rollABallInput;
        private float _playerMovementSpeed = 0;

        //  Initialization  -------------------------------
        public void Initialize(IContext context)
        {
            if (!IsInitialized)
            {
                _isInitialized = true;
                _context = context;

                _rollABallInput = new RollABallInput();
                _rollABallInput.Enable();
                
                //Model
                RollABallModel rollABallModel = Context.ModelLocator.GetItem<RollABallModel>();
                _playerMovementSpeed = rollABallModel.PlayerMovementSpeed.Value;
            }
        }


        public void RequireIsInitialized()
        {
            if (!IsInitialized)
            {
                throw new Exception("MustBeInitialized");
            }
        }
        
        
        //  Unity Methods ---------------------------------
        private void OnEnable()
        {
            _rollABallInput?.Enable();
        }

        private void OnDisable()
        {
            _rollABallInput?.Disable();
        }

        protected void Update()
        {
            if (!IsInitialized)
            {
                return;
            }

            //Input
            // You can indeed connect to input-specific events from _rollABallInput
            // However, we want to 'always' be moving (with friction) and using Update() is best
            Vector2 movementAxis = _rollABallInput.Standard.PlayerMoveInput.ReadValue<Vector2>();
            float moveHorizontal = movementAxis.x * Time.deltaTime * _playerMovementSpeed;
            float moveVertical = movementAxis.y * Time.deltaTime * _playerMovementSpeed;
            Vector3 movement = new Vector3 (moveHorizontal, 0.0f, moveVertical);

            OnInput.Invoke(movement);

        }

        //  Methods ---------------------------------------
        
        
        //  Event Handlers --------------------------------

    }
}