using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Foreground : MonoBehaviour
{
    private Camera mainCamera;
    [SerializeField] private List<GameObject> foregrounds;

    private void Awake()
    {
        mainCamera = Camera.main;
    }

    void Update()
    {
        if (GameManager.instance.state == EnumManager.GameState.START)
        {
            transform.position = new Vector2(transform.position.x + 0.25f * Time.deltaTime, transform.position.y);

            if (mainCamera.transform.position.x - foregrounds[2].transform.position.x >= -5)
            {
                foregrounds[0].transform.position = new Vector2(
                    foregrounds[2].transform.position.x + foregrounds[2].GetComponent<SpriteRenderer>().bounds.size.x, 
                    foregrounds[0].transform.position.y
                );

                GameObject newBackground = foregrounds[0];

                foregrounds.Remove(foregrounds[0]);
                foregrounds.Add(newBackground);
            }
        }
    }
}
