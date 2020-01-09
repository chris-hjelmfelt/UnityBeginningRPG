using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConvertToRegularMesh : MonoBehaviour
{
	[ContextMenu("Convert to regular mesh")]
    void Convert ()  // Skinned meshes are good for going on the body but not by themselves in the world
	{
		SkinnedMeshRenderer skinnedMeshRenderer = GetComponent<SkinnedMeshRenderer>();
		MeshRenderer meshRenderer = gameObject.AddComponent<MeshRenderer>();
		MeshFilter meshFilter = gameObject.AddComponent<MeshFilter>();
		
		meshFilter.sharedMesh = skinnedMeshRenderer.sharedMesh;
		meshRenderer.sharedMaterials = skinnedMeshRenderer.sharedMaterials;
		
		DestroyImmediate(skinnedMeshRenderer);
		DestroyImmediate(this);
	}
}
