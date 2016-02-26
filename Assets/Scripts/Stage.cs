using UnityEngine;
using System.Collections;

[RequireComponent(typeof(SpriteRenderer))]
public class Stage : MonoBehaviour {

    public Food food;
    private SpriteRenderer sr;
    public float borderX = 0.2f;
    public float borderY = 0.2f;

    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        SpawnFood();
    }

    void Update()
    {
    }

    public void SpawnFood()
    {
        float x = Random.Range(borderX / 2, sr.bounds.size.x - (borderX / 2)) - (sr.bounds.size.x / 2);
        float y = Random.Range(borderY / 2, sr.bounds.size.y - (borderY / 2)) - (sr.bounds.size.y / 2);
        Food f = Instantiate(food);
        f.transform.position = new Vector3(x, y, 0);
    }
}
