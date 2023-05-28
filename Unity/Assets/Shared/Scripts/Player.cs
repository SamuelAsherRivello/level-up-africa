using UnityEngine;

namespace RMC.LevelUpAfrica
{
    /// <summary>
    /// Replace with comments...
    /// </summary>
    public class Player : MonoBehaviour
    {
        //  Events ----------------------------------------

        //  Properties ------------------------------------

        public RocketSpawnpoint RocketSpawnpoint 
        {
            get
            {
                return _rocketSpawnpoint;
            }
        }

        //  Fields ----------------------------------------
        [SerializeField] 
        private RocketSpawnpoint _rocketSpawnpoint;

        
        //  Unity Methods ---------------------------------
        protected void Start()
        {
            Debug.Log($"{GetType().Name}.Start()");
        }


        //  Methods ---------------------------------------

        //  Event Handlers --------------------------------
    }
}