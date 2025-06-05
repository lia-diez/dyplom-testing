using System;
using System.Linq;
using System.Threading.Tasks;
using TerrainToolkit.Resources.Config;
using TerrainToolkit.Runtime.Api;
using TerrainToolkit.Runtime.Helpers;
using TerrainToolkit.Runtime.MapExport;
using TerrainToolkit.Runtime.TerrainEngine;
using Unity.VisualScripting;
using UnityEngine;
#if UNITY_EDITOR
using sc.terrain.proceduralpainter;
using UnityEditor;
#endif

namespace TerrainToolkit.Runtime.Mono
{
    public class AutoRun : MonoBehaviour
    {
        [SerializeField] private TerrainConfig config;
        [SerializeField] private TextureConfig textureConfig;
        [SerializeField] private GameObject terrainPrefab;

        public async void Start()
        {
            byte[] imageData = await ApiFetcher.FetchPngAsync(config.imageUrl);
            float[,] heightMap = PngHeightMapLoader.LoadRedChannelFromPng(imageData);
            
            int maxX = heightMap.GetLength(0);
            int maxZ = heightMap.GetLength(1);
            int maxY = (int)(GetMaxHeight(heightMap) * 255f);
            
            CameraHelper.PointMainCameraAt(new Vector3(maxX / 2f, 0, maxZ / 2f),
                new Vector3(maxX / 2f, 500, -1 * maxZ / 5f));
            
            Vector3 size = new Vector3(maxX, maxY, maxZ);
            Vector3 position = Vector3.zero;
            
            GameObject terrainGO = TerrainGenerator.GenerateTerrain(heightMap, size, position);
            Terrain terrain = terrainGO.GetComponent<Terrain>();
            
            Texture2D[] textures = textureConfig.TerrainTextures;
            
            TextureHandler.AssignTerrainTextures(terrain, textures);
            TextureHandler.ApplyTextureMap(terrain, maxY, textureConfig.Thresholds);
            
            WaterGenerator.AddFlatWaterPlane(terrain, maxY * config.seaLevel, textureConfig.WaterMaterial);
        }
        
        private float GetMaxHeight(float[,] heightMap)
        {
            float maxHeight = 0;
            for (int x = 0; x < heightMap.GetLength(0); x++)
            {
                for (int z = 0; z < heightMap.GetLength(1); z++)
                {
                    if (heightMap[x, z] > maxHeight)
                    {
                        maxHeight = heightMap[x, z];
                    }
                }
            }

            return maxHeight;
        }
        
        public async Task EditorGenerate(string URL, string apiKey, string prompt)
        {
#if UNITY_EDITOR
            byte[] result = await ApiFetcher.PostPromptAsync(URL, prompt, apiKey);
            float[,] heightMap = PngHeightMapLoader.LoadRedChannelFromPng(result);
    
            int maxX = heightMap.GetLength(0);
            int maxZ = heightMap.GetLength(1);
            int maxY = (int)(GetMaxHeight(heightMap) * 255f);

            Vector3 size = new Vector3(maxX, maxY, maxZ);
            Vector3 position = Vector3.zero;

            TerrainGenerator.ChangeTerrain(terrainPrefab, heightMap, size, position);
#endif
        }

    }
}