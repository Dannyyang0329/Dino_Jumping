using UnityEngine;

public class FadedStair : MonoBehaviour
{
    public float fadedTime;

    private SpriteRenderer stairRenderer;
    private float time = 0;

    private void Start()
    {
        stairRenderer = GetComponent<SpriteRenderer>();        
    }


    public void FadedStairCollision()
    {
        Invoke("DecreaseTranspant", Time.deltaTime);
    }


    private void DecreaseTranspant()
    {
        if(time < fadedTime) {
            float transpant = 1-(time/fadedTime);

            Color color = new Color(stairRenderer.color.r, stairRenderer.color.g, stairRenderer.color.b, transpant);
            stairRenderer.color = color;

            time += Time.deltaTime;
            Invoke("DecreaseTranspant", Time.deltaTime);
        }
        else{
            Destroy(gameObject);
        }
    }
}
