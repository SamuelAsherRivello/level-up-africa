using System.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace RMC.LevelUpAfrica.Examples.Shared
{
    //  Event Class ---------------------------------------
    public class RocketUnityEvent : UnityEvent<Rocket> {}
            
    /// <summary>
    /// Replace with comments...
    /// </summary>
    public class Rocket : MonoBehaviour
    {
        //  Events ----------------------------------------
        public RocketUnityEvent OnDestroyed = new RocketUnityEvent();

        //  Properties ------------------------------------
        public float RocketMovementSpeed 
        {
            get
            {
                return _rocketMovementSpeed;
            }
            set
            {
                _rocketMovementSpeed = value;
            }
        }
        
        public float RocketMovementAcceleration 
        {
            get
            {
                return _rocketMovementAcceleration;
            }
            set
            {
                _rocketMovementAcceleration = value;
            }
        }
        
   
        
        //  Fields ----------------------------------------

        private float _rocketMovementSpeed;
        private float _rocketMovementAcceleration;

        //  Unity Methods ---------------------------------
        protected void Start()
        {
            StartCoroutine(DestroyMeAfter5Seconds());
        }

        protected void Update()
        {
            UpdateMovement();
        }

        //  Coroutines ------------------------------------
        private IEnumerator DestroyMeAfter5Seconds()
        {
            // Wait 5 seconds
            yield return new WaitForSeconds(5);
            
            // Broadcast event
            OnDestroyed.Invoke(this);
            
            // Now its probably FAR offscreen. Remove it
            Destroy(gameObject);
        }
        
        //  Methods ---------------------------------------
        private void UpdateMovement()
        {
            // Add Acceleration
            _rocketMovementSpeed = _rocketMovementSpeed * _rocketMovementAcceleration;
            
            // Use Movement
            transform.Translate(
                new Vector3(0, _rocketMovementSpeed * Time.deltaTime, 0), 
                Space.World);
        }


        //  Event Handlers --------------------------------
    }
}