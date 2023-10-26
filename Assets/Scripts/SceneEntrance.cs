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
        if (transitionName == PlayerController.uniqueInstance.areaTransitionName)
            PlayerController.uniqueInstance.transform.position = transform.position;

    }

    // Update is called once per frame
    void Update()
    {

    }
}
