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

    // Audio
    public AudioSource carAudioSource;
    public AudioSource reachTargetSound;
    public AudioSource claxon;
    public float minPitch = 0.8f; 
    public float maxPitch = 2.0f; 
    public float pitchIncreaseSpeed = 0.1f;

    // Use this for initialization
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        // Asegúrate de que el AudioSource esté configurado correctamente
        if (carAudioSource == null)
        {
            Debug.LogError("No se ha asignado un AudioSource al CarController.");
        }
        else
        {
            carAudioSource.loop = true; 
            carAudioSource.pitch = minPitch; 
            carAudioSource.Play();    }
        }

    // Update is called once per frame
    void FixedUpdate()
    {
        //si se presiona la e, se activa el claxon
        if (Input.GetKeyDown(KeyCode.E))
        {
            claxon.Play();
        }
        

        steeringAmount = -Input.GetAxis("Horizontal");
        speed = Input.GetAxis("Vertical") * accelerationPower;
        direction = Mathf.Sign(UnityEngine.Vector2.Dot(rb.velocity, rb.GetRelativeVector(UnityEngine.Vector2.right)));
        rb.rotation += steeringAmount * steeringPower * rb.velocity.magnitude * direction;

        rb.AddRelativeForce(UnityEngine.Vector2.right * speed);

        rb.AddRelativeForce(-UnityEngine.Vector2.right * rb.velocity.magnitude * steeringAmount / 2);

        //pitch del audio
        if (Mathf.Abs(speed) > 0.1f || Mathf.Abs(steeringAmount) > 0.1f)
        {
            // Aumentar el pitch gradualmente
            if (carAudioSource.pitch < maxPitch)
            {
                carAudioSource.pitch += pitchIncreaseSpeed * Time.deltaTime;
            }
        }
        else
        {
            // Reducir el pitch gradualmente hasta el mínimo
            if (carAudioSource.pitch > minPitch)
            {
                carAudioSource.pitch -= pitchIncreaseSpeed * Time.deltaTime;
            }
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        // Verificar si colisiona con un marcador
        if (collision.gameObject.CompareTag("MarcadorInicio"))
        {
            reachTargetSound.Play();
            gameLogic.Instance.OnMarkerReachStart(collision.gameObject);
        }

        if (collision.gameObject.CompareTag("MarcadorDestino"))
        {
            reachTargetSound.Play();
            // Llamar a la lógica principal en GameLogic
            gameLogic.Instance.OnMarkerReachTarget(collision.gameObject);
        }
    }
}