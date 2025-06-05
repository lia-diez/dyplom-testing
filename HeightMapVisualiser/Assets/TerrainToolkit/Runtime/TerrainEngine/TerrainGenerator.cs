using UnityEngine;

namespace TerrainToolkit.Runtime.TerrainEngine
{
    public static class TerrainGenerator
    {
        public static GameObject GenerateTerrain(float[,] heightMap, Vector3 terrainSize, Vector3 position)
        {
            int width = heightMap.GetLength(0);
            int height = heightMap.GetLength(1);

            TerrainData terrainData = new TerrainData
            {
                heightmapResolution = Mathf.Max(width, height),
                size = terrainSize
            };

            terrainData.SetHeights(0, 0, heightMap);

            GameObject terrainGO = UnityEngine.Terrain.CreateTerrainGameObject(terrainData);
            terrainGO.transform.position = position;

            return terrainGO;
        }

        public static void ChangeTerrain(GameObject terrainPrefab, float[,] heightMap, Vector3 terrainSize,
            Vector3 position)
        {
            int width = heightMap.GetLength(0);
            int height = heightMap.GetLength(1);

            var terrain = terrainPrefab.GetComponent<Terrain>();
            
            TerrainData terrainData = terrain.terrainData;

            terrainData.heightmapResolution = Mathf.Max(width, height);
            terrainData.size = terrainSize;

            terrainData.SetHeights(0, 0, heightMap);
            terrainPrefab.transform.position = position;
        }
        
        public static GameObject GenerateFlatTerrain(Vector3 size, Vector3 position)
        {
            TerrainData terrainData = new TerrainData
            {
                heightmapResolution = 1,
                size = size
            };

            float[,] heights = new float[1, 1];
            heights[0, 0] = 0f; // Flat terrain at height 0

            terrainData.SetHeights(0, 0, heights);

            GameObject terrainGO = UnityEngine.Terrain.CreateTerrainGameObject(terrainData);
            terrainGO.transform.position = position;

            return terrainGO;
        }
    }
}