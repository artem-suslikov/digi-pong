using System.Collections;
using UnityEngine;

public class BallRaycast : MonoBehaviour
{
    public float maxLength;
    public bool drawLine;

    [HideInInspector]
    public Rigidbody rbody;
    [HideInInspector]
    public RaycastHit hit;
    private LineRenderer _lineRenderer;
    private Ray _ray;

    private void Start()
    {
        _lineRenderer = GetComponent<LineRenderer>();
        if (_lineRenderer != null)
            drawLine = true;
    }

    private void FixedUpdate()
    {
        if (rbody != null)
        {
            _ray = new Ray(rbody.position, rbody.velocity);
            float remainingLength = maxLength;

            if (drawLine)
            {
                _lineRenderer.positionCount = 1;
                _lineRenderer.SetPosition(0, _ray.origin);
            }

            while (remainingLength > 0)
            {
                if (Physics.Raycast(_ray, out hit, remainingLength))
                {
                    if (drawLine)
                    {
                        _lineRenderer.positionCount++;
                        _lineRenderer.SetPosition(_lineRenderer.positionCount - 1, hit.point);
                    }
                    if (hit.collider.tag == "goal" || hit.collider.tag == "bat")
                    {
                        remainingLength = 0;
                    }
                    else
                    {
                        remainingLength -= Vector3.Distance(_ray.origin, hit.point);
                        _ray = new Ray(hit.point, Vector3.Reflect(_ray.direction, hit.normal));
                    }
                }
                else
                {
                    if (drawLine)
                    {
                        _lineRenderer.positionCount++;
                        _lineRenderer.SetPosition(_lineRenderer.positionCount - 1, _ray.origin + _ray.direction * remainingLength);
                    }
                    remainingLength = 0;
                }
            }
        }
    }
}
