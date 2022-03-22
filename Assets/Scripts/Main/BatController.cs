using UnityEngine;

public class BatController : MonoBehaviour
{
    public bool aI;
    public InputMode inputMode;
    [HideInInspector]
    public float horInput;
    private BatAI _batAI;
    private Vector3 filteredAccelerometerValue = Vector3.zero;

    private void OnEnable()
    {
        if (aI) 
        {
            _batAI = GetComponent<BatAI>();
            inputMode = InputMode.Simple;
        }
    }

    private void Start()
    {
        if (inputMode == InputMode.Accelerometer)
        {
            filteredAccelerometerValue = Input.acceleration;
        }
    }

    private void Update()
    {
        if (aI) horInput = _batAI.moveDirection;
        else 
        {
            if (inputMode == InputMode.Simple) 
            {
                horInput = Input.GetAxis("Horizontal");
            }
            if (inputMode == InputMode.Accelerometer)
            {
                filteredAccelerometerValue = AccelerometerFilter(filteredAccelerometerValue);
                horInput = Mathf.Clamp(filteredAccelerometerValue.x * 2.0f, -1.0f, 1.0f);
            }
        }
    }

    private Vector3 AccelerometerFilter(Vector3 input)
    {
        Vector3 output = Vector3.Lerp(input, Input.acceleration, 0.6f);
        return output;
    }
}
