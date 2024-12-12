using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class gameLogic : MonoBehaviour
{
    // Singleton instance
    public static gameLogic Instance { get; private set; }

    [SerializeField] private Transform locationPoints;
    [SerializeField] private GameObject markerPrefabStart;
    [SerializeField] private GameObject markerPrefabTarget;
    [SerializeField] private Transform homePosition; // Nueva referencia para la posición de inicio
    [SerializeField] private Transform dialogPosition;
    public GameObject dialog1;
    public GameObject dialog2;
    public GameObject dialog3;
    public GameObject dialog4;
    public GameObject dialog5;
    public GameObject dialogTy;
    public GameObject dialogTy2;
    public GameObject dialogEnd;
    public GameObject finalPanel;

    private List<Transform> destinationsList = new List<Transform>();
    private int currentDestinationIndex = 0;
    private GameObject currentDialog; 

    private Vector3 homePositionGlobal;
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

    private List<Vector3> childPositions = new List<Vector3>();

    

    public void OnMarkerReachStart(GameObject marker)
    {
        currentDestinationIndex++;
        // Destroy the current marker
        Destroy(marker);
        Debug.Log("Llego al inicio");
        Debug.Log(currentDestinationIndex);

        if (currentDestinationIndex == 2)
        {
            Destroy(currentDialog);
            currentDialog = Instantiate(dialog2, dialogPosition.position, Quaternion.identity);
        }
        if (currentDestinationIndex == 3)
        {
            Destroy(currentDialog);
            currentDialog = Instantiate(dialog3, dialogPosition.position, Quaternion.identity);
        }
        if (currentDestinationIndex == 4)
        {
            Destroy(currentDialog);
            currentDialog = Instantiate(dialog4, dialogPosition.position, Quaternion.identity);
        }
        if (currentDestinationIndex == 5)
        {
            Destroy(currentDialog);
            currentDialog = Instantiate(dialog5, dialogPosition.position, Quaternion.identity);
        }
        if (currentDestinationIndex == 6)
        {
            Destroy(currentDialog);
            currentDialog = Instantiate(dialogEnd, dialogPosition.position, Quaternion.identity);
            //esperar 3 segundos
            StartCoroutine(MakeFinalPanelVisible(3f));

        }

        SpawnNextMarker();
    }

    public void OnMarkerReachTarget(GameObject marker)
    {
        Destroy(marker);
        if(currentDestinationIndex == 1)
        {
            Destroy(currentDialog);
            currentDialog = Instantiate(dialogTy, dialogPosition.position, Quaternion.identity);
        }
        if (currentDestinationIndex == 2)
        {
            Destroy(currentDialog);
            currentDialog = Instantiate(dialogTy, dialogPosition.position, Quaternion.identity);
        }
        if (currentDestinationIndex == 3)
        {
            Destroy(currentDialog);
            currentDialog = Instantiate(dialogTy, dialogPosition.position, Quaternion.identity);
        }
        if (currentDestinationIndex == 4)
        {
            Destroy(currentDialog);
            currentDialog = Instantiate(dialogTy, dialogPosition.position, Quaternion.identity);
        }
        if (currentDestinationIndex == 5)
        {
            Destroy(currentDialog);
            currentDialog = Instantiate(dialogTy2, dialogPosition.position, Quaternion.identity);
        }

        Instantiate(markerPrefabStart, homePosition.position, Quaternion.identity);



        
    }

    private void Start()
    {

        Instantiate(markerPrefabStart, homePosition.position, Quaternion.identity);
        currentDialog = Instantiate(dialog1, dialogPosition.position, Quaternion.identity);

        // Populate destinations list, skipping the parent transform
        foreach (Transform child in locationPoints.GetComponentsInChildren<Transform>())
        {
            if (child != locationPoints)
            {
                destinationsList.Add(child);
                childPositions.Add(child.localPosition); // Guardar posición local
            }
        }
    }

    private void SpawnNextMarker()
    {
        if (currentDestinationIndex <= destinationsList.Count)
        {
            // Obtener posición global directamente desde la lista
            Vector3 globalPosition = childPositions[currentDestinationIndex - 1];
            Instantiate(markerPrefabTarget, globalPosition, Quaternion.identity);
        }
        else
        {
            Debug.Log("Todas las ubicaciones han sido visitadas");
        }
    }

    IEnumerator MakeFinalPanelVisible(float delay)
    {
        // Espera el tiempo especificado
        yield return new WaitForSeconds(delay);

        // Haz el objeto visible
        
            finalPanel.SetActive(true);
       
    }
}