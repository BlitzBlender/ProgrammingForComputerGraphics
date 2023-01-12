using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaterialsBuilderBuilding
{
    private List<Material> materialsListBlack = new List<Material>();
    private List<Material> materialsListWhite = new List<Material>();
    private List<Material> materialListGray = new List<Material>();

    public MaterialsBuilderBuilding()
    {

        //Black
        Material blackMaterial = new Material(Shader.Find("Specular"));
        blackMaterial.color = Color.black;
        materialsListBlack.Add(blackMaterial);
        materialsListBlack.Add(blackMaterial);
        materialsListBlack.Add(blackMaterial);
        materialsListBlack.Add(blackMaterial);
        materialsListBlack.Add(blackMaterial);
        materialsListBlack.Add(blackMaterial);

        //White
        Material whiteMaterial = new Material(Shader.Find("Specular"));
        whiteMaterial.color = Color.white;
        materialsListWhite.Add(whiteMaterial);
        materialsListWhite.Add(whiteMaterial);
        materialsListWhite.Add(whiteMaterial);
        materialsListWhite.Add(whiteMaterial);
        materialsListWhite.Add(whiteMaterial);
        materialsListWhite.Add(whiteMaterial);

        //Gray
        Material grayMaterial = new Material(Shader.Find("Specular"));
        grayMaterial.color = Color.gray;
        materialListGray.Add(grayMaterial);
        materialListGray.Add(grayMaterial);
        materialListGray.Add(grayMaterial);
        materialListGray.Add(grayMaterial);
        materialListGray.Add(grayMaterial);
        materialListGray.Add(grayMaterial);
    }

    public List<Material> MaterialsListBlack(){
        return materialsListBlack;
    }

    public List<Material> MaterialsListWhite() { 
        return materialsListWhite;
    }

    public List<Material> MaterialsListGray()
    {
        return materialListGray;
    }
}
