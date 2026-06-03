using TMPro;
using UnityEngine;

public class UIMAnager : MonoBehaviour
{
    public PlayerController player;
    public EnemyAI enemy;

    public TextMeshProUGUI playerHealthText;
    public TextMeshProUGUI enemyHealthText;
    public TextMeshProUGUI turnText;

    public GameObject winScreen;
    public GameObject loseScreen;

    private void Start()
    {
        winScreen.SetActive(false);
        loseScreen.SetActive(false);
    }

    private void Update()
    {
        playerHealthText.text = "Player Health: " + player.health;

        if (enemy != null)
        {
            enemyHealthText.text = "Enemy Health: " + enemy.health;
        }
        else
        {
            enemyHealthText.text = "Enemy Defeated";
            winScreen.SetActive(true);
        }

        if (player.health <= 0)
        {
            loseScreen.SetActive(true);
        }
        else if (enemy == null)
        {
            winScreen.SetActive(true);
        }
        else
        {
            if(player.GameManager.playerTurn)
            {
                turnText.text = "Player Turn";
            }
            else
            {
                turnText.text = "Enemy Turn";
            }
        }
    }
}
