using UnityEngine;

public class BounceStair : MonoBehaviour
{
    public float bounceForce;

    public void BounceStairCollision(Rigidbody2D playerRb)
    {
        playerRb.AddForce(Vector2.up * bounceForce);

        FindObjectOfType<AudioManager>().Play("Bounce");
    }
}
