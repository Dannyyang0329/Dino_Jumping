using UnityEngine;

public class PushLeftStair : MonoBehaviour
{
    public float pushForce;

    private Rigidbody2D rb;
    private bool isPush = false;

    private void OnCollisionEnter2D(Collision2D collision) 
    {
        if(collision.collider.CompareTag("Player")) {
            isPush = true;
            rb = collision.collider.GetComponent<Rigidbody2D>();
        }        
    }
    

    private void OnCollisionExit2D(Collision2D collision)
    { 
        if(collision.collider.CompareTag("Player")) {
            isPush = false;
        }
    }


    private void FixedUpdate()
    {
        if(isPush) {
            rb.AddForce(Vector2.left * pushForce * Time.fixedDeltaTime);
        }
    }
}
