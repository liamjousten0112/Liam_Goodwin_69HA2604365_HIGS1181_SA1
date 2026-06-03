using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour
{
    public PlayerController Player;
    public EnemyAI Enemy;

    public bool playerTurn = true;

    public void EndPlayerTurn()
    {
        StartCoroutine(EnemyTurnRoutine());
    }

    private IEnumerator EnemyTurnRoutine()
    {

        playerTurn = false;

        Debug.Log("Player turn ended");

        if (Enemy == null)
        {
            yield break;
        }

        yield return new WaitForSeconds(0.5f);

        Enemy.TakeTurn();

        Debug.Log("Enemy turn started");

        yield return new WaitForSeconds(0.25f);

        playerTurn = true;
    }
}
