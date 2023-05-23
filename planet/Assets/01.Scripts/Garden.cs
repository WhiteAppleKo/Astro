using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Garden : MonoBehaviour
{
    public GameObject object1;
    public GameObject object2;
    public GameObject object3;
    public float transitionTime1to2 = 2f;
    public float transitionTime2to3 = 5f;

    private float timer = 0f;
    private int currentObject = 1;

    // Start is called before the first frame update
    void Start()
    {
        object1.SetActive(true);
        object2.SetActive(false);
        object3.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        if (currentObject == 1 && timer >= transitionTime1to2)
        {
            TransitionTo(object2);
            currentObject = 2;
            timer = 0f;
        }
        else if (currentObject == 2 && timer >= transitionTime2to3)
        {
            TransitionTo(object3);
            currentObject = 3;
            timer = 0f;
        }
    }

    void TransitionTo(GameObject targetObject)
    {
        object1.SetActive(false);
        object2.SetActive(false);
        object3.SetActive(false);

        targetObject.SetActive(true);
    }
}