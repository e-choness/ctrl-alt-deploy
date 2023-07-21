using System;
using UnityEngine;

public class Testing : MonoBehaviour
{
    public delegate void TestDelegate();
    public delegate bool TestBoolDelegate(int i);

    private TestDelegate _testDelegateFunction;
    private TestBoolDelegate _testBoolDelegateFuntion;

    // None parameter action
    private Action _testAction;

    // Action with parameters
    private Action<int, float> _testIntFloatAction;

    // Func cast out return type, in this case it's boolean
    private Func<bool> testFunc;
    
    // Func can receive a parameter and cast a return type, in this case it's an int parameter and bool return type
    private Func<int, bool> testIntBoolFunc;
    // Start is called before the first frame update
    void Start()
    {
        _testDelegateFunction += MyTestDelegateFunction;
        _testDelegateFunction += MySecondTestDelegateFunction;
        // It's important to call function after function assignment
        _testDelegateFunction();
        _testDelegateFunction -= MySecondTestDelegateFunction;
        // It's important to call function after function assignment
        _testDelegateFunction();

        _testBoolDelegateFuntion += MyTestBoolDelegateFunction;
        Debug.Log($"{_testBoolDelegateFuntion(1).ToString()}");
        
        // delegate function can be initialized through direct call
        _testDelegateFunction = delegate() { Debug.Log("Anonymous method"); };
        _testDelegateFunction();
        
        // delegate function can also be initialized through lambda expression
        _testDelegateFunction = () => { Debug.Log("Lambda expression method"); };
        _testDelegateFunction();
        
        // lambda expression can also return values
        _testBoolDelegateFuntion = (int i) => { return i < 5; };
        Debug.Log($"{_testBoolDelegateFuntion(1).ToString()}");
        
        // lambda expression can also return values in an even compact form
        _testBoolDelegateFuntion = (int i) => i<5;
        Debug.Log($"{_testBoolDelegateFuntion(1).ToString()}");
        
        // The downside is that un-named functions cannot be popped from the function pointer stack
        _testIntFloatAction = (int i, float f) => { Debug.Log($"Test int float action! {i.ToString()} {f.ToString()}"); };
        _testIntFloatAction(2, 4.5f);
        
        testFunc = () => false;
        Debug.Log($"{testFunc().ToString()}");

        testIntBoolFunc = (int i) =>  i < 5 ;
        Debug.Log($"The function get 1 and returns {testIntBoolFunc(1).ToString() }");
    }

    // Update is called once per frame
    private void MyTestDelegateFunction()
    {
        Debug.Log("MyTestDelegateFunction");
    }

    private void MySecondTestDelegateFunction()
    {
        Debug.Log("MySecondTestDelegateFunction");
    }

    // The parameter name of the delegate function can be completely different from the delegate definition
    private bool MyTestBoolDelegateFunction(int ababa)
    {
        return ababa<5;
    }
}
