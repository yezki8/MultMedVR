using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DissolveController : MonoBehaviour
{
    public Transform glowModel;
    public Transform dissolveModel;
    public float dissolveThreshold = 0.0f;
    public float dissolveStartingPoint = 0.0f;
    public float dissolveRate = 1.0f;

    [SerializeField] bool isActive = false;
    AudioSource sfx;
    Material[] glowShader;
    Material[] dissolveShader;
    String propertyID;

    void Start()
    {
        glowShader = glowModel.GetComponent<Renderer>().materials;
        dissolveShader = dissolveModel.GetComponent<Renderer>().materials;
        propertyID = glowShader[0].shader.GetPropertyName(0);
        dissolveThreshold = glowShader[0].GetFloat(propertyID);
        sfx = gameObject.GetComponent<AudioSource>();
        //StartDissolver();
    }

    void FixedUpdate()
    {
        if (isActive) Dissolver();
    }

    public void StartDissolver()
    {
        if (sfx != null) sfx.Play();
        dissolveThreshold = dissolveStartingPoint;
        isActive = true;
    }

    void Dissolver()
    {
        dissolveThreshold += dissolveRate * Time.fixedDeltaTime;
        for (int i = 0; i < glowShader.Length; i++)
        {
            glowShader[i].SetFloat(propertyID, dissolveThreshold);
            dissolveShader[i].SetFloat(propertyID, dissolveThreshold - 0.05f);
        }
    }

}
