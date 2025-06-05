using sc.terrain.proceduralpainter;
using TerrainToolkit.Runtime.Mono;
using UnityEditor;
using UnityEngine;

namespace TerrainToolkit.Editor
{
    public class TerrainGeneratorWindow : EditorWindow
    {
        private AutoRun generator;
        private TerrainPainter terrainPainter;

        private string URL = "http://localhost:8080/api/generate"; // Default URL
        private string ApiKey = "API_KEY"; // Default API key
        private string Prompt = "Generate a map for a pirate DnD campaign"; // Default prompt

        [MenuItem("Tools/Terrain Generator")]
        public static void ShowWindow()
        {
            GetWindow<TerrainGeneratorWindow>("Terrain Generator");
        }

        private void OnGUI()
        {
            generator = (AutoRun)EditorGUILayout.ObjectField("AutoRun Script", generator, typeof(AutoRun), true);
            terrainPainter = (TerrainPainter)EditorGUILayout.ObjectField("Terrain Painter", terrainPainter, typeof(TerrainPainter), true);
            
            URL = EditorGUILayout.TextField("API URL", URL);
            ApiKey = EditorGUILayout.TextField("API Key", ApiKey);
            Prompt = EditorGUILayout.TextField("Prompt", Prompt);

            if (generator == null)
            {
                EditorGUILayout.HelpBox("Assign an object with the AutoRun script", MessageType.Warning);
                return;
            }
            
            if (terrainPainter == null)
            {
                EditorGUILayout.HelpBox("Assign a Terrain Painter object", MessageType.Warning);
                return;
            }

            if (GUILayout.Button("Generate Terrain"))
            {
                GenerateTerrainInEditor();
            }
        }

        private async void GenerateTerrainInEditor()
        {
            // Optionally pass URL and ApiKey into the generator here if needed
            await generator.EditorGenerate(URL, ApiKey, Prompt); // Add parameters if necessary
            terrainPainter.RepaintAll();
        }
    }
}