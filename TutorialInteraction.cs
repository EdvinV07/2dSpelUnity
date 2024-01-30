using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialInteraction : MonoBehaviour
{
    public Transform circleTransform;
    public float interactionRange = 1f;
    public GameObject TutUI;


    public bool Open = false;

    private void Start()
    {
        TutUI.SetActive(false);
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            float distance = Vector2.Distance(transform.position, circleTransform.position);

            if (!Open)
            {


                if (distance <= interactionRange)
                {
                    // Player is within interaction range

                    OpenTutUI();
                }
            }
            else
            {

                if (distance <= interactionRange)
                {

                    CloseTutUI();
                }
            }

        }
    }

    void OpenTutUI()
    {
        // Activate your shop UI or perform any other actions here

        TutUI.SetActive(true);
        Open = true;
    }

    void CloseTutUI()
    {
        // Activate your shop UI or perform any other actions here
        TutUI.SetActive(false);
        Open = false;
    }
}