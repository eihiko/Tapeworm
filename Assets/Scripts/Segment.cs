using UnityEngine;
using System.Collections;
using System;

[RequireComponent(typeof(SpriteRenderer))]
public class Segment : MonoBehaviour {

    public Segment parent;
    public Segment child;
    public enum Direction { Up, Down, Left, Right };
    protected Direction direction;
    public Sprite tail;
    private SpriteRenderer sr;

    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if(null == child)
        {
            sr.sprite = tail;
        }
    }

    public Direction GetDirection()
    {
        return direction;
    }

    public void Move(Vector3 position)
    {
        transform.position = position;
    }

    public void Follow(Vector3 position)
    {
        Vector3 pos = transform.position;
        Move(position);
        if(null != child)
        {
            child.Follow(pos);
        }
        ChangeDirection(parent.direction);
    }

    public void Die()
    {
        Destroy(gameObject);
        if (null != child)
        {
            child.Die();
        }
    }

    public void ChangeDirection(Direction d)
    {
        if (direction != d)
        {
            direction = d;
            transform.eulerAngles = new Vector3(0, 0, DirectionToDegrees(d));
        }
    }

    protected float DirectionToDegrees(Direction d)
    {
        switch (d)
        {
            case Direction.Right:
                return 0f;
            case Direction.Up:
                return 90f;
            case Direction.Left:
                return 180f;
            case Direction.Down:
                return 270f;
            default:
                throw new ArgumentException();
        }
    }

}
