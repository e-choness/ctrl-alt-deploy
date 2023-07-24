using System;
using System.Collections.Generic;
using UnityEngine;

public class TestingGenerics : MonoBehaviour
{
    private delegate void MyActionDelegate<T1, T2>(T1 t1, T2 t2);
    private Action<int, string> action;

    private delegate TResult MyFucnDelegate<T1, TResult>(T1 t1);
    private Func<int, bool> func;
    // Start is called before the first frame update
    private void Start()
    {
        // IEnemy<EnemyMinion> myClassMinion;
        // myClassMinion = new MyClass<EnemyMinion>(new EnemyMinion());
        // int[] intArray = myClassMinion.CreateArray(5, 6);
        // OutputArray(intArray);
        //
        // IEnemy<EnemyArcher> myClassArcher = new MyClass<EnemyArcher>(new EnemyArcher());
        // string[] stringArray = myClassArcher.CreateArray("dadad", "daaddadsada");
        // OutputArray(stringArray);
        
        // TestMultiGenerics(intArray[0], stringArray[0]);
        
        
        // Debug.Log(myClassMinion.value.ToString());
    }
    
    private void OutputArray<T>(IReadOnlyList<T> tArray)
    {
        Debug.Log(tArray.Count.ToString() + " " + tArray[0] + " " + tArray[1]);
    }

    private void TestMultiGenerics<T1, T2>(T1 t1, T2 t2)
    {
        Debug.Log(t1.GetType());
        Debug.Log(t2.GetType());
    }
}

public class MyClass<T> where T: IEnemy<T> // T can be followed by classes, interfaces and methods
{
    public T value;

    public MyClass(T value)
    {
        value.Damage(value);
    }
    public T[] CreateArray<T>(T firstElement, T secondElement)
    {
        return new T[] { firstElement, secondElement };
    }
}

public interface IEnemy<T>
{
    void Damage(T t);
}

public class EnemyMinion : IEnemy<int>
{
    public void Damage(int i){
        Debug.Log("EnemyMinion.Damage()" + i.ToString());
    }
}

public class EnemyArcher : IEnemy<string>
{
    public void Damage(string s){
        Debug.Log("EnemyArcher.Damage()" + s);
    }
}