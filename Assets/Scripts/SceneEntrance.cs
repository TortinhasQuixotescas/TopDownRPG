using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneEntrance : MonoBehaviour
{
    public string transitionName;

    // Start is called before the first frame update
    void Start()
    {
        // Debug.Log(PlayerController.uniqueInstance.areaTransitionName);
        if (transitionName == PlayerController.uniqueInstance.areaTransitionName)
        {
            Vector3 zAjust = transform.position;
            zAjust.z = 0;
            PlayerController.uniqueInstance.transform.position = zAjust;
        }

    }

    // Update is called once per frame
    void Update()
    {

    }
}
