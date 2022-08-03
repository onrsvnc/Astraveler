using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindowRotator : MonoBehaviour
{
    [SerializeField] Vector3 rotationVector;
    void Update()
    {
        transform.Rotate (rotationVector * Time.deltaTime);
    }
}
