using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OceanManager : MonoBehaviour
{
    public float wavesHeight = .7f;
    public float wavesFrequency = 1.0f;
    public float wavesSpeed = .07f;
    public Transform ocean;

    private Material oceanMaterial;
    Texture2D wavesDisplacement;

    void SetVariables()
    {
        oceanMaterial = ocean.GetComponent<Renderer>().sharedMaterial;
        wavesDisplacement = (Texture2D)oceanMaterial.GetTexture("_WavesDisplacement");
    }
    // Start is called before the first frame update
    void Start()
    {
        SetVariables();
    }

    public float WaterHeightAtPosition(Vector3 position)
    {
        return ocean.position.y + wavesDisplacement.GetPixelBilinear(position.x * wavesFrequency, position.z * wavesFrequency + Time.time * wavesSpeed).g * wavesHeight * ocean.localScale.x;
    }

    private void OnValidate()
    {
        if(!oceanMaterial)
            SetVariables();

        UpdateMaterial();
    }

    private void UpdateMaterial()
    {
        oceanMaterial.SetFloat("_WavesFrequency", wavesFrequency);
        oceanMaterial.SetFloat("_WavesSpeed", wavesSpeed);
        oceanMaterial.SetFloat("_WavesHeight", wavesHeight);
    }
}
