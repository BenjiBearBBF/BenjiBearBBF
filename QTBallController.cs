using UnityEngine;
using TMPro;

public class QTBallController : MonoBehaviour
{
    public TextMeshProUGUI statusText;

    public void ApplyGateX()
    {
        transform.Rotate(Vector3.right, 90f);
        UpdateStatus("X");
    }

    public void ApplyGateY()
    {
        transform.Rotate(Vector3.up, 90f);
        UpdateStatus("Y");
    }

    public void ApplyGateZ()
    {
        transform.Rotate(Vector3.forward, 90f);
        UpdateStatus("Z");
    }

    public void ApplyGateH()
    {
        transform.Rotate(new Vector3(1, 1, 0), 180f);
        UpdateStatus("H");
    }

    private void UpdateStatus(string gate)
    {
        if (statusText != null)
            statusText.text = $"Last Gate: {gate}";
    }
}