using TerrainToolkit.Runtime.Mono;
using UnityEngine;

public class Test : MonoBehaviour
{
    [SerializeField] private AutoRun autoRun;
    void Start()
    {
        autoRun.Start();
    }
}