using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecialSaw : MonoBehaviour
{
    [SerializeField] private float movementDistance = 5f;
    [SerializeField] private float minSpeed = 1f;
    [SerializeField] private float maxSpeed = 3f;
    [SerializeField] private float verticalAmplitude = 1f;  // Máximo movimiento vertical
    [SerializeField] private float damage = 10f;
    [SerializeField] private float directionChangeInterval = 2f; // Tiempo para cambiar dirección

    private float movementSpeed;
    private bool movingLeft;
    private float leftEdge;
    private float rightEdge;
    private float baseY;
    private float directionChangeTimer;
    private float minY, maxY; // Límites de movimiento vertical

    private void Awake()
    {
        leftEdge = transform.position.x - movementDistance;
        rightEdge = transform.position.x + movementDistance;
        baseY = transform.position.y;

        // Define límites verticales
        minY = baseY - verticalAmplitude;
        maxY = baseY + verticalAmplitude;

        movementSpeed = Random.Range(minSpeed, maxSpeed);
        movingLeft = Random.value > 0.5f; // Decide aleatoriamente si empieza a la izquierda o derecha
        directionChangeTimer = directionChangeInterval;
    }

    private void Update()
    {
        // Movimiento horizontal
        if (movingLeft)
        {
            if (transform.position.x > leftEdge)
            {
                transform.position += Vector3.left * movementSpeed * Time.deltaTime;
            }
            else
            {
                movingLeft = false;
            }
        }
        else
        {
            if (transform.position.x < rightEdge)
            {
                transform.position += Vector3.right * movementSpeed * Time.deltaTime;
            }
            else
            {
                movingLeft = true;
            }
        }

        // Movimiento vertical con límite
        float newY = transform.position.y + Mathf.Sin(Time.time * movementSpeed) * Time.deltaTime;
        newY = Mathf.Clamp(newY, minY, maxY); // Limita la posición Y
        transform.position = new Vector3(transform.position.x, newY, transform.position.z);

        // Cambio aleatorio de dirección y velocidad
        directionChangeTimer -= Time.deltaTime;
        if (directionChangeTimer <= 0)
        {
            ChangeDirection();
            movementSpeed = Random.Range(minSpeed, maxSpeed);
            directionChangeTimer = Random.Range(1f, directionChangeInterval);
        }
    }

    private void ChangeDirection()
    {
        if (Random.value > 0.5f) movingLeft = !movingLeft; // 50% de probabilidad de invertir dirección horizontal
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.GetComponent<Health>()?.TakeDamage(damage);
        }
    }
}
