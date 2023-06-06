using UnityEngine;

namespace RMC.LevelUpAfrica.Examples.Shared
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
        public Vector2 ScrollSpeed
        {
            get
            {
                return _scrollSpeed;
            }
            set
            {
                //Only if the value has changed
                if (_scrollSpeed != value)
                {
                    // Store the material reference (just once)
                    if (_material == null)
                    {
                        _material = _renderer.material;
                    }
              
                    // Store the value
                    _scrollSpeed = value;
                    
                    // Update the ShaderGraph value
                    _material.SetVector("_ScrollSpeed", _scrollSpeed);
                }
        
            }
        }

        //  Fields ----------------------------------------
        [SerializeField] 
        private Renderer _renderer;
        
        private Material _material;
        private Vector2 _scrollSpeed;
        
        
        //  Unity Methods ---------------------------------
        protected void Start()
        {
            Debug.Log($"{GetType().Name}.Start()");

            ScrollSpeed = new Vector2(0, 0.1f);
        }
        
        //  Methods ---------------------------------------

        //  Event Handlers --------------------------------
    }
}