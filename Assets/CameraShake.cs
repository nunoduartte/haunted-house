using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    public float duration = .1f;
    public Shader shader;
    public Vector2 ShakeSize = new Vector2(.001f, .001f);

    Vector2 _shake = Vector2.zero;

    float CurrentTime = 0;

    Material mat; 
    // Start is called before the first frame update
    void Start()
    {
        mat = new Material(shader);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyUp(KeyCode.F1))
            DoShake();
    }

    void DoShake(){
        CurrentTime = 0.5f;
    }

    void OnRenderImage(RenderTexture source, RenderTexture destination){
        if(CurrentTime > 0){
            CurrentTime -= duration;
            mat.SetTexture("_MainTex", source);
            mat.SetVector("_shake", new Vector2(Mathf.Cos(Random.value) * ShakeSize.x, Mathf.Sin(Random.value) * ShakeSize.y));
            ShakeSize *= -1;
            Graphics.Blit(source, destination, mat);
        }
    }
}
