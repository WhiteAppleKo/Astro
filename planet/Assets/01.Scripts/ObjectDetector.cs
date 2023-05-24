using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ObjectDetector : MonoBehaviour
{
    public GameObject objectdetector;
    public GameObject turret;
    public GameObject garden;

    public GameObject selectedObject;

    Camera mainCamera;
    RectTransform rectTransform;
    Vector2 targetPosition;
    RaycastHit2D hit;
    bool objectCreated;

    // Start is called before the first frame update
    void Start()
    {
        mainCamera = Camera.main;
        objectCreated = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!objectCreated && Input.GetMouseButtonDown(0))
        {
            Vector2 worldPoint = mainCamera.ScreenToWorldPoint(Input.mousePosition);
            hit = Physics2D.Raycast(worldPoint, Vector2.zero);

            Debug.Log("123" + worldPoint);
            Debug.Log(hit.collider);

            if (selectedObject != null)
            {
                Instantiate(selectedObject, worldPoint, Quaternion.identity);
                objectCreated = true;
            }
        }
    }

    public void OnTurretButtonClicked()
    {
        selectedObject = turret;
        objectCreated = false;
    }

    public void OnGardenButtonClicked()
    {
        selectedObject = garden;
        objectCreated = false;
    }

}