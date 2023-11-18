using UnityEngine;

[ExecuteInEditMode]
[RequireComponent (typeof(Camera))]

public class RadialBlur : MonoBehaviour
{
    [Range(0.0f, 1.0f)]
	public float _strength = 0.25f;
    [Range(0.0f, 50.0f)]
    public int _samples = 20;
    public Vector2 _center = new Vector2 (0.5f, 0.5f);

    Camera cam;

	private Shader radialBlurShader = null;
	private Material radialBlurMaterial = null;
    bool isSupported = true;

    void Start()
    {
        CheckResources();
    }

    public bool CheckResources()
    {
        radialBlurShader = Shader.Find("MyShaders/RadialBlur");
        radialBlurMaterial = CheckShader(radialBlurShader, radialBlurMaterial);

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
        DestroyImmediate(radialBlurMaterial);
#else
        Destroy(radialBlurMaterial);
#endif
    }

    void OnRenderImage (RenderTexture source, RenderTexture destination)
	{
		if (CheckResources() == false)
		{
			Graphics.Blit (source, destination);
			return;
		}

		radialBlurMaterial.SetFloat ("_strength", _strength);
		radialBlurMaterial.SetInt ("_samples", _samples);
		radialBlurMaterial.SetFloat ("_centerX", _center.x);
		radialBlurMaterial.SetFloat ("_centerY", _center.y);

		Graphics.Blit (source, destination, radialBlurMaterial);
	}
}
