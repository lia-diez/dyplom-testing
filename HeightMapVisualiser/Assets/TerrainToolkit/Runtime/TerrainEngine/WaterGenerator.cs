using UnityEngine;

namespace TerrainToolkit.Runtime.TerrainEngine
{
    public static class WaterGenerator
    {
        public static GameObject AddFlatWaterPlane(UnityEngine.Terrain terrain, float waterHeight, Material waterMaterial = null)
        {
            TerrainData terrainData = terrain.terrainData;
            Vector3 terrainPos = terrain.transform.position;

            float width = terrainData.size.x;
            float length = terrainData.size.z;

            GameObject water = GameObject.CreatePrimitive(PrimitiveType.Plane);
            water.name = "WaterPlane";

            // Adjust scale: Unity's default plane is 10x10 units, so scale to match terrain size
            water.transform.localScale = new Vector3(width / 10f, 1, length / 10f);
            water.transform.position = new Vector3(
                terrainPos.x + width / 2f,
                terrainPos.y + waterHeight,
                terrainPos.z + length / 2f
            );

            if (waterMaterial != null)
            {
                water.GetComponent<MeshRenderer>().material = waterMaterial;
            }

            return water;
        }
    }
}