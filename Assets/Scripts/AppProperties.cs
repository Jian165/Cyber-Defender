using TMPro;
using UnityEditor.iOS.Extensions.Common;
using UnityEngine;
using UnityEngine.UI;

public class AppProperties : MonoBehaviour
{
    [SerializeField] private MalwareApp AppParent;

    [SerializeField] private Image appIcon;
    [SerializeField] private TextMeshProUGUI appNameUI;
    [SerializeField] private TextMeshProUGUI publisherNameUI;
    [SerializeField] private TextMeshProUGUI locationUI;
    [SerializeField] private TextMeshProUGUI SizeUI;
    [SerializeField] private TextMeshProUGUI CPUusageUI;
    [SerializeField] private TextMeshProUGUI RAMusageUI;
    [SerializeField] private TextMeshProUGUI signatureUI;
    [SerializeField] private TextMeshProUGUI PermissionUI;

    [SerializeField] private Button UnInstallButton;
    [SerializeField] private Button cancel;
    [SerializeField] private Button close;

    [SerializeField] private UninstallLoading uninstallLoading;

    private MalwareDescriptionSO DescriptionToDisplay;
    private void Start()
    {
        UnInstallButton.onClick.AddListener(OnUninstall);
        cancel.onClick.AddListener(onCloseUI);
        close.onClick.AddListener(onCloseUI);

        if (DescriptionToDisplay != null)
        {
            appIcon.sprite = DescriptionToDisplay.appImage;
            appNameUI.text = DescriptionToDisplay.appName;
            publisherNameUI.text = DescriptionToDisplay.appPublisher;
            locationUI.text = DescriptionToDisplay.appFileLocation;
            SizeUI.text = DescriptionToDisplay.appSize;
            CPUusageUI.text = DescriptionToDisplay.appCPUResource;
            RAMusageUI.text = DescriptionToDisplay.appRamResource;
            signatureUI.text = DescriptionToDisplay.appDigitalSgnature;
            PermissionUI.text = DescriptionToDisplay.appPermission;
        }
        
    }

    private void Update()
    {
        if (uninstallLoading.gameObject.activeSelf==true)
        {
            if (uninstallLoading.isLoadingFinished() == false)
            {
                uninstallLoading.GetAppDescription(DescriptionToDisplay);
            }
            else
            {
                gameObject.SetActive(false);
                MalwareMinigame.Instance.CloseUninstallApp(AppParent);
            }
        }
    }

    private void OnUninstall()
    {
        uninstallLoading.gameObject.SetActive(true);
        uninstallLoading.GetAppDescription(DescriptionToDisplay);
    }

    private void onCloseUI()
    {
        gameObject.SetActive(false);
    }

    public void SetAppDescription(MalwareDescriptionSO description)
    {
        DescriptionToDisplay = description;
    }
}
