using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class ItemPlacementManager : MonoBehaviour
{
    [SerializeField] private GameObject[] itemsToPlace;
    [SerializeField] private GameObject[] itemsToHang;
    [SerializeField] private GameObject enemy;
    [SerializeField] private GameObject water;
    private List<GameObject> allItems;

    HashSet<Vector3Int> nearWallTilesUp;
    HashSet<Vector3Int> nearWallTilesDown;
    HashSet<Vector3Int> nearWallTilesRight;
    HashSet<Vector3Int> nearWallTilesLeft;
    HashSet<Vector3Int> nearWallTiles;
    HashSet<Vector3Int> innerTiles;

    private void Start()
    {
        allItems = new List<GameObject>();
    }

    public void Reset()
    {
        if (allItems.Count == 0)
            return;

        Debug.Log(allItems.Count);
        foreach (GameObject item in allItems)
            Destroy(item);
        allItems.Clear();
    }

    public void PlaceItems(HashSet<Vector2Int> floorPositions, List<Vector2Int> roomCenterPoints)
    {
        nearWallTilesUp=new HashSet<Vector3Int>();
        nearWallTilesDown=new HashSet<Vector3Int>();
        nearWallTilesRight = new HashSet<Vector3Int>();
        nearWallTilesLeft = new HashSet<Vector3Int>();
        nearWallTiles = new HashSet<Vector3Int>();
        innerTiles = new HashSet<Vector3Int>();
        
        
        foreach (Vector2Int pos in floorPositions)
        {
            if (!roomCenterPoints.Contains(pos))
            {
                if (!floorPositions.Contains(pos + Vector2Int.up))
                {
                    if (floorPositions.Contains(pos + Vector2Int.down) && floorPositions.Contains(pos + Vector2Int.right) &&
                        floorPositions.Contains(pos + Vector2Int.left))
                    {
                        nearWallTilesUp.Add((Vector3Int)pos);
                        nearWallTiles.Add((Vector3Int)pos);
                    }
                }
                else if (!floorPositions.Contains(pos + Vector2Int.down))
                {
                    if (floorPositions.Contains(pos + Vector2Int.up) && floorPositions.Contains(pos + Vector2Int.right) &&
                        floorPositions.Contains(pos + Vector2Int.left))
                    {
                        nearWallTilesDown.Add((Vector3Int)pos);
                        nearWallTiles.Add((Vector3Int)pos);
                    }
                }
                else if (!floorPositions.Contains(pos + Vector2Int.right))
                {
                    if (floorPositions.Contains(pos + Vector2Int.up) && floorPositions.Contains(pos + Vector2Int.down) &&
                        floorPositions.Contains(pos + Vector2Int.left))
                    {
                        nearWallTilesRight.Add((Vector3Int)pos);
                        nearWallTiles.Add((Vector3Int)pos);
                    }
                }
                else if (!floorPositions.Contains(pos + Vector2Int.left))
                {
                    if (floorPositions.Contains(pos + Vector2Int.up) && floorPositions.Contains(pos + Vector2Int.down) &&
                        floorPositions.Contains(pos + Vector2Int.right))
                    {
                        nearWallTilesLeft.Add((Vector3Int)pos);
                        nearWallTiles.Add((Vector3Int)pos);
                    }
                }
                else
                {
                    innerTiles.Add((Vector3Int)pos);
                }
            }
        }
            
        PlaceItems();
    }

    public void PlaceEnemies(HashSet<Vector2Int> floorPositions)
    {
        int enemyCount = Random.Range(4, 7);
        for (int i = 0; i < enemyCount; i++)
        {
            if (innerTiles.Count == 0)
                break;
            var position = innerTiles.ElementAt(Random.Range(0, innerTiles.Count));
            allItems.Add(Instantiate(enemy, position, Quaternion.identity));
            innerTiles.Remove(position);
        }
    }

    private void PlaceItems()
    {
        int itemCount = Random.Range(7, 10);
        int itemHangCount = Random.Range(4, 6);
        int waterCount = Random.Range(1, 4);

        for(int i = 0; i < itemCount; i++)
        {
            if (innerTiles.Count == 0)
                break;
            var position = innerTiles.ElementAt(Random.Range(0, innerTiles.Count));
            allItems.Add(Instantiate(itemsToPlace[Random.Range(0,itemsToPlace.Length)], position, Quaternion.identity));
            innerTiles.Remove(position);
        }
        for(int i = 0; i < itemHangCount; i++)
        {
            if (nearWallTilesUp.Count == 0)
                break;
            var position = nearWallTilesUp.ElementAt(Random.Range(0, nearWallTilesUp.Count));
            allItems.Add(Instantiate(itemsToHang[Random.Range(0, itemsToHang.Length)], position+Vector3Int.up, Quaternion.identity));
            nearWallTilesUp.Remove(position);
        }
        for (int i = 0; i < waterCount; i++)
        {
            if (nearWallTiles.Count == 0)
                break;
            var position = nearWallTiles.ElementAt(Random.Range(0, nearWallTiles.Count));
            allItems.Add(Instantiate(water, position, Quaternion.identity));
            nearWallTiles.Remove(position);
        }
        
    }


}
