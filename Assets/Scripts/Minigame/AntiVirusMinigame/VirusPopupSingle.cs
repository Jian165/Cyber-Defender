using UnityEngine;
using UnityEngine.UI;

public class VirusPopupSingle : MonoBehaviour
{
    [SerializeField] Button closeButton;
    [SerializeField] Button advertButton;
    [SerializeField] Computer parentComputer;

    private void Awake()
    {
        closeButton.onClick.AddListener(OnCloseAds);
        advertButton.onClick.AddListener(OnOpenAds);
    }

    private void OnOpenAds()
    {
        TimeController.instance.SkipTimePenalty();
        parentComputer.PlayerPenalized();
    }

    private void OnCloseAds()
    {
        VirusPopupManager.instance.popUpViruseActiveList.Remove(gameObject);
        gameObject.SetActive(false);
    }
}
