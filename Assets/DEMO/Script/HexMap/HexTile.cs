using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class HexTile : MonoBehaviour
{
    public HexTileGenerationSettings settings;

    public HexTileGenerationSettings.TileType tiletype;

    public GameObject tile;

    public GameObject fow;

    public Vector2Int offsetCordinates;

    public Vector3Int cubeCoordinates;

    public List<HexTile> neighbours;

    private bool isDirty = false;

    private void OnValidate()
    {
        if(tile == null) { return; }

        isDirty = true;
    }

    private void Update()
    {
        if(isDirty)
        {
            if(Application.isPlaying)
            {
                GameObject.Destroy(tile);
            }
            else
            {
                GameObject.DestroyImmediate(tile);
            }
            AddTile();
            isDirty = false;
        }
    }

    public void RollTileType()
    {
        tiletype = (HexTileGenerationSettings.TileType)Random.Range(0, 3);
    }

    public void AddTile()
    {
        tile = GameObject.Instantiate(settings.GetTile(tiletype),transform);
        if(gameObject.GetComponent<MeshCollider>() == null)
        {
            MeshCollider collider = gameObject.AddComponent<MeshCollider>();
            collider.sharedMesh = GetComponentInChildren<MeshFilter>().mesh;
        }
        transform.SetParent(tile.transform, true);
    }
}
