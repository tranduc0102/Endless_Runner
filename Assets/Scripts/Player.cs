using System.Collections;
using UnityEngine;

public class Player : MonoBehaviour
{
    #region private value
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Animator anim;
    [SerializeField] private Collider2D playerCollider; 
    private GameManager GM;
    private bool isFirstJump = true;
    private float state; // biến này dùng để lưu speed. khi nhân vật va chạm chướng ngại vật map sẽ dừng 1 lúc sau đó nó sẽ chạy tiếp
    #endregion
   

    public float jumpForce = 10f;
    public bool checkJump = true;
    public bool doubleJump = true;
    public bool JumpAtck = false; // cái này là để xét animation bay xuống dưới
    public new AudioManager audio;

    private int playerLayer;
    private int obstacleLayer;
    private static readonly int Jump1 = Animator.StringToHash("Jump");
    private static readonly int DoubleJump = Animator.StringToHash("doubleJump");
    private static readonly int JumpAttack = Animator.StringToHash("jumpAttack");
    private static readonly int JumpAttack1 = Animator.StringToHash("jumpAttack1");

    private void Awake()
    {
        audio = GameObject.Find("GameManager").GetComponent<AudioManager>();
    }

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>(); // lấy rigidbody của Player
        anim = GetComponent<Animator>(); // lấy Animation cửa player
        playerCollider = GetComponent<Collider2D>(); // lấy collider của player
        GM = FindObjectOfType<GameManager>();
        GM.live = 2;
        playerLayer = gameObject.layer; // lấy layer của Player
        obstacleLayer = LayerMask.NameToLayer("obstaclesLayer"); // lấy layer cuả chướng ngại vật
        Physics2D.IgnoreLayerCollision(playerLayer, obstacleLayer, false); // layer của player và chướng ngại vật đc phép va chạm
    }
    
    private void Update()
    {
        if (GM.isPlay)
        {
            Jump(); // hành động nhảy
            ActionStomping();// hành động dậm chân
            CheckJumpAttack1Animation();//chạy Animation dậm sau khi chạm đất 1 khoảng thời gian rồi trở lại trạng thái run
            DieByDistance();// chết khi bị rơi
        }
    }
    private void Jump()
    {
        if (!Input.GetKey(KeyCode.Space) && checkJump)
        {
            doubleJump = false;
        }

        if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.W))
        {
            if (checkJump || doubleJump)
            {
                if (checkJump)
                {
                    anim.SetBool(Jump1, true);
                    audio.PlaySFX(audio.jump);
                    anim.SetBool(DoubleJump, false);
                }
                else if (doubleJump)
                {
                    anim.SetBool(Jump1, false);
                    audio.PlaySFX(audio.jump);
                    anim.SetBool(DoubleJump, true);
                }

                rb.velocity = new Vector2(rb.velocity.x, jumpForce);

                doubleJump = !doubleJump;
                isFirstJump = !isFirstJump;
                checkJump = false;
            }
        }

        if (checkJump)
        {
            anim.SetBool(Jump1, false);
            anim.SetBool(DoubleJump, false);
        }
    }
    private void ActionStomping()
    {
        if (Input.GetKeyDown(KeyCode.S) || Input.GetMouseButtonDown(1))
        {
            anim.SetBool(JumpAttack, true);
            rb.velocity = Vector2.down * 30f;
            JumpAtck = true;
            doubleJump = false;
        }

        if (checkJump && JumpAtck)
        {
            anim.SetBool(JumpAttack, false);
            anim.SetBool(JumpAttack1, true);
            JumpAtck = false;
        }
    }
    private void CheckJumpAttack1Animation()
    {
        AnimatorStateInfo stateInfo = anim.GetCurrentAnimatorStateInfo(0);

        if (stateInfo.IsName("Jump2") && stateInfo.normalizedTime >= 0.3f)
        {
            anim.SetBool(JumpAttack1, false);
        }
    }
    #region XuLiVaCham
    private void OnCollisionEnter2D(Collision2D other)
    {
        anim.SetBool(JumpAttack, false);
        if (other.gameObject.CompareTag("Null"))
        {
            checkJump = false;
        }
        else
        {
            checkJump = true;
        }
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Coin"))
        {
            ++GM.coin;
            audio.PlaySFX(audio.coin);
            other.gameObject.SetActive(false);
        }
        if (other.gameObject.CompareTag("Obstacles"))
        {
            state = GameManager.Instances.speed;
            if (GM.live > 1)
            {
                anim.Play("TakeDamage");
                rb.velocity = Vector2.up*7f;
                GM.speed = 0f;
                GM.isPlay = false;
                Invoke("ResumeRunAnimation",0.4f);
                other.enabled = false;
                StartCoroutine(HandleCollisionWithDelay(other, 10f));
            }
            else
            {
                anim.Play("TakeDamage");
                Invoke("StopAtLastFrame", 0.3f);
            }
            GM.live--;
        }
    }

    private IEnumerator HandleCollisionWithDelay(Collider2D other, float delay)
    {
        yield return new WaitForSeconds(delay);
        RestOtherCollider(other);
    }

    private void ResumeRunAnimation()
    {
        anim.Play("Run");
        GM.speed = state;
        GM.isPlay = true;
    }

    private void StopAtLastFrame()
    {
        rb.velocity = Vector2.up * 10f;
        playerCollider.enabled = false;
        GM.live = 0;
        anim.speed = 0;
        SaveGame.Save();
        SaveGame.Load();
    }

    private void RestOtherCollider(Collider2D other)
    {
        other.enabled = true;
    }

    private void DieByDistance()
    {
        if (rb.transform.position.y <= -10 && GM.live > 0)
        {
            if (GM.live > 1)
            {
                rb.transform.position = new Vector3(transform.position.x, 2f, 0f);
                rb.velocity = new Vector2(rb.velocity.x, 0f);
                StartCoroutine(DisableCollisionWithObstacles(0.9f));
            }
            GM.live--;
            if (GM.live == 0 )
            {
                SaveGame.Save();
                SaveGame.Load();
            }
        }

        if (GM.live == 0)
        {
            GM.isPlay = false;
        }
    }

    private IEnumerator DisableCollisionWithObstacles(float duration)
    {
        Physics2D.IgnoreLayerCollision(playerLayer, obstacleLayer, true);
        yield return new WaitForSeconds(duration);
        Physics2D.IgnoreLayerCollision(playerLayer, obstacleLayer, false);
    }
    

    #endregion
   
}
