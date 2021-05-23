using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragDrop : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
	public GameObject prefab;
	GameObject hoverPrefab;

	// [SerializeField] private GameObject radius;

	//public SpriteRenderer spriteRenderer;

	// Use this for initialization
	void Start()
	{
		hoverPrefab = Instantiate(prefab);
		//hoverPrefab.transform.Rotate(new Vector3(90f, 0f, 0f));

		RemoveScriptsFromPrefab();
		//AdjustPrefabAlpha();
		hoverPrefab.SetActive(false);

		//this.spriteRenderer = GetComponent<SpriteRenderer>();

	}

	
	void RemoveScriptsFromPrefab()
	{
		Component[] components = hoverPrefab.GetComponentsInChildren<ArcherTurret>();
		foreach (Component component in components)
		{
			Destroy(component);
		}
	}
	

	/*
	void AdjustPrefabAlpha()
	{
		MeshRenderer[] meshRenderers = hoverPrefab.GetComponentsInChildren<MeshRenderer>();
		for (int i = 0; i < meshRenderers.Length; i++)
		{
			Material mat = meshRenderers[i].material;
			meshRenderers[i].material.color = new Color(mat.color.r, mat.color.g, mat.color.b, 0.5f);
		}
	}
	*/

	// Update is called once per frame
	void Update()
	{

	}

	void IBeginDragHandler.OnBeginDrag(PointerEventData eventData)
	{
		//Debug.Log("Beginning drag");
	}

	public void OnDrag(PointerEventData eventData)
	{
		//Debug.Log(eventData);
		RaycastHit[] hits;
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

		hits = Physics.RaycastAll(ray, 50f);

		
		if (hits != null && hits.Length > 0)
		{
			int gridColliderIndex = GetGridColliderIndex(hits);
			int pathColliderIndex = GetPathColliderIndex(hits);
			//Debug.Log("1");

			
			if (gridColliderIndex != -1 && pathColliderIndex == -1 && GameManager.coins >= 100)
			{
				hoverPrefab.transform.position = hits[gridColliderIndex].point;
				hoverPrefab.SetActive(true);
				//Debug.Log (hits [gridColliderIndex].point);
			}
			else
			{
				hoverPrefab.SetActive(false);
				Debug.Log("Cannot build here!");
			}
			
		}
		
	}

	int GetGridColliderIndex(RaycastHit[] hits)
	{
		for (int i = 0; i < hits.Length; i++)
		{
			if (hits[i].collider.gameObject.name.Equals("TerrainCollider"))
			{
				return i;
			}
		}

		return -1;
	}

	int GetPathColliderIndex(RaycastHit[] hits)
    {
		for (int i = 0; i < hits.Length; i++)
		{
			if (hits[i].collider.gameObject.name.StartsWith("Path"))
			{
				return i;
			}
		}

		return -1;
	}


	public void OnEndDrag(PointerEventData eventData)
	{
		//Debug.Log("End");
		// If the prefab instance is active after dragging stopped, it means
		// it's in the arena so (for now), just drop it in.


		
		if (hoverPrefab.activeSelf)
		{
			Instantiate(prefab, hoverPrefab.transform.position, Quaternion.identity);
			GameManager.coins -= 100;
		}
		
		

		// Then set it to inactive ready for the next drag!
		hoverPrefab.SetActive(false);
	}

}