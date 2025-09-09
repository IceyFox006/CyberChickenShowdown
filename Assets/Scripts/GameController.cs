using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

public class GameController : MonoBehaviour
{
    private static GameController instance;

    public static GameController Instance { get => instance; set => instance = value; }

    private void Awake()
    {
        instance = this;
    }
}
