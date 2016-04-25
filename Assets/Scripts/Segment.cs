using UnityEngine;
using System.Collections;
using System;

[RequireComponent(typeof(SpriteRenderer))]
public class Segment : MonoBehaviour {

    public Stage stage;
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

    void OnTriggerEnter2D(Collider2D other)
    {
        if (null != other.GetComponent<Food>())
        {
            stage.SpawnFood();
            Destroy(other.gameObject);
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
        if (ValidDirectionChange(direction, d))
        {
            direction = d;
            transform.eulerAngles = new Vector3(0, 0, DirectionToDegrees(d));
        }
    }

    public bool ValidDirectionChange(Direction d1, Direction d2)
    {
        bool valid = !((d1 != Direction.Up && d1 != Direction.Down && d2 != Direction.Up && d2 != Direction.Down)
            || (d1 != Direction.Left && d1 != Direction.Right && d2 != Direction.Left && d2 != Direction.Right));
        return valid;
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
