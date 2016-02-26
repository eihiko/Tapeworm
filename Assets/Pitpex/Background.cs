using UnityEngine;
using System.Collections;

[RequireComponent(typeof(SpriteRenderer))]
public class Background : MonoBehaviour {

    SpriteRenderer sr;

    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    void Update()
    {

        float spriteHeight = sr.sprite.bounds.size.y;
        float spriteWidth = sr.sprite.bounds.size.x;

        float viewHeight = Camera.main.orthographicSize * 2.0f;
        float viewWidth = (viewHeight / Screen.height) * Screen.width;

        Vector3 position = Camera.main.ViewportToWorldPoint(new Vector3(0, 1, 0));
        position.z = 0;
        transform.position = position;
        transform.localScale = new Vector3(viewWidth / spriteWidth, viewHeight / spriteHeight, 1);
    }

}
