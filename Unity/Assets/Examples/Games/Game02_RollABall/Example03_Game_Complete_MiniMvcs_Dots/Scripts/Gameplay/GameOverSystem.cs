using Unity.Entities;
using UnityEngine;

namespace TMG.RollABallDOTS
{
    public partial class GameOverSystem : SystemBase
    {
        protected override void OnCreate()
        {
            RequireForUpdate<GameOverTag>();
        }

        protected override void OnStartRunning()
        {
            Debug.Log("Game Over!");
        }

        protected override void OnUpdate()
        {
            
        }
    }
}