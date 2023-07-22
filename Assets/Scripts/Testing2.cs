using System;
using UnityEngine;

public class Testing2 : MonoBehaviour
{
    // Start is called before the first frame update
    private void Start()
    {
        var myClass = new MyClass();
        var mySecondClass = new MySecondClass();
        TestInterface(myClass);
        TestInterface(mySecondClass);
        TestSecondInterface(myClass);
    }

    private void TestInterface(IMyInterface myInterface)
    {
        myInterface.TestFunction();
    }

    private void TestSecondInterface(IMySecondInterface mySecondInterface)
    {
        mySecondInterface.SecondInterfaceFunction(4);
    }
}

public interface IMyInterface
{
    event EventHandler OnMyEvent;
    int MyInt { get; set; }
    void TestFunction();
}

public interface IMySecondInterface
{
    void SecondInterfaceFunction(int i);
}
public class MyClass : IMyInterface, IMySecondInterface
{
    public event EventHandler OnMyEvent;
    public int MyInt { get; set; }

    public void TestFunction()
    {
        Debug.Log("MyClass.TestFunction()");
    }

    public void SecondInterfaceFunction(int i)
    {
        Debug.Log($"MyClass.SecondInterfaceFunction() is {i.ToString()}");
    }
}

public class MySecondClass : IMyInterface
{
    public event EventHandler OnMyEvent;
    public int MyInt { get; set; }

    public void TestFunction()
    {
        Debug.Log("MySecondClass.TestFunction()");
    }
}