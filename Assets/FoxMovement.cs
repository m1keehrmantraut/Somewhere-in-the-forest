using System.Collections;
using UnityEngine;
using UnityEngine.AI;


public class FoxMovement : MonoBehaviour
{
    public Transform Target;
    public float updateSpeed = 0.1f; 

    private NavMeshAgent agent;
    
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;
        StartCoroutine(FollowTarget());
    }
    
    private IEnumerator FollowTarget()
    {
        WaitForSeconds wait = new WaitForSeconds(updateSpeed);
        
        while (enabled)
        {
            agent.SetDestination(Target.transform.position);

            yield return wait;
        }
        
    }
}
