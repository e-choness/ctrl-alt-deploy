using System;
using UnityEngine;

public class TestingEventSubscriber : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        TestingEvents testingEvents = GetComponent<TestingEvents>();
        testingEvents.OnSpacePressed += TestingEvents_OnspacePressed;
        testingEvents.OnFloatEvent += TestingEvents_OnfloatEvent;
        testingEvents.OnActionEvent += TestingEvents_OnactionEvent;
    }

    private void TestingEvents_OnactionEvent(bool arg1, int arg2)
    {
        Debug.Log(arg1 + " " + arg2);
    }

    private void TestingEvents_OnfloatEvent(float f)
    {
        Debug.Log("Float: " + f);
    }

    private void TestingEvents_OnspacePressed(object sender, TestingEvents.OnSpacePressedEventArgs e)
    {
        Debug.Log("Space!" + e.spaceCount);
        TestingEvents testingEvents = GetComponent<TestingEvents>();
        testingEvents.OnSpacePressed -= TestingEvents_OnspacePressed;
    }

    public void TestingUnityEvent()
    {
        Debug.Log("TestingUnityEvents");
    }
}
