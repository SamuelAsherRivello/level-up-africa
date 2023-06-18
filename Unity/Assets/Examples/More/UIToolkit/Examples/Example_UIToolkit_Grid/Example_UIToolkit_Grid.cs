using System.Collections.Generic;
using RMC.LevelUpAfrica.Examples.Shared;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UIElements;

namespace RMC.LevelUpAfrica.Examples.More.UIToolkit.Example_UIToolkit_Grid
{
    /// <summary>
    /// Replace with comments...
    /// </summary>
    public class Example_UIToolkit_Grid : MonoBehaviour
    {
        //  Events ----------------------------------------


        //  Properties ------------------------------------


        //  Fields ----------------------------------------
        
        [Header("UI (Scene)")]
        [SerializeField] 
        private UIDocument _uiDocument;

        [Header("UI (Project)")] 
        [SerializeField]
        private List<Sprite> _iconSprites;

        //  Unity Methods ---------------------------------
        
        /// <summary>
        /// Unity calls this once automatically (when Scene starts running)
        /// </summary>
        protected void Start()
        {
            Debug.Log($"{GetType().Name}.Start()");

            List<VisualElement> _tiles = 
                _uiDocument.rootVisualElement.Query<VisualElement>("Tile").ToList();

            for  (int i = 0; i < _tiles.Count; i++)
            {
                // Set Icon
                VisualElement tile = _tiles[i];
                int iconSpriteIndex = i % _iconSprites.Count;
                Sprite sprite = _iconSprites[iconSpriteIndex];
                tile.style.backgroundImage = new StyleBackground(sprite);
            }

        }

        //  Methods ---------------------------------------

        //  Event Handlers --------------------------------
    }
}