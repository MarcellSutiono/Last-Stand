using System.Collections;
using UnityEngine;

public class SimpleFlash : MonoBehaviour
{
    [SerializeField] private float duration = 0.1f;
    [SerializeField] private float flashIntensity = 2f; // Brightness multiplier

    private SpriteRenderer spriteRenderer;
    private Material material;
    private Coroutine flashRoutine;

    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        material = spriteRenderer.material;
    }

    public void Flash()
    {
        if (spriteRenderer == null) return;
        
        if (flashRoutine != null)
        {
            StopCoroutine(flashRoutine);
        }

        flashRoutine = StartCoroutine(FlashRoutine());
    }

    private IEnumerator FlashRoutine()
    {
        // Make sprite brighter (over-expose to white)
        material.color = Color.white * flashIntensity;

        yield return new WaitForSeconds(duration);

        // Return to normal
        material.color = Color.white;

        flashRoutine = null;
    }
}