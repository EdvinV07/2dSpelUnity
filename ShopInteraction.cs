using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopInteraction : MonoBehaviour
{
    public Transform circleTransform;
    public float interactionRange = 1f;
    public GameObject shopUI; // Reference to your shop UI GameObject

    public bool Open = false;

    private void Start()
    {
        shopUI.SetActive(false);
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
                    Debug.Log("Boxers");
                    OpenShopUI();
                }
            }
            else
            {
               
                if (distance <= interactionRange)
                {

                    CloseShopUI();
                }
            }
     
        }
    }

    void OpenShopUI()
    {
        // Activate your shop UI or perform any other actions here
        shopUI.SetActive(true);
        Open = true;
    }

    void CloseShopUI()
    {
        // Activate your shop UI or perform any other actions here
        shopUI.SetActive(false);
        Open = false;
    }
}


