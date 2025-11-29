using App.Gameplay.GameplayFSM;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using VContainer;

namespace App.UI.LevelResult
{
    public class LevelResultPanel : MonoBehaviour
    {
        public UnityEvent OnRestartClicked => restartButton.onClick;
        
        [SerializeField] private RectTransform winContent;
        [SerializeField] private RectTransform loseContent;
        [SerializeField] private Button restartButton;
        
        [Header("Animation")]
        [SerializeField] private Vector3 punchScale;
        [SerializeField] private float punchDuration;
        [SerializeField] private int punchElasticity;
        [SerializeField] private Ease punchEase;
        
        private void Start()
        {
            restartButton.gameObject.SetActive(false);
            gameObject.SetActive(false);
            loseContent.gameObject.SetActive(false);
            winContent.gameObject.SetActive(false);
        }
        
        public void ShowLose()
        {
            gameObject.SetActive(true);
            loseContent.gameObject.SetActive(true);
            loseContent
                .DOPunchScale(punchScale, punchDuration, punchElasticity)
                .SetEase(punchEase)
                .OnComplete(() => restartButton.gameObject.SetActive(true));
        }

        public void ShowWin()
        {
            gameObject.SetActive(true);
            winContent.gameObject.SetActive(true);
            winContent
                .DOPunchScale(punchScale, punchDuration, punchElasticity)
                .SetEase(punchEase)
                .OnComplete(() => restartButton.gameObject.SetActive(true));
        }

        public void Hide()
        {
            gameObject.SetActive(false);
            winContent.gameObject.SetActive(false);
            loseContent.gameObject.SetActive(false);
            restartButton.gameObject.SetActive(false);
        }
    }
}
