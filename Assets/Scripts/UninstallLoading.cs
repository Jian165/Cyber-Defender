using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class UninstallLoading : MonoBehaviour
{

    [SerializeField] private Image loadingBar;
    [SerializeField] private TextMeshProUGUI toProcess;
    [SerializeField] private TextMeshProUGUI ApptoUnInstall;

    private  List <string> listOfProperties;

    private string appName;

    private bool isLoadingComplete = false;
    private float currentProgress = 0f;
    private float loadingSpeed = 0.01f;

    private float loadingSpeed2 = 0.2f;
    private MalwareDescriptionSO description;
    string property;

    private void Start()
    {
        listOfProperties = new List<string>();
        if (description != null)
        {
            appName = description.appName;
            if (description.isMalware)
            {
                listOfProperties.Add($"The {description.appName} is detected As Malware!");
            }
            else
            {
                listOfProperties.Add($"The {description.appName} is safe!");
            }
            listOfProperties.Add("permissions: " + description.appPermission);
            listOfProperties.Add("signature: " + description.appDigitalSgnature);
            listOfProperties.Add("resource: " + description.appRamResource);
            listOfProperties.Add("resource: " + description.appCPUResource);
            listOfProperties.Add("Size: " + description.appSize);
            listOfProperties.Add(description.appFileLocation);
            listOfProperties.Add(description.appPublisher);

        };
    }

    private void Update()
    {
        ApptoUnInstall.text = appName;
        if (!isLoadingComplete)
        {
            if (listOfProperties.Count != 0)
            {
                currentProgress += Time.deltaTime * loadingSpeed;
            }
            else
            {
                currentProgress += Time.deltaTime * loadingSpeed2;
            }

            loadingBar.fillAmount = currentProgress;


            if (currentProgress >= 1f)
            {
                isLoadingComplete = true;
            }
            else
            {
                isLoadingComplete = false;
                if (listOfProperties.Count > 0)
                {
                    property = listOfProperties[listOfProperties.Count - 1];
                    toProcess.text = property;
                    listOfProperties.Remove(property);
                }
                else if (listOfProperties.Count == 0)
                {
                    toProcess.text = property;
                }
            }
        }
    }

    public bool isLoadingFinished()
    {
        return isLoadingComplete;
    }

    public void GetAppDescription(MalwareDescriptionSO description)
    {
        this.description = description;
    }
}
