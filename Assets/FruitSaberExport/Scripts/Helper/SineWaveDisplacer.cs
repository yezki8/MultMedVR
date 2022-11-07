using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SineWaveDisplacer : MonoBehaviour
{
    [Tooltip("How far to the side you want this object to move.")]
    public float maxOffset = 5;
    [Tooltip("Set this object's starting point.")]
    [Range(-90.0f,90.0f)]
    public float initialOffset = 0;
    [Tooltip("Set to true to randomly set this object's initial starting point.")]
    public bool randomizeInitialOffset = false;
    [Tooltip("Set this object's displacement speed.")]
    [Range(1.0f, 3.0f)]
    public float displacementSpeed = 1.0f;
    [Tooltip("Set to true to randomly set this object's initial displacement speed.")]
    public bool randomizeDisplacementSpeed = false;

    public bool isActive = false;
    Vector3 initialPosition;

    private void Awake()
    {
        initialPosition = transform.position;
        if (randomizeInitialOffset) initialOffset = UnityEngine.Random.Range(-90.0f, 90.0f);
        if (randomizeDisplacementSpeed) displacementSpeed = UnityEngine.Random.Range(1.0f, 3.0f);
    }

    private void FixedUpdate()
    {
        if (isActive)
        {
            transform.position = initialPosition;
            transform.Translate(Vector3.right * maxOffset * Mathf.Sin(Time.timeSinceLevelLoad * displacementSpeed + initialOffset));
        }
    }

}
