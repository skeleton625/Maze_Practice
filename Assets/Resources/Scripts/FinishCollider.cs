using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishCollider : MonoBehaviour
{
    private bool isFinish;
    public bool IsFinish
    { get => isFinish; }

    private void Awake()
    {
        isFinish = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag.Equals("Player"))
            isFinish = true;
    }
}
