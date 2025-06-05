using UnityEngine;

namespace TerrainToolkit.Resources.Config
{
    [CreateAssetMenu(fileName = "TerrainConfig", menuName = "Config/TerrainConfig")]
    public class TerrainConfig : ScriptableObject
    {
        [Header("Water Settings")]
        public float seaLevel = 0.3f;
        
        [Header("Api Settings")]
        public string imageUrl = "http://localhost:8080/ostrovy.png";
    }
}