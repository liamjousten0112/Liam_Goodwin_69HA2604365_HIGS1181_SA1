using System.Runtime.CompilerServices;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public GameManager GameManager;
    public LayerMask wall;
    public int health = 3;
    private Rigidbody2D rb2D;
    private Vector2 facing = Vector2.down;

    private void Awake()
    {
        rb2D = GetComponent<Rigidbody2D>();
    }

    public void StartTurn()
    {

    }

    private void Update()
    {
        if (!GameManager.playerTurn)
            return;

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Shoot();
            GameManager.EndPlayerTurn();
            return;
        }

        Vector2 move = Vector2.zero;

        if (Input.GetKeyDown(KeyCode.W))
        {
            move = Vector2.up;
            facing = Vector2.up;
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            move = Vector2.down;
            facing = Vector2.down;
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            move = Vector2.right;
            facing = Vector2.right;
        }
        else if (Input.GetKeyDown(KeyCode.A))
        {
            move = Vector2.left;
            facing = Vector2.left;
        }

        if (move == Vector2.zero)
            return;

        Vector2 newPos = rb2D.position + move;

        if (!Physics2D.OverlapCircle(newPos, 0.1f, wall))
        {
            rb2D.MovePosition(newPos);
            GameManager.EndPlayerTurn();
        }
    }

    private void Shoot()
    {
        Vector2 attackPos = rb2D.position + facing;

        Collider2D hit = Physics2D.OverlapCircle(attackPos, 2f);
        Debug.Log("Hit: " + hit);

        if (hit != null)
        {
            EnemyAI enemy = hit.GetComponent<EnemyAI>();

            if (enemy != null)
            {
                enemy.TakeDamage(1);
                Debug.Log("Enemy hit");
            }
        }
    }

    public void TakeDamage(int damage)
    {
        health -= damage;

        Debug.Log("Player Health:" + health);

        if (health <= 0)
        {
            Debug.Log("Game Over");
        }
    }
}
