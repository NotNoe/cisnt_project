using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;

public class FMODEvents : MonoBehaviour
{
    [field: SerializeField] public EventReference Door { get; private set; }
    [field: SerializeField] public EventReference Music { get; private set; }
    [field: SerializeField] public EventReference Paper { get; private set; }
    [field: SerializeField] public EventReference EndDay { get; private set; }

    static public FMODEvents Instance;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else Destroy(gameObject);
    }
}
