using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FireWallToggle : MonoBehaviour
{

    public event EventHandler<OnToggleChangeAgrs> OnToggleChange;
    private ToggleGroup toggleGroup;
    private bool isFirewallOn;

    public class OnToggleChangeAgrs : EventArgs
    { 
        public bool isFirewallOnValue;
    }

    

    public void Awake()
    {
       toggleGroup = GetComponent<ToggleGroup>();
    }

    public void Start()
    {
        foreach (Toggle toggle in toggleGroup.GetComponentsInChildren<Toggle>())
        {
            toggle.onValueChanged.AddListener(OnValyeChange);
        }
    }

    private void OnValyeChange(bool arg0)
    {
        foreach (Toggle toggle in toggleGroup.GetComponentsInChildren<Toggle>())
        {
            if (toggle.isOn)
            {
                if (toggle.name == "On")
                {
                    isFirewallOn = true;
                    OnToggleChange?.Invoke(this,new OnToggleChangeAgrs
                    {
                        isFirewallOnValue = isFirewallOn
                    });
                }
                else if (toggle.name == "Off")
                { 
                    isFirewallOn = false;
                    OnToggleChange?.Invoke(this,new OnToggleChangeAgrs
                    {
                        isFirewallOnValue = isFirewallOn
                    });
                }

                break;
            }
        }
    }
}