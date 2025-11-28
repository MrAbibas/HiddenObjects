using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace App.UI.HUD
{
    public class TargetItemOnUI : MonoBehaviour
    {
        [SerializeField] private Image _image;
        [SerializeField] private TMP_Text _countText;

        [Header("Show Animation")]
        [SerializeField] private float showPunchScale = 1.2f;
        [SerializeField] private float showPunchDuration = 0.4f;
    
        [Header("Update Animation")]
        [SerializeField] private float updatePunchScale = 1.2f;
        [SerializeField] private float updatePunchDuration = 0.4f;
    
        [Header("Hide Animation")]
        [SerializeField] private float hideEndScale = 1.2f;
        [SerializeField] private float hideDuration = 0.4f;

        public void Show(Sprite icon, int count)
        {
            _image.sprite = icon;
            _countText.text = count.ToString();
            _image.rectTransform.DOPunchScale(Vector3.one * showPunchScale, showPunchDuration);
        }

        public void UpdateCount(int count)
        {
            _countText.text = count.ToString();
            _countText.rectTransform.DOPunchScale(Vector3.one * updatePunchScale, updatePunchDuration);
        }

        public void Hide()
        {
            _countText.text = "0";
            _image.rectTransform.DOScale(Vector3.one * hideEndScale, hideDuration).SetEase(Ease.InOutBack);
            Destroy(gameObject);
        }
    }
}
