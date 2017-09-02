using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class HumanAI : MonoBehaviour {
    [SerializeField]
    NavMeshAgent agent;

    [SerializeField]
    float walkSpeed = 5f;
    [SerializeField]
    float runSpeed = 10f;
    Vector3 targetPos;
    [SerializeField] float scareDistance = 10f;

    [SerializeField] float maxRoamDistance = 10f;

    GameObject lastEntitySpotted;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        targetPos = transform.position;
        StartCoroutine(Roam());
    }

    private void Update()
    {
        
    }

    IEnumerator Roam()
    {
        while (true)
        {
            if (DetectThreat())
            {
                Vector3 fleePoint = transform.position + -(lastEntitySpotted.transform.position - transform.position) * 5;
                //Debug.DrawLine(fleePoint, fleePoint + new Vector3(0, 5, 0),Color.blue, 3f);
                NavMeshHit hit;
                NavMesh.SamplePosition(fleePoint, out hit, maxRoamDistance, 1);
                targetPos = hit.position;
                
                //targetPos = RandomNavSphere(transform.position, maxRoamDistance, 1);
                agent.SetDestination(targetPos);
                agent.speed = runSpeed;
                agent.isStopped = false;
                yield return new WaitForSeconds(1f);
                if (!DetectThreat())
                {
                    //targetPos = transform.position;
                    //agent.SetDestination(targetPos);
                }
            }
            if (Vector3.Distance(transform.position, targetPos) < 1.2)
            {
                //agent.isStopped = true;
                yield return new WaitForSeconds(Random.Range(0, 5));
                targetPos = RandomNavSphere(transform.position, maxRoamDistance, 1);
                agent.SetDestination(targetPos);
                agent.speed = walkSpeed;
                agent.isStopped = false;
            }
            yield return new WaitForSeconds(0.2f);
        }
    }

    bool DetectThreat()
    {
        var player = FindObjectOfType<Player>();
        if (player == null) return false;
        lastEntitySpotted = player.gameObject;
        Vector3 targetDir = player.transform.position - transform.position;
        targetDir.y = 0;
        float angle = Vector3.Angle(targetDir, transform.forward);
        var distance = Vector3.Distance(transform.position, player.transform.position);
        if (angle < 50f)
        {
            return distance <= player.detectVisualDistance;
        }
        return distance < player.detectHearingDistance;
    }

    public static Vector3 RandomNavSphere(Vector3 origin, float distance, int layermask)
    {
        Vector3 randomDirection = UnityEngine.Random.insideUnitSphere * distance;

        randomDirection += origin;

        NavMeshHit navHit;
        NavMesh.SamplePosition(randomDirection, out navHit, distance, layermask);

        return navHit.position;
    }
}
