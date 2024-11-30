using UnityEngine;
using UnityEngine.UI;

public class VirusPopupSingle : MonoBehaviour
{
    [SerializeField] Button closeButton;
    [SerializeField] Button advertButton;

    private void Awake()
    {
        closeButton.onClick.AddListener(OnCloseAds);
        advertButton.onClick.AddListener(OnOpenAds);
    }

    private void OnOpenAds()
    {
        Debug.Log("Penalty!");
    }

    private void OnCloseAds()
    {
        VirusPopupManager.instance.popUpViruseActiveList.Remove(gameObject);
        gameObject.SetActive(false);
    }
}
