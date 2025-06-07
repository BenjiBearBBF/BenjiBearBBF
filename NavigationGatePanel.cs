using UnityEngine;
using UnityEngine.UI;

public class NavigationGatePanel : MonoBehaviour
{
    public enum NavMode { Slide, Turn, Tilt }
    public enum WorldProjection { Dim2, Dim3, Dim4 }

    public Text navText;
    public Text projText;

    private NavMode currentMode = NavMode.Turn;
    private WorldProjection currentProj = WorldProjection.Dim3;

    public void CycleNavMode()
    {
        currentMode = (NavMode)(((int)currentMode + 1) % System.Enum.GetValues(typeof(NavMode)).Length);
        UpdateUI();
    }

    public void CycleProjection()
    {
        currentProj = (WorldProjection)(((int)currentProj + 1) % System.Enum.GetValues(typeof(WorldProjection)).Length);
        UpdateUI();
    }

    private void UpdateUI()
    {
        navText.text = "Nav: " + currentMode;
        projText.text = "Projection: " + currentProj;
    }

    void Start()
    {
        UpdateUI();
    }
}