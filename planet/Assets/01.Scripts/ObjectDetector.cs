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

    private PlayerGold playerGold;

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
        playerGold = GameObject.Find("Rocket").GetComponent<PlayerGold>();
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
        if (playerGold.currentGold >= 4)
        {
            selectedObject = turret;
            objectCreated = false;
            playerGold.currentGold -= 4;
        }
    }

    public void OnGardenButtonClicked()
    {
        selectedObject = garden;
        objectCreated = false;
    }

}