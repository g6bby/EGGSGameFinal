using UnityEngine;

[ExecuteInEditMode]
[RequireComponent(typeof(Camera))]

public class Kino : MonoBehaviour
{
    public enum _styleX { Cinematic = 0, Corridor = 1, Scarf = 2 }
    public _styleX _style;
    [Range(0.0f, 1.0f)]
    public float _width = 0.25f;

    public Color _color = Color.black;

    Camera cam;

    private Shader kinoShader = null;
    private Material kinoMaterial = null;
    bool isSupported = true;

    void Start()
    {
        CheckResources();
    }

    public bool CheckResources()
    {
        kinoShader = Shader.Find("MyShaders/Kino");
        kinoMaterial = CheckShader(kinoShader, kinoMaterial);

        return isSupported;
    }

    protected Material CheckShader(Shader s, Material m)
    {
        if (s == null)
        {
            Debug.Log("Missing shader on " + ToString());
            this.enabled = false;
            return null;
        }

        if (s.isSupported == false)
        {
            Debug.Log("The shader " + s.ToString() + " is not supported on this platform");
            this.enabled = false;
            return null;
        }

        cam = GetComponent<Camera>();
        cam.renderingPath = RenderingPath.UsePlayerSettings;

        m = new Material(s);
        m.hideFlags = HideFlags.DontSave;

        if (s.isSupported && m && m.shader == s)
            return m;

        return m;
    }

    void OnDestroy()
    {
#if UNITY_EDITOR
        DestroyImmediate(kinoMaterial);
#else
        Destroy(kinoMaterial);
#endif
    }

    void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        if (CheckResources() == false)
        {
            Graphics.Blit(source, destination);
            return;
        }

       kinoMaterial.SetColor("_color", _color);

       if (_styleX.Cinematic == _style)
       {
            Shader.EnableKeyword("Cinematic");
            Shader.DisableKeyword("Corridor");
            Shader.DisableKeyword("Scarf");

            kinoMaterial.SetFloat("_top", 1 - _width/2);
            kinoMaterial.SetFloat("_bottom", _width/2);
        }

        if (_styleX.Corridor == _style)
        {
            Shader.DisableKeyword("Cinematic");
            Shader.EnableKeyword("Corridor");
            Shader.DisableKeyword("Scarf");

            kinoMaterial.SetFloat("_right", 1 - _width/2);
            kinoMaterial.SetFloat("_left", _width/2);
        }

        else if (_styleX.Scarf == _style)
        {
            Shader.DisableKeyword("Cinematic");
            Shader.DisableKeyword("Corridor");
            Shader.EnableKeyword("Scarf");

            kinoMaterial.SetFloat("_top", 1 - _width/2);
            kinoMaterial.SetFloat("_bottom", _width/2);
            kinoMaterial.SetFloat("_right", 1 - _width/2);
            kinoMaterial.SetFloat("_left", _width/2);
        }

        Graphics.Blit(source, destination, kinoMaterial);
    }
}
