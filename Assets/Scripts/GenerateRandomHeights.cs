using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

[System.Serializable]
public class TerrainTextureData
{
    public Texture2D terrainTexture;
    public Vector2 TileSize;
}

[System.Serializable]
public class TreeData
{
    public GameObject TreeMesh;
    public float MinHeight;
    public float MaxHeight;
}




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

    [SerializeField]
    private bool flattenTerrain = true;

    [Header("Perlin Noise")]
    [SerializeField]
    private bool PerlinNoise = false;

    [SerializeField]
    private float PerlinNoiseWidthScale = 0.01f;

    [SerializeField]
    private float PerlinNoiseHeightScale = 0.01f;

    [Header("Texture Data")]
    [SerializeField]
    private List<TerrainTextureData> terrainTextureData;

    [SerializeField]
    private bool addTerrainTexture = false;

    [Header("Tree Data")]
    [SerializeField]
    private List<TreeData> treeData;

    [SerializeField]
    private int MaxTrees = 2000;

    [SerializeField]
    private int TreeSpacing = 10;

    [SerializeField]
    private bool addTrees = false;

    [SerializeField]
    private int TerrainLayerIndex;


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
        AddTerrainTextures();
        AddTrees();
    }

    private void GenerateHeights()
    {
        float[,] heightMap = new float[TerrainData.heightmapResolution, TerrainData.heightmapResolution];
        
        for(int width = 0; width < TerrainData.heightmapResolution; width++) 
        { 
            for(int height = 0; height < TerrainData.heightmapResolution; height++)
            {        
                if (PerlinNoise)
                {
                    heightMap[width, height] = Mathf.PerlinNoise(width * PerlinNoiseWidthScale, height * PerlinNoiseHeightScale);
                }
                else
                {
                    heightMap[width, height] = Random.Range(MinimumRandomHeight, MaximumRandomHeight);
                }
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

    private void AddTerrainTextures()
    { 
        TerrainLayer[] terrainlayers = new TerrainLayer[terrainTextureData.Count];

        for (int i = 0; i < terrainTextureData.Count; i++)
        {
            if (addTerrainTexture)
            {
                terrainlayers[i] = new TerrainLayer();
                terrainlayers[i].diffuseTexture = terrainTextureData[i].terrainTexture;
                terrainlayers[i].tileSize = terrainTextureData[i].TileSize;
            }
            else
            {
                terrainlayers[i] = new TerrainLayer();
                terrainlayers[i].diffuseTexture = null;
            }
        }

        TerrainData.terrainLayers = terrainlayers;
    }

    private void AddTrees()
    {
        TreePrototype[] trees = new TreePrototype[treeData.Count];

        for(int i = 0; i < treeData.Count; i++)
        {
            trees[i] = new TreePrototype();
            trees[i].prefab = treeData[i].TreeMesh;
        }

        TerrainData.treePrototypes = trees;

        List<TreeInstance> treeInstanceList = new List<TreeInstance>();

        if (addTrees)
        {
            for(int z = 0; z < TerrainData.size.z; z += TreeSpacing)
            {
                for(int x = 0; x < TerrainData.size.x; x += TreeSpacing)
                {
                    for(int TreeIndex = 0; TreeIndex < trees.Length; TreeIndex++)
                    {
                        if(treeInstanceList.Count < MaxTrees)
                        {
                            float CurrentHeight = TerrainData.GetHeight(x, z) / TerrainData.size.y;

                            if(CurrentHeight >= treeData[TreeIndex].MinHeight &&  CurrentHeight <= treeData[TreeIndex].MaxHeight)
                            {
                                float RandomX = (x + Random.Range(-5.0f, 5.0f)) / TerrainData.size.x;
                                float RandomZ = (z + Random.Range(-5.0f, 5.0f)) / TerrainData.size.z;

                                Vector3 TreePosition = new Vector3(RandomX * TerrainData.size.x, 
                                                                   CurrentHeight * TerrainData.size.y, 
                                                                   RandomZ * TerrainData.size.z) + this.transform.position;

                                RaycastHit raycastHit;
                                int LayerMask = 1 << TerrainLayerIndex;

                                if(Physics.Raycast(TreePosition, -Vector3.up, out raycastHit, 100, LayerMask) || Physics.Raycast(TreePosition, Vector3.up, out raycastHit, 100, LayerMask))
                                {
                                    float TreeDistance = (raycastHit.point.y - this.transform.position.y) / TerrainData.size.y;

                                    TreeInstance treeInstance = new TreeInstance();
                                    treeInstance.position = new Vector3(RandomX, TreeDistance, RandomZ);
                                    treeInstance.rotation = Random.Range(0, 360);
                                    treeInstance.prototypeIndex = TreeIndex;
                                    treeInstance.color = Color.white;
                                    treeInstance.lightmapColor = Color.white;
                                    treeInstance.heightScale = 0.95f;
                                    treeInstance.widthScale = 0.95f;

                                    treeInstanceList.Add(treeInstance);
                                }
                            }
                        }
                    }
                }
            }
        }

        TerrainData.treeInstances = treeInstanceList.ToArray();
    }

    private void OnDestroy()
    {
        if (flattenTerrain)
        {
           FlattenTerrain();
        }
        
    }
}
