using UnityEngine;

namespace TerrainToolkit.Runtime.MapExport
{
    public static class HeightMapGenerator
    {
        public static float[,] GeneratePerlinHeightMap(int width, int height, float scale, float offsetX = 0f, float offsetY = 0f)
        {
            float[,] heightMap = new float[width, height];

            if (scale <= 0f) scale = 0.0001f;

            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    float sampleX = (x + offsetX) / scale;
                    float sampleY = (y + offsetY) / scale;

                    float noise = Mathf.PerlinNoise(sampleX, sampleY);
                    heightMap[x, y] = noise;
                }
            }

            return heightMap;
        }
    }
}