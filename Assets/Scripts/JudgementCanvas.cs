using TMPro;
using UnityEngine;
using System.Collections;

public class JudgementCanvas : MonoBehaviour
{
    public PointsSystem pointsSystem;
    public UIManager uIManager;

    public TextMeshProUGUI partnerText;

    public float judgementDuration = 5f;

    void Awake()
    {
        if (pointsSystem == null) pointsSystem = FindFirstObjectByType<PointsSystem>();
        if (uIManager == null) uIManager = FindFirstObjectByType<UIManager>();
    }

    public void StartJudgement()
    {
        uIManager.ShowJudgement();
        partnerText.text = pointsSystem.GetJudgementText();
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        StartCoroutine(WaitAndNextDay());
    }

    private IEnumerator WaitAndNextDay()
    {
        yield return new WaitForSeconds(judgementDuration);
        StartNextDay();
    }

    public void StartNextDay()
    {
        uIManager.HideAll();
        pointsSystem.StartNewDay();
    }
}
