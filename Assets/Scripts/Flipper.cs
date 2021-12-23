using UnityEngine;

public class Flipper : MonoBehaviour
{
    private static Vector3 _theScaleStatic;
    
    private Vector3 _theScale;
    
    private void Awake()
    {
        Debug.Log($"Awakke {gameObject.name}");
        _theScale = GetComponent<Transform>().localScale;
    }

    public static void FlipXY(Transform scale, bool facing)
    {
        _theScaleStatic = scale.localScale;
        _theScaleStatic.x *= -1;
        _theScaleStatic.y *= -1;
        
        scale.localScale = _theScaleStatic;
    }
    
    public static void FlipX(Transform scale, bool facing)
    {
        
        _theScaleStatic = scale.localScale;
        _theScaleStatic.x *= -1;
        scale.localScale = _theScaleStatic;
    }
    
    public static void FlipY(Transform scale, bool facing)
    {
        _theScaleStatic = scale.localScale;
        _theScaleStatic.y *= -1;
        scale.localScale = _theScaleStatic;
    }

    public void FlipXY(bool facing)
    {
        _theScale.x *= -1;
        _theScale.y *= -1;
        transform.localScale = _theScale;
    }
    
    public void FlipX(bool facing)
    {
        _theScale.x *= -1;
        transform.localScale = _theScale;
    }
    
    public void FlipY(bool facing)
    {
        _theScale.y *= -1;
        transform.localScale = _theScale;
    }
}
