using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sky : MonoBehaviour
{
    private Camera mainCamera;
    [SerializeField] private List<GameObject> skies;

    private void Awake()
    {
        mainCamera = Camera.main;
    }

    void Update()
    {
        if (GameManager.instance.state == EnumManager.GameState.START)
        {
            if (mainCamera.transform.position.x - skies[1].transform.position.x >= 0)
            {
                skies[0].transform.position = new Vector2(
                    skies[1].transform.position.x + skies[1].GetComponent<SpriteRenderer>().bounds.size.x,
                    skies[0].transform.position.y
                );

                GameObject newBackground = skies[0];

                skies.Remove(skies[0]);
                skies.Add(newBackground);
            }
        }
    }
}
