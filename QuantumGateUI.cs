using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class QuantumGateUI : MonoBehaviour
{
    [System.Serializable]
    public class QuantumGate
    {
        public string label;
        public Vector3 axis;
        public float angle;
    }

    public GameObject buttonPrefab;
    public Transform buttonParent;
    public QFlyCam targetCamera;

    public List<QuantumGate> gates = new List<QuantumGate>
    {
        new QuantumGate { label = "X", axis = Vector3.right, angle = 90 },
        new QuantumGate { label = "Y", axis = Vector3.up, angle = 90 },
        new QuantumGate { label = "Z", axis = Vector3.forward, angle = 90 },
        new QuantumGate { label = "RX(90)", axis = Vector3.right, angle = 90 },
        new QuantumGate { label = "RZ(180)", axis = Vector3.forward, angle = 180 },
        new QuantumGate { label = "H", axis = new Vector3(1, 1, 0), angle = 180 }
    };

    void Start()
    {
        foreach (var gate in gates)
        {
            GameObject b = Instantiate(buttonPrefab, buttonParent);
            b.GetComponentInChildren<Text>().text = gate.label;

            Vector3 axisCopy = gate.axis;
            float angleCopy = gate.angle;

            b.GetComponent<Button>().onClick.AddListener(() =>
            {
                if (targetCamera != null)
                    targetCamera.ApplyQuantumGate(axisCopy, angleCopy);
            });
        }
    }
}

