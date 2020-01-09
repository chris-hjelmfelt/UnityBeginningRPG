using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipmentManager : MonoBehaviour
{
	#region Singleton
    public static EquipmentManager instance;
	
	void Awake ()
	{
		instance = this;
	}
	#endregion
	
	public Equipment[] defaultItems;
	public SkinnedMeshRenderer targetMesh;
	public SkinnedMeshRenderer npcMesh;
	public Equipment npcShirt;
	public Equipment npcPants;
	public Equipment defaultTool;
	public static Equipment equippedTool;
	Equipment[] currentEquipment;
	SkinnedMeshRenderer[] currentMeshes;	
	
	public delegate void OnEquipmentChanged(Equipment newItem, Equipment oldItem);  // Other scripts can listen for changes to equipment
	public OnEquipmentChanged onEquipmentChanged;
	
	Inventory inventory;
	
	void Start ()
	{
		inventory = Inventory.instance;
		int numSlots = System.Enum.GetNames(typeof(EquipmentSlot)).Length;
		currentEquipment = new Equipment[numSlots];
		currentMeshes = new SkinnedMeshRenderer[numSlots];
		equippedTool = defaultTool;
		EquipDefaultItems();
		npcClothes();
	}
	
	public void Equip (Equipment newItem)
	{
		int slotIndex = (int)newItem.equipSlot;
		Equipment oldItem = Unequip(slotIndex);  // Take off item in slot	
				
		if (onEquipmentChanged != null)
		{
			onEquipmentChanged.Invoke(newItem, oldItem);
		}
		if((int)newItem.equipSlot == 3)
		{
			equippedTool = newItem;
		}
		
		SetEquipmentBlendShapes(newItem, 100);  // This repairs body showing through armor in spots
		currentEquipment[slotIndex] = newItem;
		
		// Put the mesh for the armor onto the player
		SkinnedMeshRenderer newMesh = Instantiate<SkinnedMeshRenderer>(newItem.mesh);
		newMesh.transform.parent = targetMesh.transform;		
		newMesh.bones = targetMesh.bones;
		newMesh.rootBone = targetMesh.rootBone;
		currentMeshes[slotIndex] = newMesh;
	}
	
	public void npcClothes()
	{
		// Put the mesh for shirt and pants on the NPC
		SkinnedMeshRenderer shirtMesh = Instantiate<SkinnedMeshRenderer>(npcShirt.mesh);
		SkinnedMeshRenderer pantsMesh = Instantiate<SkinnedMeshRenderer>(npcPants.mesh);
		shirtMesh.transform.parent = npcMesh.transform;		
		shirtMesh.bones = npcMesh.bones;
		shirtMesh.rootBone = npcMesh.rootBone;
		pantsMesh.transform.parent = npcMesh.transform;		
		pantsMesh.bones = npcMesh.bones;
		pantsMesh.rootBone = npcMesh.rootBone;
	}
	
	public Equipment Unequip (int slotIndex)
	{
		if (currentEquipment[slotIndex] != null)
		{
			if(slotIndex == 3)
			{
				equippedTool = defaultTool;
			}
			
			if (currentMeshes[slotIndex] != null) // Take armor mesh off of player body
			{
				Destroy(currentMeshes[slotIndex].gameObject); 
			}
			Equipment oldItem = currentEquipment[slotIndex];
			SetEquipmentBlendShapes(oldItem, 0);  // This repairs body showing through armor in spots
			inventory.Add(oldItem);
			
			currentEquipment[slotIndex] = null;
			
			if (onEquipmentChanged != null)
			{
				onEquipmentChanged.Invoke(null, oldItem);
			}			
			return oldItem;
		}
		return null;		
	}
	
	public void UnequipAll ()
	{
		for (int i = 0; i < currentEquipment.Length; i++)
		{
			Unequip(i);
			EquipDefaultItems();
		}
	}
	
	void SetEquipmentBlendShapes(Equipment item, int weight)  // This repairs body showing through armor in spots
	{
		foreach (EquipmentMeshRegion blendShape in item.coveredMeshRegions)
		{	
			targetMesh.SetBlendShapeWeight((int)blendShape, weight);
		}
	}
	
	void EquipDefaultItems ()  // make sure character has some sort of pants, hair, etc as a default
	{
		foreach (Equipment item in defaultItems)
		{
			Equip(item);
		}
	}
	
	void Update ()
	{
		if (Input.GetKeyDown(KeyCode.U))
			UnequipAll();
	}
	
}
