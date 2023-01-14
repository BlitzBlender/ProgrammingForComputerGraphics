using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateRandomHeights : MonoBehaviour
{
    private Terrain Terrain;
    private TerrainData TerrainData;

    [SerializeField]
    [Range(0f, 1f)]
    private float MinimumRandomHeight = 0f;

    [SerializeField]
    [Range(0f, 1f)]
    private float MaximumRandomHeight = 0.1f;


    // Start is called before the first frame update
    private void Start()
    {
        if (Terrain == null) 
        {
            Terrain = this.GetComponent<Terrain>();
        }

        if (TerrainData == null)
        {
            TerrainData = Terrain.activeTerrain.terrainData;
        }

        GenerateHeights();
    }

    private void GenerateHeights()
    {
        float[,] heightMap = new float[TerrainData.heightmapResolution, TerrainData.heightmapResolution];
        
        for(int width = 0; width < TerrainData.heightmapResolution; width++) 
        { 
            for(int height = 0; height < TerrainData.heightmapResolution; height++)
            {
                heightMap[width, height] = Random.Range(MinimumRandomHeight, MaximumRandomHeight);
            }      
        }

        TerrainData.SetHeights(0, 0, heightMap);
    }


    private void FlattenTerrain()
    {
        float[,] heightMap = new float[TerrainData.heightmapResolution, TerrainData.heightmapResolution];

        for (int width = 0; width < TerrainData.heightmapResolution; width++)
        {
            for (int height = 0; height < TerrainData.heightmapResolution; height++)
            {
                heightMap[width, height] = 0;
            }
        }

        TerrainData.SetHeights(0, 0, heightMap);
    }

    private void OnDestroy()
    {
        FlattenTerrain();
    }
}
