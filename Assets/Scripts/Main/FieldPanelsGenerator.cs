using UnityEngine;

public class FieldPanelsGenerator : MonoBehaviour
{
    public GameObject fieldPanel;
    public GameObject sidePanel;
    public int fieldWidth;
    public int fieldLength;

    private void OnEnable()
    {
        for (int i = -fieldWidth/2; i < fieldWidth/2; i++)
        {
            for (int j = -fieldLength/2; j < fieldLength/2; j++)
            {
                Instantiate(fieldPanel, new Vector3( j + 0.5f, 0.5f, i + 0.5f), Quaternion.identity);
            }
        }

        for (int i = -fieldLength/2; i < fieldLength/2; i++)
        {
            Instantiate(sidePanel, new Vector3(i + 0.5f, 1.5f, -10.05f), Quaternion.identity);
            Instantiate(sidePanel, new Vector3(i + 0.5f, 1.5f, 10.05f), Quaternion.identity);
        }
    }
}
