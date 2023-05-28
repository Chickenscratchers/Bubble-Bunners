using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Flash behavior when a character (e.g. main char, enemies) take damage.
// Need to reference this script's functions in the characters' scripts in order to leverage this behavior.
public class FlashBehavior : MonoBehaviour
{
    // Material to switch to during the flash
    [SerializeField] private Material flashMaterial;

    // Duration of a single flash
    [SerializeField] private float singleFlashDuration;

    // Duration of the entire flash animation
    [SerializeField] private float flashDuration;

    // Sprite renderer that should flash
    private SpriteRenderer spriteRenderer;

    // Original material that was in use before this script started
    private Material originalMaterial;

    // Currently running coroutine
    private Coroutine flashRoutine;

    //private float timeLeft;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        originalMaterial = spriteRenderer.material;
        //timeLeft = flashDuration;
    }

    public void Flash()
    {
        // If the flashRoutine is not null, then it is currently running and it should be stopped first to avoid multiple coroutines causing bugs
        if (flashRoutine != null)
        {
            StopCoroutine(flashRoutine);
        }

        // Start coroutine and store ref
        flashRoutine = StartCoroutine(FlashRoutine());
    }

    private IEnumerator FlashRoutine()
    {
        float startTime = Time.time;
        float endTime = startTime + flashDuration;

        while (Time.time < endTime)
        {
            // Swap to the flash material
            spriteRenderer.material = flashMaterial;

            // Pause the execution of this function 
            yield return new WaitForSeconds(singleFlashDuration);

            // After the pause, swap back to the original material
            spriteRenderer.material = originalMaterial;

            // Pause the execution of this function 
            yield return new WaitForSeconds(singleFlashDuration);
        }

        // Set the routine to null, signaling that it's finished
        flashRoutine = null;
    }
}
