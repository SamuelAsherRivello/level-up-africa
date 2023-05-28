using UnityEngine;

namespace RMC.LevelUpAfrica
{
    //  Namespace Properties ------------------------------


    //  Class Attributes ----------------------------------


    /// <summary>
    /// Replace with comments...
    /// </summary>
    public class Background : MonoBehaviour
    {
        //  Events ----------------------------------------


        //  Properties ------------------------------------


        //  Fields ----------------------------------------

        
        //  Unity Methods ---------------------------------
        protected void Start()
        {
            Debug.Log($"{GetType().Name}.Start()");
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