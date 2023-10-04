using Pathfinding;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

public class PetAI : MonoBehaviour
{
    public UnityEvent OnSearchEnemy;
    
    [Header("Pathfinding")]
    public Transform currentTarget;
    public Transform Player;
    public float activateDistance = 50f;
    public float pathUpdateSeconds = 0.5f;
    public float distanceBtwTargets;
    public float distanceBtwPlayer = 5f;
    public float distanceBtwEnemy = 0.1f;
     

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

    private Path path;
    private int currentWaypoint = 0;
    RaycastHit2D isGrounded;
    Seeker seeker;
    Rigidbody2D rb;

    private float moveSpeed;
    private float searchDistance = 8f;
    
    [SerializeField] private LayerMask enemyLayers;
    [SerializeField] private Animator _animator;
    
    public void Awake()
    {
        Player = GameObject.FindWithTag("Player").GetComponent<Transform>();
        ChangeTarget(Player, distanceBtwPlayer);
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

        FindClosestObjectInRange();

        if (currentTarget == null)
        {
            ChangeTarget(Player, distanceBtwPlayer);
        }
        
    }

    private void UpdatePath()
    {
        if (followEnabled && TargetInDistance() && seeker.IsDone() && !TargetOutField())
        {
            seeker.StartPath(rb.position, currentTarget.position, OnPathComplete);
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
            gameObject.transform.localScale = new Vector3(-1f, 1f, 1f);
        } else if (rb.velocity.x > -0.1f)
        {
            gameObject.transform.localScale = new Vector3(1f, 1f, 1f);
        }
    }

    private bool TargetInDistance()
    {
        return Vector2.Distance(transform.position, currentTarget.transform.position) < activateDistance;
    }

    private bool TargetOutField()
    {
        return Vector2.Distance(transform.position, currentTarget.transform.position) < distanceBtwTargets;
    }

    private void OnPathComplete(Path p)
    {
        if (!p.error)
        {
            path = p;
            currentWaypoint = 0;
        }
    }

    public void SetMainTarget()
    {
        currentTarget = Player;
        distanceBtwTargets = distanceBtwPlayer;
    }
    
    private void ChangeTarget(Transform target, float distance)
    {
        currentTarget = target;
        distanceBtwTargets = distance;
    }
    
    public void FindClosestObjectInRange()
    {
        // Получаем массив объектов в радиусе взгляда
        var objectsInArea = Physics2D.OverlapCircleAll(gameObject.transform.position, searchDistance, enemyLayers);
        
        // Проходим по объектам и ищем ближайший
        GameObject closestObject = null;
        var minDist = searchDistance;
        foreach (var obj in objectsInArea)
        {
            // Получаем дистанцию до объекта
            var dist = Vector2.Distance(obj.transform.position, gameObject.transform.position);
            if (dist < minDist)
            {
                minDist = dist;
                closestObject = obj.gameObject;
            }
        }

        if (closestObject != null)
        {
            ChangeTarget(closestObject.transform, distanceBtwEnemy);
            
        }
        else
        {
            ChangeTarget(Player, distanceBtwPlayer);
        }
        
        
    }
}
