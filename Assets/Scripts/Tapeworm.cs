using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine.UI;

[RequireComponent(typeof(Collider2D))]
public class Tapeworm : Segment {

    public float tickPoints = 1f;
    public float tickPointsGrowth = 1f;
    public float foodPoints = 100f;
    public float foodPointsGrowth = 1.5f;
    public float delayDecay = 0.0001f;
    public float delay = 0.1f;
    public float growTime = 0.2f;
    public float growGrowthRate = 0.05f;
    public Segment segment;
    public Stage stage;
    public Text segmentText;
    private int segmentCount;
    private Direction bufferedDirection;
    private float size;
    private float ticks;
    private float growTicks;
    private LinkedList<Segment> segments;
    private bool growing;
  

	void Start () {
        segmentCount = 1;
        direction = Direction.Right;
        size = GetComponent<Collider2D>().bounds.size.x;
        segments = new LinkedList<Segment>();
        growing = false;
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (Input.GetKeyDown(KeyCode.D))
        {
            bufferedDirection = Direction.Right;
        }
        else if (Input.GetKeyDown(KeyCode.W))
        {
            bufferedDirection = Direction.Up;
        }
        else if (Input.GetKeyDown(KeyCode.A))
        {
            bufferedDirection = Direction.Left;
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            bufferedDirection = Direction.Down;
        }

    }

    void FixedUpdate()
    {
        if(growing && growTicks < growTime)
        {
            growTicks += Time.deltaTime;
        }
        else
        {
            growing = false;
            growTicks = 0f;
        }

        if(ticks < delay)
        {
            ticks += Time.deltaTime;
            return;
        }

        ticks = 0f;
        Pitpex.AddScore(tickPoints);
        tickPoints += tickPointsGrowth;
        delay -= delayDecay;

        ChangeDirection(bufferedDirection);
        if (growing)
        {
            Grow();
        }
        else
        {
            Lead();
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        Food f = other.gameObject.GetComponent<Food>();
        if(null != f)
        {
            Destroy(f.gameObject);
            Pitpex.AddScore(foodPoints);
            foodPoints *= foodPointsGrowth;
            stage.SpawnFood();
            growing = true;
            growTime += growGrowthRate;
        }
        else
        {
            if (null != child && other.gameObject != child.gameObject)
            {
                Die();
            }
        }
    }

    private void Grow()
    {
        Vector3 pos = transform.position;
        Move();
        Segment s = Instantiate(segment);
        s.parent = this;
        s.child = this.child;
        if(null != this.child)
        {
            s.child.parent = s;
        }
        s.Move(pos);
        this.child = s;
        segmentCount += 1;
        UpdateSegmentText();
    }

    private void Move()
    {
        transform.Translate(0.01f + size * 2, 0, 0);
    }

    private void Lead()
    {
        Vector3 pos = transform.position;
        Move();
        if(null != child)
        {
            child.Follow(pos);
        }
        
    }

    private void UpdateSegmentText()
    {
        segmentText.text = "Tapeworm Segments: " + segmentCount;
    }

}
