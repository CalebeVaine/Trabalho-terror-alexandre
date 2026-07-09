using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    public enum State { Patrol, Chase, Investigate, Search }
    public State state;

    [Header("References")]
    public Transform player;
    public Transform[] patrolPoints;
    public AudioSource footsteps;
    public AudioSource detectionSound; // Som ao detectar o jogador
    public JumpscareController jumpscare;
    public NavMeshAgent agent;

    [Header("Vision")]
    public float viewDistance = 12f;
    public float viewAngle = 100f;
    public LayerMask visionMask, obstacleMask;

    [Header("Hearing")]
    public float hearingRange = 10f;

    [Header("Movement")]
    public float patrolSpeed = 2f;
    public float chaseSpeed = 4f;
    public float stopDistance = 2.2f;

    [Header("Audio")]
    public float maxStepDistance = 20f;
    public float minVolume = 0.05f;
    public float maxVolume = 0.8f;

    [Header("Timers")]
    public float waitTime = 2f;
    public float searchDuration = 7f;
    public float startDelay;

    int patrolIndex;
    float waitTimer, searchTimer;
    bool active, heardNoise, jumpscareTriggered;

    bool playerDetected; // Evita repetir o som enquanto estiver vendo o jogador

    Vector3 heardPos, lastSeenPos;

    void Start()
    {
        if (!agent) agent = GetComponent<NavMeshAgent>();

        if (footsteps)
        {
            footsteps.loop = true;
            footsteps.volume = minVolume;
            footsteps.Play();
        }

        if (detectionSound)
        {
            detectionSound.playOnAwake = false;
            detectionSound.loop = false;
        }

        Invoke(nameof(ActivateAI), startDelay);
    }

    void ActivateAI() => active = true;

    void Update()
{
    Debug.Log(state);

    if (!active || jumpscareTriggered) return;

    UpdateFootsteps();
    DetectPlayer();

    switch (state)
    {
        case State.Patrol: Patrol(); break;
        case State.Chase: Chase(); break;
        case State.Investigate: Investigate(); break;
        case State.Search: Search(); break;
    }
}

    void UpdateFootsteps()
    {
        if (!footsteps || !player) return;

        float dist = Vector3.Distance(transform.position, player.position);
        footsteps.volume = Mathf.Lerp(maxVolume, minVolume, dist / maxStepDistance);
    }

    void DetectPlayer()
    {
        Vector3 eye = transform.position + Vector3.up * 1.6f;
        Vector3 dir = (player.position - eye).normalized;
        float dist = Vector3.Distance(transform.position, player.position);

        if (dist > viewDistance)
        {
            playerDetected = false;
            return;
        }

        if (Vector3.Angle(transform.forward, dir) > viewAngle * 0.5f)
        {
            playerDetected = false;
            return;
        }

        if (Physics.Raycast(eye, dir, out RaycastHit hit, viewDistance, visionMask) &&
    hit.collider.CompareTag("Player") &&
    !Physics.Raycast(eye, dir, dist, obstacleMask))
{
    Debug.Log("Vi o player!");
    Debug.Log("Raycast acertou: " + hit.collider.name);

    lastSeenPos = player.position;
    state = State.Chase;

            playerDetected = true;
        }
        else
        {
            playerDetected = false;
        }
    }

    public void HearNoise(Vector3 pos)
    {
        if (Vector3.Distance(transform.position, pos) > hearingRange) return;

        heardNoise = true;
        heardPos = pos;
        state = State.Investigate;
    }

    void Patrol()
    {
        agent.speed = patrolSpeed;

        if (patrolPoints.Length == 0) return;

        if (heardNoise)
        {
            state = State.Investigate;
            return;
        }

        if (agent.remainingDistance > 0.3f) return;

        waitTimer += Time.deltaTime;

        if (waitTimer >= waitTime)
        {
            waitTimer = 0;
            patrolIndex = Random.Range(0, patrolPoints.Length);
            agent.SetDestination(patrolPoints[patrolIndex].position);
        }
    }

    void Chase()
{
    agent.speed = chaseSpeed;

    float dist = Vector3.Distance(transform.position, player.position);

    Debug.Log("Distância até o player: " + dist);

    if (dist <= stopDistance)
    {
        Debug.Log("ENCOSTOU NO PLAYER");

        jumpscareTriggered = true;
        agent.enabled = false;
        jumpscare.TriggerJumpscare();
        return;
    }

    agent.SetDestination(player.position);
    lastSeenPos = player.position;

    if (dist > viewDistance * 1.3f)
        state = State.Investigate;
}

    void Investigate()
    {
        agent.speed = patrolSpeed;
        agent.SetDestination(heardNoise ? heardPos : lastSeenPos);

        if (agent.remainingDistance > 0.4f) return;

        heardNoise = false;
        searchTimer = 0;
        state = State.Search;
    }

    void Search()
    {
        searchTimer += Time.deltaTime;

        if ((searchTimer += Time.deltaTime) >= searchDuration)
        {
            state = State.Patrol;

            if (patrolPoints.Length > 0)
                agent.SetDestination(patrolPoints[patrolIndex].position);
        }
    }
}