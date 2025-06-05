using UnityEngine;

namespace TerrainToolkit.Runtime.MapExport
{
    public static class PngHeightMapLoader
    {
        public static float[,] LoadRedChannelFromPng(byte[] fileData)
        {
            Texture2D tex = new Texture2D(2, 2);
            if (!tex.LoadImage(fileData))
            {
                Debug.LogError("Could not load image data.");
                return null;
            }

            int width = tex.width;
            int height = tex.height;
            Color32[] pixels = tex.GetPixels32();

            float[,] heights = new float[width, height];
            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    byte red = pixels[y * width + x].r;
                    heights[x, height - 1 - y] = red / 255f; // Y-flip
                }
            }

            return heights;
        }
    }
}