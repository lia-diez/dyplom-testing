using System.Linq;
using UnityEngine;

namespace TerrainToolkit.Runtime.TerrainEngine
{
    public static class TextureHandler
    {
        public static void AssignTerrainTextures(Terrain terrain, Texture2D[] textures)
        {
            var layers = new TerrainLayer[textures.Length];

            for (int i = 0; i < textures.Length; i++)
            {
                var layer = new TerrainLayer
                {
                    diffuseTexture = textures[i],
                    tileSize = new Vector2(10, 10) // Set how the texture tiles
                };
                layers[i] = layer;

                // Save the layer asset to avoid memory leaks in editor
#if UNITY_EDITOR
                UnityEditor.AssetDatabase.CreateAsset(layer, $"Assets/TerrainLayer_{i}.asset");
#endif
            }

            terrain.terrainData.terrainLayers = layers;
        }
    
        public static void ApplyTextureMap(Terrain terrain, float maxHeight, float[] thresholds)
        {
            var terrainData = terrain.terrainData;
            int width = terrainData.alphamapWidth;
            int height = terrainData.alphamapHeight;
            int numTextures = terrainData.terrainLayers.Length;

            float[,,] map = new float[width, height, numTextures];

            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    float normX = (float)x / width;
                    float normY = (float)y / height;

                    // Example: assign based on height
                    float heightSample = terrain.terrainData.GetHeight(
                        Mathf.RoundToInt(normY * terrain.terrainData.heightmapResolution),
                        Mathf.RoundToInt(normX * terrain.terrainData.heightmapResolution)
                    );
                
                    heightSample /= maxHeight; // Normalize height to [0, 1]

                    float[] weights = new float[numTextures];

                    for (int i = 0; i < numTextures; i++)
                        weights[i] = 0;
                    
                    // Assign weights based on height thresholds
                    for (int i = 0; i < thresholds.Length; i++)
                    {
                        if (heightSample < thresholds[i])
                        {
                            weights[i] = 1;
                            break;
                        }
                    }
                    // If no threshold matched, assign the last texture
                    if (weights.Sum() == 0)
                        weights[numTextures - 1] = 1;

                    // Normalize
                    float total = weights.Sum();
                    for (int i = 0; i < numTextures; i++)
                        map[x, y, i] = weights[i] / total;
                }
            }

            terrain.terrainData.SetAlphamaps(0, 0, map);
        }
    }
}
