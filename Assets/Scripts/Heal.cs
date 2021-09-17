using UnityEngine;

public class Heal : MonoBehaviour
{
    public int heal;

    private PlayerController controller;


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.CompareTag("Player")) {
            controller = FindObjectOfType<PlayerController>();

            if(controller.health+heal <= controller.fullHealth) {
                controller.health += heal;
                FindObjectOfType<HealthBar>().SetHealth(controller.health);
            }

            FindObjectOfType<AudioManager>().Play("Recover");

            Destroy(gameObject);
        }
    }
}
