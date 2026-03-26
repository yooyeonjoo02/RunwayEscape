using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;

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

        Vector3 move = new Vector3(h, 0, v);

        // Shift 누르면 달리기
        bool isRunning = Input.GetKey(KeyCode.LeftShift);

        float currentSpeed = isRunning ? 2f : 0.8f;

        controller.Move(move * currentSpeed * Time.deltaTime);

        // 🔥 애니메이션용 Speed
        float animSpeed = move.magnitude * (isRunning ? 1f : 0.5f);

        animator.SetFloat("Speed", animSpeed);
    }
}