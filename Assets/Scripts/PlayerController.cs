using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float walkSpeed = 2f;
    public float runSpeed = 4f;
    public float rotationSpeed = 10f;

    private CharacterController controller;
    private Animator animator;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        Vector3 move = new Vector3(h, 0f, v);
        bool isRunning = Input.GetKey(KeyCode.LeftShift);
        float currentSpeed = isRunning ? runSpeed : walkSpeed;

        if (move.magnitude > 0.01f)
        {
            Vector3 moveDir = move.normalized;

            controller.Move(moveDir * currentSpeed * Time.deltaTime);

            Quaternion targetRotation = Quaternion.LookRotation(moveDir);
            transform.rotation = Quaternion.Slerp(
                transform.rotation,
                targetRotation,
                rotationSpeed * Time.deltaTime
            );
        }

        float animSpeed = move.magnitude * (isRunning ? 1f : 0.5f);
        animator.SetFloat("Speed", animSpeed, 0.1f, Time.deltaTime);
    }
}