using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    [Header("Görüþ ve NavMesh Ayarlarý")]
    public Transform playerTransform;    
    private NavMeshAgent agent;

    [Header("Görüþ Açýsý (FOV) Parametreleri")]
    public float visionRange = 10f;      
    public float fieldOfView = 90f;      
    public float rotationSpeed = 5f;

    [Header("Can Ayarlarý")]
    public int maxHealth = 50;
    private int currentHealth;

    void Start()
    {
        currentHealth = maxHealth;
        agent = GetComponent<NavMeshAgent>();
        playerTransform=GameObject.Find("Player").transform;
    }

    void Update()
    {
        if (playerTransform != null && agent != null)
        {
            if (CanSeePlayer())
            {
                agent.SetDestination(playerTransform.position);
                RotateTowardsPlayer();
            }
            else
            {
                agent.SetDestination(transform.position);
            }
        }
    }


    private void OnCollisionEnter(Collision collision)
    {

        Debug.Log(collision.gameObject.name);
        NewMonoBehaviourScript player = collision.gameObject.GetComponent<NewMonoBehaviourScript>();
        if (player != null)
        {
            player.TakeDamage(10);
            Debug.Log(collision.gameObject.name + "10" + "hasar verildi");
        }
        else
        {
            Debug.Log(collision.gameObject.name);
        }
    }


    bool CanSeePlayer()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, playerTransform.position);
        if (distanceToPlayer > visionRange)
        {
            return false; 
        }

        Vector3 directionToPlayer = (playerTransform.position - transform.position).normalized;
        float angleBetweenPlayer = Vector3.Angle(transform.forward, directionToPlayer);

        if (angleBetweenPlayer < fieldOfView / 2f)
        {
            return true; 
        }

        return false;
    }

    void RotateTowardsPlayer()
    {
        Vector3 direction = (playerTransform.position - transform.position).normalized;
        direction.y = 0;

        if (direction != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }
    }

    public void TakeDamage(int damageAmount)
    {
        currentHealth -= damageAmount;
        Debug.Log(gameObject.name + " hasar aldý! Kalan Can: " + currentHealth);

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Debug.Log(gameObject.name + " öldü!");
        Destroy(gameObject);
    }



    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, visionRange);

        Vector3 leftBoundary = Quaternion.Euler(0, -fieldOfView / 2f, 0) * transform.forward * visionRange;
        Vector3 rightBoundary = Quaternion.Euler(0, fieldOfView / 2f, 0) * transform.forward * visionRange;

        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, transform.position + leftBoundary);
        Gizmos.DrawLine(transform.position, transform.position + rightBoundary);
    }
}