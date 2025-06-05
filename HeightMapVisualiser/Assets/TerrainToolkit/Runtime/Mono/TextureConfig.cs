using UnityEngine;

namespace TerrainToolkit.Runtime.Mono
{
    public class TextureConfig : MonoBehaviour
    {
        [SerializeField] private Texture2D grassTexture;
        [SerializeField] private Texture2D dirtTexture;
        [SerializeField] private Texture2D rockTexture;
        [SerializeField] private Material waterMaterial;
        
        [SerializeField] private float[] thresholds = { 0.2f, 0.5f, 0.8f };
    
        private Texture2D[] _terrainTextures;

        private void Awake()
        {
            _terrainTextures = new[] { grassTexture, dirtTexture, rockTexture };
        }

        public Texture2D[] TerrainTextures => _terrainTextures;
        public float[] Thresholds => thresholds;
        public Material WaterMaterial => waterMaterial;
    }
}
