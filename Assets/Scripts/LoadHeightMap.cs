using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadHeightMap : MonoBehaviour
{
    private Terrain Terrain;
    private TerrainData TerrainData;

    [SerializeField]
    private Texture2D HeightMapImage;

    [SerializeField]
    private Vector3 HeightMapScale = new Vector3(1, 1, 1);


    [SerializeField]
    private bool loadHeightMap = true;

    [SerializeField]
    private bool TerrainFlattenOnExit = true;

    void Start()
    {
        if (Terrain == null)
        {
            Terrain = this.GetComponent<Terrain>();
        }

        if (TerrainData == null)
        {
            TerrainData = this.GetComponent<TerrainData>();
        }

        if (loadHeightMap)
        {
            LoadMapHeightImage();
        }

    }

    void LoadMapHeightImage()
    {
        float[,] heightMap = new float[TerrainData.heightmapResolution, TerrainData.heightmapResolution];
        
        for (int width = 0; width < TerrainData.heightmapResolution; width++)
        {
            for (int height = 0; height < TerrainData.heightmapResolution; height++)
            {
                heightMap[width, height] = HeightMapImage.GetPixel((int)(width * HeightMapScale.x), (int)(height * HeightMapScale.z)).grayscale * HeightMapScale.y;
            }
        }

        TerrainData.SetHeights(0, 0, heightMap);
    }


    void FlattenTerrain()
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

    void OnDestroy()
    {
        if (TerrainFlattenOnExit)
        {
            FlattenTerrain();
        }
    }
}
