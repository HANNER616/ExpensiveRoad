using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarcadorMovement : MonoBehaviour
{

    public float factor = 0.5f;
    private Vector3 startPosition;
    [SerializeField] float floatStrength = 1f; // Fuerza de la flotación (amplitud)
    [SerializeField] float floatFrequency = 2f; // Frecuencia del movimiento (ciclo)
    [SerializeField] float verticalOffset = 0f; // Offset inicial en el eje Y

    void Start()
    {
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        startPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        float newY = startPosition.y + Mathf.Sin(Time.time * floatFrequency) * floatStrength + verticalOffset;

        //hacer que el objeto flote en el eje y
        transform.position = new Vector3(transform.position.x, newY, transform.position.z);

    }
}
