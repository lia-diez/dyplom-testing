using UnityEngine;

namespace TerrainToolkit.Runtime.Helpers
{
    public static class CameraHelper
    {
        public static void PointMainCameraAt(Vector3 target, Vector3 position)
        {
            var cam = Camera.main;
            if (cam != null)
            {
                cam.transform.position = position;
                cam.transform.LookAt(target);
            }
        }
    }
}