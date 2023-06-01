using System.Collections.Generic;
using UnityEngine;

namespace RMC.LevelUpAfrica.Examples
{
    /// <summary>
    /// Replace with comments...
    /// </summary>
    public class Ship : MonoBehaviour
    {
        //  Events ----------------------------------------

        //  Properties ------------------------------------

        public List<RocketSpawnpoint> RocketSpawnpoints
        {
            get
            {
                return _rocketSpawnpoints;
            }
        }

        //  Fields ----------------------------------------
        [SerializeField] 
        private List<RocketSpawnpoint> _rocketSpawnpoints;

        
        //  Unity Methods ---------------------------------
        protected void Start()
        {
            Debug.Log($"{GetType().Name}.Start()");
        }


        //  Methods ---------------------------------------

        //  Event Handlers --------------------------------
    }
}