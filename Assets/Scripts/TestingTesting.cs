using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestingTesting : MonoBehaviour
{
    [SerializeField] private ActionOnTimer actionOnTimer;

    // Start is called before the first frame update
    void Start()
    {
        actionOnTimer = GetComponent<ActionOnTimer>();
        actionOnTimer.SetTimer(1.0f, ()=>{Debug.Log("Timer complete!");});
    }
}
