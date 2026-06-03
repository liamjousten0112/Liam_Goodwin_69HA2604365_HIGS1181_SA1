using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public LayerMask wall;
    public int health = 3;

    private Rigidbody2D rb2D;
    private Transform player;

    private void Awake()
    {
        rb2D = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        player = GameObject.FindWithTag("Player").transform;
    }

    public void TakeTurn()
    {
        if (player == null)
        {
            Debug.LogWarning("Player not in scene");
            return;
        }

        Vector2 playerPos = player.position;
        Vector2 enemyPos = rb2D.position;

        Vector2 diff = playerPos - enemyPos;

        Vector2 moveDir = Vector2.zero;

        if (Mathf.Abs(diff.x) > Mathf.Abs(diff.y))
        {
            moveDir = diff.x > 0 ? Vector2.right : Vector2.left;
        }

        else
        {
            moveDir = diff.y > 0 ? Vector2.up : Vector2.down;
        }

        Vector2 targetpos = enemyPos + moveDir;

        if (Vector2.Distance(targetpos, playerPos) < 0.1f)
        {
            PlayerController playerController = player.GetComponent<PlayerController>();

            if (playerController != null)
            {
                playerController.TakeDamage(1);
            }
        }

        Debug.Log("Enemy Pos: " + enemyPos);
        Debug.Log("Target Pos: " + targetpos);

        Collider2D hit = Physics2D.OverlapCircle(targetpos, 0.1f, wall);

        Debug.Log("Hit: " + hit);

        if (!Physics2D.OverlapCircle(targetpos, 0.1f, wall))
        {
            rb2D.MovePosition(targetpos);
        }
        else
        {
            Vector2[] directions =
            {
                Vector2.up,
                Vector2.down,
                Vector2.left,
                Vector2.right,
            };

            foreach (Vector2 direction in directions)
            {
                Vector2 testPos = enemyPos + direction;

                if (targetpos == playerPos)
                {
                    PlayerController playerController = player.GetComponent<PlayerController>();

                    if (playerController != null)
                    {
                        playerController.TakeDamage(1);
                    }

                    Debug.Log("Enemy attacked player");
                    return;
                }

                if (!Physics2D.OverlapCircle(testPos, 0.1f, wall))
                {
                    rb2D.MovePosition(testPos);
                    break;
                }

            }
        }
        Debug.Log("Enemy moved toward player.");
    }

    public void TakeDamage(int damage)
    {
        health -= damage;

        Debug.Log("Enemy health: " + health);

        if (health <= 0)
        {
            Debug.Log("Enemy Defeated");
            Destroy(gameObject);
        }
    }
}
