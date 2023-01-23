using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Background : MonoBehaviour
{
    private Camera mainCamera;
    [SerializeField] private List<GameObject> backgrounds;

    private void Awake()
    {
        mainCamera = Camera.main;
    }

    void Update()
    {
        if (GameManager.instance.state == EnumManager.GameState.START)
        {
            transform.position = new Vector2(transform.position.x + 0.5f * Time.deltaTime, transform.position.y);

            if (mainCamera.transform.position.x - backgrounds[5].transform.position.x >= -10)
            {
                backgrounds[0].transform.position = new Vector2(
                    backgrounds[5].transform.position.x + backgrounds[5].GetComponent<SpriteRenderer>().bounds.size.x,
                    backgrounds[0].transform.position.y
                );

                GameObject newBackground = backgrounds[0];

                backgrounds.Remove(backgrounds[0]);
                backgrounds.Add(newBackground);
            }
        }
    }
}
