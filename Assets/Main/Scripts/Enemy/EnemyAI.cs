using System;
using UnityEngine;
using Pathfinding;

public class EnemyAI : MonoBehaviour
{
    /*public Transform currentTarget;

    public float speed = 200f;
    public float nextWaypointDistance = 3f;
    public float stoppingDistance;
    public float seekerDistance;

    Path path;
    int currentWaypoint = 0;
    bool reachedEndOfPath = false;

    Seeker seeker;
    Rigidbody2D rb;

    void Start()
    {
        seeker = GetComponent<Seeker>();
        rb = GetComponent<Rigidbody2D>();

        InvokeRepeating("UpdatePath", 0f, .5f);
    }

    void UpdatePath()
    {
        if (seeker.IsDone() && Vector2.Distance(rb.position, currentTarget.position) < seekerDistance)
        {
            seeker.StartPath(rb.position, currentTarget.position, OnPathComplete);
        }
        else
        {
            transform.position = this.transform.position;
        }
    }

    void OnPathComplete(Path p)
    {
        if(!p.error)
        {
            path = p;
            currentWaypoint = 0;
        }
    }

    void FixedUpdate()
    {
        if (path == null)
        {
            return;
        }

        if (currentWaypoint >= path.vectorPath.Count)
        {
            reachedEndOfPath = true;
            return;
        }
        else
        {
            reachedEndOfPath = false;
        }

        if (Vector2.Distance(transform.position, currentTarget.position) > stoppingDistance)
        {
            Vector2 direction = ((Vector2)path.vectorPath[currentWaypoint] - rb.position).normalized;
            Vector2 force = direction * speed * Time.deltaTime;
            rb.AddForce(force);
            
            
        }

        else if(Vector2.Distance(transform.position, currentTarget.position) < stoppingDistance) 
        {
            transform.position = this.transform.position;
        }
        
        float distance = Vector2.Distance(rb.position, path.vectorPath[currentWaypoint]);
            
        if (distance < nextWaypointDistance)
        {
            currentWaypoint++;
        }*/
    [Header("Pathfinding")]
    public Transform target;
    public float activateDistance = 50f;
    public float pathUpdateSeconds = 0.5f;
    public float distanceBtwTargets = 10f;

    [Header("Physics")]
    public float speed = 200f;
    public float nextWaypointDistance = 3f;
    public float jumpNodeHeightRequirement = 0.8f;
    public float jumpModifier = 0.3f;
    public float jumpCheckOffset = 0.1f;

    [Header("Custom Behavior")]
    public bool followEnabled = true;
    public bool jumpEnabled = false;
    public bool directionLookEnabled = true;
    public bool meleeMode;
    
    private Path path;
    private int currentWaypoint = 0;
    RaycastHit2D isGrounded;
    Seeker seeker;
    Rigidbody2D rb;

    private bool isDead = false;

    [SerializeField] private Animator _animator;

    [SerializeField] private Transform attackPoint;

    private float moveSpeed;

    public void Awake()
    {
        target = GameObject.FindWithTag("Player").GetComponent<Transform>();
    }

    public void Start()
    {
        seeker = GetComponent<Seeker>();
        rb = GetComponent<Rigidbody2D>();

        InvokeRepeating("UpdatePath", 0f, pathUpdateSeconds);
    }

    private void FixedUpdate()
    {
        moveSpeed = rb.velocity.magnitude;
        
        _animator.SetFloat("Speed", moveSpeed);

        if (TargetInDistance() && followEnabled && !TargetOutField())
        {
            PathFollow();
        }
    }

    private void UpdatePath()
    {
        if (followEnabled && TargetInDistance() && seeker.IsDone() && !TargetOutField() && !isDead)
        {
            seeker.StartPath(rb.position, target.position, OnPathComplete);
        }
    }

    private void PathFollow()
    {
        if (path == null)
        {
            return;
        }

        // Reached end of path
        if (currentWaypoint >= path.vectorPath.Count)
        {
            return;
        }

        // See if colliding with anything
        Vector3 startOffset = transform.position - new Vector3(0f, GetComponent<Collider2D>().bounds.extents.y + jumpCheckOffset);
        isGrounded = Physics2D.Raycast(startOffset, -Vector3.up, 0.05f);
        
        // Direction Calculation
        Vector2 direction = ((Vector2)path.vectorPath[currentWaypoint] - rb.position).normalized;
        Vector2 force = direction * speed * Time.deltaTime;

        // Jump
        if (jumpEnabled && isGrounded)
        {
            if (direction.y > jumpNodeHeightRequirement)
            {
                rb.AddForce(Vector2.up * speed * jumpModifier);
            }
        }
        
        
        
        // Movement
        rb.AddForce(force);
        // Next Waypoint
        float distance = Vector2.Distance(rb.position, path.vectorPath[currentWaypoint]);
        if (distance < nextWaypointDistance)
        {
            currentWaypoint++;
        }
        
        if (rb.velocity.x < 0.1f)
        {
            attackPoint.localScale = new Vector3(-.3f, .8f, 0f);
            gameObject.transform.localScale = new Vector3(-1f, 1f, 1f);
        } else if (rb.velocity.x > -0.1f)
        {
            attackPoint.localScale = new Vector3(.3f, .8f, 0f);
            gameObject.transform.localScale = new Vector3(1f, 1f, 1f);
        }
    }

    private bool TargetInDistance()
    {
        return Vector2.Distance(transform.position, target.transform.position) < activateDistance;
    }

    private bool TargetOutField()
    {
        return Vector2.Distance(transform.position, target.transform.position) < distanceBtwTargets;
    }

    public void IsDead()
    {
        isDead = true;
    }
    
    private void OnPathComplete(Path p)
    {
        if (!p.error)
        {
            path = p;
            currentWaypoint = 0;
        }
    }
}
