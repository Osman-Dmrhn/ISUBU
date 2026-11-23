using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    public Camera PlayerCamera;
    public float range = 100;
    public int damage = 25;

    private GameObject hit;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            Shoot();
        }
    }

    void Shoot()
    {
        RaycastHit hit;

        Debug.DrawRay(PlayerCamera.transform.position, PlayerCamera.transform.forward * range,Color.red,5f);

        if(Physics.Raycast(PlayerCamera.transform.position,PlayerCamera.transform.forward,out hit,range))
        {
            Debug.Log(hit.collider.name);
            EnemyAI enemy= hit.collider.gameObject.GetComponent<EnemyAI>();
            if(enemy != null)
            {
                enemy.TakeDamage(damage);
            }
        }
    }
}
