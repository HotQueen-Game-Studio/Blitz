using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

[RequireComponent(typeof(Rigidbody2D))]
public class InputMovimentation : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private Animator playerAnimator;
    [SerializeField] private AudioClip[] stepClips;
    private float currentStepTempo;
    [SerializeField] Transform sight;
    BlitzInputs blitzInputs;
    Rigidbody2D rb;
    //MOVEMENT
    [Range(0, 20)] public float speed = 5.0f;
    // [Range(0,20)]public float rotateSpeed = 10.0f;

    private void Awake()
    {
        blitzInputs = new BlitzInputs();
        rb = this.gameObject.GetComponent<Rigidbody2D>();
    }

    void OnEnable()
    {
        blitzInputs.Enable();
    }

    void OnDisable()
    {
        blitzInputs.Disable();
    }

    private void FixedUpdate()
    {
        Move(blitzInputs.Player.Move.ReadValue<Vector2>(), speed);
        // Rotate(blitzInputs.Player.Look.ReadValue<Vector2>());
    }
    void Update()
    {
        if (rb.velocity.magnitude > 0.1f)
        {
            currentStepTempo -= Time.deltaTime;
            if (currentStepTempo <= 0)
            {
                audioSource.clip = stepClips[Random.Range(0, stepClips.Length)];
                audioSource.Play();
                currentStepTempo = speed / 8;
            }
        }

        playerAnimator.SetFloat("MoveX", rb.velocity.x);
        playerAnimator.SetFloat("MoveY", rb.velocity.y);
    }

    // private void Rotate(Vector2 deltaMouse)
    // {

    //     Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    //     Vector2 targetDir = mousePos - (Vector2)this.transform.position;

    //     float angle = Mathf.Atan2(targetDir.y, targetDir.x) * Mathf.Rad2Deg;

    //     sight.eulerAngles = new Vector3(0, 0, angle - 90);

    // }

    private void Move(Vector2 direction, float speed)
    {
        rb.velocity = direction * speed; //((sight.up * direction.y) + (sight.right * direction.x)) * speed;
    }
}
