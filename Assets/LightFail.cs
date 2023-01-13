using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightFail : MonoBehaviour
{
    public new Light light;
    public GameObject emissiveGameObject;
    public AnimationCurve animationCurve;
    public Color color;

    public WrapMode wrapMode = WrapMode.PingPong;

    private Material emissiveMaterial;

    // Start is called before the first frame update
    void Start()
    {
        animationCurve.postWrapMode = this.wrapMode;
    }

    // Update is called once per frame
    void Update()
    {
        this.light.color = color;
        float value = animationCurve.Evaluate(Time.time);
        this.light.intensity = value;
        this.emissiveMaterial.SetColor("_EmissionColor", this.color * value);

    }
}
