using UnityEngine;

public class OutOfBound : MonoBehaviour
{
    public float topBound;

    private void Update()
    {
        if(transform.position.y > topBound) {
            Destroy(gameObject);
        }        
    }
}
