using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class TestAccount : MonoBehaviour
{
    [DllImport("__Internal")]
    private static extern string GetAccount();


    // Start is called before the first frame update
    void Start()
    {
        GetAccount();
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
