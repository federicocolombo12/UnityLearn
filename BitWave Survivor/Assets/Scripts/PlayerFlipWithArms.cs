using UnityEngine;

public class PlayerFlipWithArms : MonoBehaviour
{
    private SpriteRenderer playerSpriteRenderer;
    public Transform armTransform;  // Riferimento al Transform del braccio
    public Transform gunTransform;  // Riferimento al Transform dell'arma

    void Start()
    {
        playerSpriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        // Verifica se il player è girato
        if (playerSpriteRenderer.flipX)
        {
            // Se è girato, sposta il braccio e l'arma sulla sinistra e flippa localmente
            armTransform.localPosition = new Vector3(-Mathf.Abs(armTransform.localPosition.x), armTransform.localPosition.y, armTransform.localPosition.z);
            gunTransform.localPosition = new Vector3(-Mathf.Abs(gunTransform.localPosition.x), gunTransform.localPosition.y, gunTransform.localPosition.z);

            armTransform.localScale = new Vector3(-Mathf.Abs(armTransform.localScale.x), armTransform.localScale.y, armTransform.localScale.z);
            gunTransform.localScale = new Vector3(-Mathf.Abs(gunTransform.localScale.x), gunTransform.localScale.y, gunTransform.localScale.z);
        }
        else
        {
            // Se non è girato, sposta il braccio e l'arma sulla destra e flippa localmente
            armTransform.localPosition = new Vector3(Mathf.Abs(armTransform.localPosition.x), armTransform.localPosition.y, armTransform.localPosition.z);
            gunTransform.localPosition = new Vector3(Mathf.Abs(gunTransform.localPosition.x), gunTransform.localPosition.y, gunTransform.localPosition.z);

            armTransform.localScale = new Vector3(Mathf.Abs(armTransform.localScale.x), armTransform.localScale.y, armTransform.localScale.z);
            gunTransform.localScale = new Vector3(Mathf.Abs(gunTransform.localScale.x), gunTransform.localScale.y, gunTransform.localScale.z);
        }
    }
}
