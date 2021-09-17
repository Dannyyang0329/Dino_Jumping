using UnityEngine;

public class SpawnHeart : MonoBehaviour
{
    [Header("Item")]
    public GameObject heart;

    [Header("Info")]
    public float probability;
    public float leftBound;
    public float rightBound;

    private void Start()
    {
        if(Random.Range(0f, 1f) <= probability) {
            Vector3 offset = new Vector3(Random.Range(leftBound, rightBound), 30, 0);
            Instantiate(heart, transform.position+offset, heart.transform.rotation);
        }
    }
}
