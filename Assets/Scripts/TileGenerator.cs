using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TileGenerator : MonoBehaviour
{
    public Tilemap tilemap;
    public TileBase ground;
    public TileBase underground;

    [SerializeField] private Transform playerPos;

    void Update()
    {
        if (GameManager.instance.state == EnumManager.GameState.START)
        {
            Move();

            Generate();
        }
    }

    private void Move()
    {
        transform.position = new Vector2(playerPos.position.x + 30f, 0);
    }

    private void Generate()
    {
        Vector3Int position = new Vector3Int((int)transform.position.x, -2, (int)transform.position.z);

        if (tilemap.GetTile(position) == null)
        {
            GenerateGround(position);
            GenerateUnderground(position);

            ResetOldTiles();
        }
    }

    private void GenerateGround(Vector3Int position)
    {
        tilemap.SetTile(position, ground);
    }

    private void GenerateUnderground(Vector3Int groundPosition)
    {
        int posY = groundPosition.y - 1;

        for (int i = 0; i < 4; i++)
        {
            Vector3Int undergroundPosition = new Vector3Int(groundPosition.x, posY, groundPosition.z);

            tilemap.SetTile(undergroundPosition, underground);

            posY--;
        }
    }

    private void ResetOldTiles()
    {
        Vector3Int position = new Vector3Int((int)playerPos.position.x - 30, -2, 0);

        if (tilemap.GetTile(position))
        {
            tilemap.SetTile(position, null);

            int posY = position.y - 1;

            for (int i = 0; i < 4; i++)
            {
                Vector3Int undergroundPosition = new Vector3Int(position.x, posY, position.z);

                tilemap.SetTile(undergroundPosition, null);

                posY--;
            }
        }
    }
}
