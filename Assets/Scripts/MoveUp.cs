using UnityEngine;

public class MoveUp : MonoBehaviour
{
    private float speed;

    private void Start() 
    {
        speed = FindObjectOfType<SpawnManager>().GetSpeed();

        if(speed == 0) speed = 250f;
    }


    private void FixedUpdate()
    {
        transform.Translate(Vector3.up * speed * Time.fixedDeltaTime);
    }
}
