using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EndGameScreen : MonoBehaviour
{
    [SerializeField] GameObject endTitle;
    [SerializeField] GameObject endStats;
    [SerializeField] GameObject overlay;
    [SerializeField] Slider timeSlider;

    [SerializeField] float titleFadeTime;
    [SerializeField] float titleOnScreenTime;
    [SerializeField] float titleMoveTime;
    [SerializeField] float statsFadeTime;
    [SerializeField] float statsOnScreenTime = 10;

    [SerializeField] Vector3 titleEndPos;

    [SerializeField] string deathText;
    [SerializeField] string finishedGameText;

    Vector3 titleOrigin;

    RectTransform titleRectTransform;

    CanvasGroup endTitleCanvas;
    CanvasGroup endStatsCanvas;
    CanvasGroup overlayCanvas;

    PlayerStats stats;

    [SerializeField] TextMeshProUGUI pointsText, killsText, headShotsText, bloodGainText, deathsText,roundText, deadText;

    private void Awake()
    {
        endTitleCanvas = endTitle.GetComponent<CanvasGroup>();
        endStatsCanvas = endStats.GetComponent<CanvasGroup>();
        overlayCanvas = overlay.GetComponent<CanvasGroup>();
        stats = FindObjectOfType<PlayerStats>();
        titleRectTransform = endTitle.GetComponent<RectTransform>();

        titleOrigin = titleRectTransform.localPosition;
    }

    private void Start()
    {
        titleRectTransform.localPosition = titleOrigin;

        endTitleCanvas.alpha = 0;
        endStatsCanvas.alpha = 0;
        overlayCanvas.alpha = 0;
    }

    [ContextMenu("Tween")]
    public void Open(bool finishedGame)
    {
        Time.timeScale = 0;

        if(finishedGame )
        {
            deadText.text = finishedGameText;
        }
        else
        {
            deadText.text = deathText;
        }
        timeSlider.value = 0;

        // Chag
        roundText.text = "you survived " + (SpawnerManager.currentRound -1) + " rounds";
        pointsText.text = stats.pointsGained.ToString();
        killsText.text = stats.kills.ToString();
        headShotsText.text = stats.headshotKills.ToString();
        bloodGainText.text = stats.GetComponent<PlayerAbilityCaster>().bloodSpent.ToString();
        deathsText.text = stats.deaths.ToString();


        Sequence on = DOTween.Sequence();
        on.SetUpdate(true);
        on.Append(overlayCanvas.DOFade(1, titleFadeTime + titleMoveTime + titleOnScreenTime + statsFadeTime).SetUpdate(true));
        on.Append(endTitleCanvas.DOFade(1, titleFadeTime));
        on.AppendInterval(titleOnScreenTime);
        on.AppendCallback(() => titleRectTransform.DOLocalMove(titleEndPos, titleMoveTime).SetUpdate(true));
        on.AppendInterval(titleMoveTime);
        on.Append(endStatsCanvas.DOFade(1, statsFadeTime));
        on.AppendCallback(() => 
        { 
            endStatsCanvas.interactable = true; 
            Cursor.lockState = CursorLockMode.Confined;
            Cursor.visible = true;
            
        });
        on.Append(DOTween.To(() => timeSlider.value, x => timeSlider.value = x, 100, statsOnScreenTime));
        on.AppendCallback(() => { QuitToMenu(); });
    }

    public void QuitToMenu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(0);
    }
}
