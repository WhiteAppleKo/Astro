using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ObjectDetector : MonoBehaviour
{

    public GameObject turret;

    Camera mainCamera;
    RectTransform rectTransform;
    Vector2 targetPosition;
    RaycastHit2D hit;



    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (!EventSystem.current.IsPointerOverGameObject())
        {
            if (Input.GetMouseButtonDown(0))
            {
                Vector2 worldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                hit = Physics2D.Raycast(worldPoint, Vector2.zero);

                Debug.Log("123" + worldPoint);
                Debug.Log(hit.collider);

                Instantiate(turret, worldPoint, Quaternion.identity);
            }
        }
    }
}
