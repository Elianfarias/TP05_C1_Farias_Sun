using UnityEngine;

public class ParallaxLayer : MonoBehaviour
{
    [SerializeField] public float speed = 0.2f;                
    [SerializeField] private string texPropName = "_MainTex";
    [SerializeField] private string colorPropName = "_Color";

    private Renderer _r;
    private MaterialPropertyBlock _mpb;
    private Vector2 _tiling = Vector2.one;
    private float _alpha = 1f;
    private float _offsetX;

    private void Awake()
    {
        _r = GetComponent<Renderer>();
        _mpb = new MaterialPropertyBlock();

        var mat = _r.sharedMaterial;
        if (mat != null)
        {
            Vector4 st = mat.GetVector(texPropName + "_ST");
            if (st != Vector4.zero) _tiling = new Vector2(st.x, st.y);
        }
        Apply();
    }

    public void SyncWithClock(float baseDistance)
    {
        _offsetX = baseDistance * speed;
        Apply();
    }

    public void SetAlpha(float a)
    {
        _alpha = a;
        Apply();
    }

    private void Apply()
    {
        if (_r == null) return;
        _r.GetPropertyBlock(_mpb);

        _mpb.SetVector(texPropName + "_ST", new Vector4(_tiling.x, _tiling.y, _offsetX, 0f));

        if (_r.sharedMaterial != null && _r.sharedMaterial.HasProperty(colorPropName))
        {
            Color c = _r.sharedMaterial.GetColor(colorPropName);
            c.a = _alpha;
            _mpb.SetColor(colorPropName, c);
        }

        _r.SetPropertyBlock(_mpb);
    }
}
