using System;
using UnityEngine;
using UnityEngine.Events;

public class TestingEvents : MonoBehaviour
{
    public event EventHandler<OnSpacePressedEventArgs> OnSpacePressed;

    public class OnSpacePressedEventArgs : EventArgs
    {
        public int spaceCount;
    }

    public event TestEventDelegate OnFloatEvent;
    public delegate void TestEventDelegate(float f);

    public event Action<bool,int> OnActionEvent;

    public UnityEvent OnUnityEvent;
    private int _spaceCount = 0;
    // Start is called before the first frame update
    void Start()
    {
       
    }
 
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            // Space pressed
            _spaceCount++;
            OnSpacePressed?.Invoke(this, new OnSpacePressedEventArgs { spaceCount = _spaceCount});
            
            OnFloatEvent?.Invoke(5.5f);
            
            OnActionEvent?.Invoke(true, 56);
            
            OnUnityEvent?.Invoke();
        }
    }
}
