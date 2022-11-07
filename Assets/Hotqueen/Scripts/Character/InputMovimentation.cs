using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

[RequireComponent(typeof(Rigidbody2D))]
public class InputMovimentation : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip[] stepClips;
    [SerializeField] private bool invertX;
    [SerializeField] private bool invertY;
    private float currentStepTempo;
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
        Vector2 direction = new Vector2();
        Vector2 input = blitzInputs.Player.Move.ReadValue<Vector2>();

        direction.y = invertY ? -input.y : input.y;
        direction.x = invertX ? -input.x : input.x;
        
        Move(direction, speed);
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
    }


    private void Move(Vector2 direction, float speed)
    {
        rb.velocity = direction * speed;
    }
}
