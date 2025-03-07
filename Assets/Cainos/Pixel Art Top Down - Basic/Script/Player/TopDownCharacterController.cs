using UnityEngine;
using UnityEngine.UI;
public class TopDownCharacterController : MonoBehaviour
{
    public float speed;
    private Animator animator;
    private Rigidbody2D body;
    private Vector2 dir;

    [SerializeField] private Weapon weapon;

    [SerializeField] private Image dashIcon;
    [SerializeField] private Image dashDisplay;
    [SerializeField] private Sprite dashSprite;
    [SerializeField] private float dashCooldown;
    [SerializeField] private float dashSpeed;
    [SerializeField] private float dashDuration;
    private float dashTimer = Mathf.Infinity;
    private float dashTime = 0;
    private Vector2 dashDir;

    [SerializeField] private GameObject dashWind;
    private Animator dashAnim;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }
    private void Awake()
    {
        body = GetComponent<Rigidbody2D>();
        dashIcon.sprite = dashSprite;
        dashDisplay.sprite = dashSprite;
        dashDisplay.fillAmount = 1 - Mathf.Min(dashTimer / dashCooldown, 1);

        dashAnim = dashWind.GetComponent<Animator>();
        dashWind.SetActive(false);
    }

    private void Update()
    {
        dir = Vector2.zero;
        if (Input.GetKey(KeyCode.A))
        {
            dir.x = -1;
        }
        else if (Input.GetKey(KeyCode.D))
        {
            dir.x = 1;
        }

        if (Input.GetKey(KeyCode.W))
        {
            dir.y = 1;
        }
        else if (Input.GetKey(KeyCode.S))
        {
            dir.y = -1;
        }

        dir.Normalize();
        animator.SetBool("running", dir.magnitude > 0);
        if (dir.magnitude > 0)
        {
            weapon.casting = false;
            animator.Play("run");
        }
        if (dir.x > 0)
        {
            transform.localScale = Vector3.one;
        }
        else if (dir.x < 0)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }

        body.linearVelocity = speed * dir;

        if (Input.GetKey(KeyCode.LeftShift) && dashTimer >= dashCooldown && dir != Vector2.zero)
        {
            Dash();
        }
        else
        {
            dashTimer += Time.deltaTime;
        }

        dashDisplay.fillAmount = 1 - Mathf.Min(dashTimer / dashCooldown, 1);

        if (dashDir != Vector2.zero)
        {
            if (dashTime > dashDuration)
            {
                dashDir = Vector2.zero;
                dashWind.SetActive(false);
                dashTime = 0;
                Physics2D.IgnoreLayerCollision(23, 8, false);
                Physics2D.IgnoreLayerCollision(23, 9, false);
            }
            else
            {
                transform.position = new Vector3(transform.position.x + dashDir.x * dashSpeed * Time.deltaTime,
                                                transform.position.y + dashDir.y * dashSpeed * Time.deltaTime,
                                                transform.position.z);
                dashTime += Time.deltaTime;
            }
            
        }
    }
    private void Dash()
    {
        dashTimer = 0;
        dashDir = dir;
        dashWind.SetActive(true);
        dashAnim.SetTrigger("active");
        Physics2D.IgnoreLayerCollision(23, 8, true);
        Physics2D.IgnoreLayerCollision(23, 9, true);
    }
}
