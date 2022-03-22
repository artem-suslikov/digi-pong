using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject target;
    public float targetShiftingRange = 8.5f;
    [Range(0.0f, 10.0f)]
    public float shift;
    [Range(0.0f, 10.0f)]
    public float descent;
    [Range(0.0f, 5.0f)]
    public float zoom;
    [Range(0.0f, 45.0f)]
    public float horRotation;
    [Range(0.0f, 45.0f)]
    public float vertRotation;
    //for acc control
    public bool cameraTilt = true;
    [Range(0.0f, 45.0f)]
    public float tiltRotation;

    private Vector3 _defPosition;
    private Vector3 _defRotation;

    private void OnEnable()
    {
        _defPosition = transform.position;
        _defRotation = transform.eulerAngles;
        if (!cameraTilt) tiltRotation = 0.0f;
    }

    private void FixedUpdate()
    {
        float coef = target.transform.position.z / targetShiftingRange;
        coef = Mathf.Clamp(coef, -1.0f, 1.0f);
        coef = Mathf.Pow(coef, 3);
        transform.position = new Vector3(
            _defPosition.x - Mathf.Abs(zoom * coef),
            _defPosition.y - Mathf.Abs(descent * coef),
            _defPosition.z + shift * coef);
        transform.eulerAngles = new Vector3(
            _defRotation.x - Mathf.Abs(vertRotation * coef),
            _defRotation.y - horRotation * coef, 
            _defRotation.z - tiltRotation * coef);
    }
}
