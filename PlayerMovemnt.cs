using Unity.Hierarchy;
using UnityEngine;

public class NewMonoBehaviourScript : MonoBehaviour
{
    public float moveSpeed=6f;
    public float runSpeed = 15f;
    public float gravity = -9.81f;
    public float jumpHeight = 2f;

    private CharacterController characterController;
    private Vector3 velocity;
    private bool isGrounded;
    private float speed;

    public float stamina = 100f;

    public int health = 100;

    void Start()
    {
        characterController= GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (health > 0)
        {

        
        isGrounded = characterController.isGrounded;

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");

        if (Input.GetKey(KeyCode.LeftShift) && stamina>=1)
        {
            speed = runSpeed;
            stamina -= 1f*Time.deltaTime;
        }
        else
        {
            speed = moveSpeed;
        }

        Vector3 move = transform.right * moveX + transform.forward * moveZ;
        characterController.Move(move*speed*Time.deltaTime);

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded && stamina>=20)
        {
            Debug.Log("jump");
            velocity.y = Mathf.Sqrt(-2 * gravity * jumpHeight);
            stamina -= 20f;
        }

        velocity.y += gravity*Time.deltaTime;
        characterController.Move(velocity*Time.deltaTime);


        if(stamina<100f)
         stamina += 0.5f*Time.deltaTime;
        }

    }

    public void TakeDamage(int damage)
    {
        if(health>0)
            health-=damage;
        if (health < 0)
        {
            gameObject.SetActive(false);
        }
    }
}

