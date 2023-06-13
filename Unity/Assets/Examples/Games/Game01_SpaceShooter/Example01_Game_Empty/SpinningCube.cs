using UnityEngine;

namespace RMC.LevelUpAfrica.Examples.Game.Example01_Empty
{
    public class SpinningCube: MonoBehaviour
    {
        [SerializeField] 
        private float _speed = 2;
        
        protected void Awake () 
        {
            // Called: First. Usage: Set up local scope    .  
        }
        
        protected void Start () 
        {
            // Called: Second. Usage: Set up dependencies  .
        }
        
        protected void Update () 
        {
            // Called: Every frame. 
            transform.Rotate(new Vector3(0, _speed, 0));
        }
    }
}