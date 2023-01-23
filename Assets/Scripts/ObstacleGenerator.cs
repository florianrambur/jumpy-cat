using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleGenerator : MonoBehaviour
{
    public List<GameObject> obstaclePrefabs = new List<GameObject>();

    [SerializeField] private GameObject player;
    [SerializeField] private int nextPosX = 35;

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
        transform.position = new Vector2(player.transform.position.x + 30f, 0);
    }

    private void Generate()
    {
        int currentPosX = (int)transform.position.x;

        if (currentPosX == nextPosX)
        {
            GameObject prefab = obstaclePrefabs[Random.Range(0, obstaclePrefabs.Count)];

            float posY = prefab.GetComponent<Obstacle>().positionY;

            Instantiate(prefab, new Vector2(transform.position.x, posY), Quaternion.identity);

            nextPosX = Random.Range(currentPosX + 5, currentPosX + 12);
        }
    }
}
