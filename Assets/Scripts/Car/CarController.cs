using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;

public class CarController : MonoBehaviour
{
    Rigidbody2D rb;

    [SerializeField]
    float accelerationPower = 5f;
    [SerializeField]
    float steeringPower = 5f;
    float steeringAmount, speed, direction;

    // Use this for initialization
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        steeringAmount = -Input.GetAxis("Horizontal");
        speed = Input.GetAxis("Vertical") * accelerationPower;
        direction = Mathf.Sign(UnityEngine.Vector2.Dot(rb.velocity, rb.GetRelativeVector(UnityEngine.Vector2.right)));
        rb.rotation += steeringAmount * steeringPower * rb.velocity.magnitude * direction;

        rb.AddRelativeForce(UnityEngine.Vector2.right * speed);

        rb.AddRelativeForce(-UnityEngine.Vector2.right * rb.velocity.magnitude * steeringAmount / 2);

    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        // Verificar si colisiona con un marcador
        if (collision.gameObject.CompareTag("MarcadorInicio"))
        {
            gameLogic.Instance.OnMarkerReachStart(collision.gameObject);
        }

        if (collision.gameObject.CompareTag("MarcadorDestino"))
        {
            // Llamar a la lógica principal en GameLogic
            gameLogic.Instance.OnMarkerReachTarget(collision.gameObject);
        }
    }

}
