using UnityEngine;
using System.Collections.Generic;

public class gameLogic : MonoBehaviour
{
    // Singleton instance
    public static gameLogic Instance { get; private set; }

    [SerializeField] private Transform locationPoints;
    [SerializeField] private GameObject markerPrefabStart;
    [SerializeField] private GameObject markerPrefabTarget;
    [SerializeField] private Transform homePosition; // Nueva referencia para la posición de inicio

    private List<Transform> destinationsList = new List<Transform>();
    private int currentDestinationIndex = 0;

    void Awake()
    {
        // Singleton pattern
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        Instantiate(markerPrefabStart, homePosition.position, Quaternion.identity);

        // Populate destinations list, skipping the parent transform
        foreach (Transform child in locationPoints.GetComponentsInChildren<Transform>())
        {
            if (child != locationPoints)
            {
                destinationsList.Add(child);
            }
        }

    }

    public void OnMarkerReachStart(GameObject marker)
    {
        currentDestinationIndex++;
        // Destroy the current marker
        Destroy(marker);
        Debug.Log("Llego al inicio");

        SpawnNextMarker();
    }

    public void OnMarkerReachTarget(GameObject marker)
    {
        Destroy(marker);

        Instantiate(markerPrefabStart, homePosition.position, Quaternion.identity);




    }

    private void SpawnNextMarker()
    {
        if (currentDestinationIndex <= (destinationsList.Count))
        {
            // Spawn marker at next location
            Transform targetLocation;

                // Going to destination
                targetLocation = destinationsList[currentDestinationIndex-1];
        
            Instantiate(markerPrefabTarget, targetLocation.position, targetLocation.rotation);
        }
        else
        {
            Debug.Log("Todas las ubicaciones han sido visitadas");
        }
    }
}