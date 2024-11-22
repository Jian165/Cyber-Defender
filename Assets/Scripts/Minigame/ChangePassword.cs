using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ChangePassword : Minigame
{
    
    [SerializeField] private TMP_InputField oldPassword;
    [SerializeField] private TMP_InputField newPassword;
    [SerializeField] private TMP_InputField confirmPassword;

    [SerializeField] private Button changesPasswordButton;

    [SerializeField] private TextMeshProUGUI promptText;

    public const string OLD_PASSWORD = "Admin"; 
    private void Start() {
        changesPasswordButton.onClick.AddListener(OnChange_Password);
        newPassword.contentType = TMP_InputField.ContentType.Password;
        newPassword.onValueChanged.AddListener(OnUserNewPassword);
        confirmPassword.contentType = TMP_InputField.ContentType.Password;
        
    }

    private void OnChange_Password()
    {
        if (!OLD_PASSWORD.Equals(oldPassword.text))
        {
            promptText.text = "Your current password is not exsisting";
        }

        else if (OLD_PASSWORD.Equals(newPassword.text))
        {
            promptText.text = "Your using your old password";
        }

        else if (!newPassword.text.Equals(confirmPassword.text))
        {
            promptText.text = "Your confirm password dosen't match";
        }

        else
        {
            promptText.color = Color.green;
            promptText.text = "Change Success";
        }
    }


    private void OnUserNewPassword(string arg0)
    {
        promptText.text = EvaluatePasswordStringth(newPassword.text);
    }


    private string EvaluatePasswordStringth(string password)
    { 
        int passwordLenth = password.Length;
        int characterSetSize = GetCharacterSetSize(password);
        double combination = Math.Pow(characterSetSize, passwordLenth);

        const double guessPerSecond = 1e9;
        double timeToCrackInSecods =  combination/ guessPerSecond;

        try
        {
            TimeSpan timeSpan = TimeSpan.FromSeconds(timeToCrackInSecods);
            string timeToCrack = FormatTimeSpan(timeSpan);
            return $"Your password has {combination:N0} combinations.\nEstinatied time to crack: {timeToCrack} ";
        }
        catch (OverflowException ex)
        { 
            
            return $"Your password is impossible to crack";
        }


    }

    private string FormatTimeSpan(TimeSpan timeSpan)
    {
       
            if (timeSpan.TotalSeconds < 1)
            {
                return "less than a second";
            }

            if (timeSpan.TotalMinutes < 1)
            {
                return $"{Math.Floor(timeSpan.TotalSeconds)} seconds";
            }

            if (timeSpan.TotalHours < 1)
            {
                int minutes = (int)timeSpan.TotalMinutes;
                int seconds = (int)(timeSpan.TotalSeconds % 60); // Remainder of seconds
                return $"{minutes} minutes : {seconds} seconds";
            }

            if (timeSpan.TotalDays < 1)
            {
                int hours = (int)timeSpan.TotalHours;
                int minutes = (int)(timeSpan.TotalMinutes % 60); // Remainder of minutes
                return $"{hours} hours : {minutes} minutes";
            }

            if (timeSpan.TotalDays < 365)
            {
                int days = (int)timeSpan.TotalDays;
                int hours = (int)(timeSpan.TotalHours % 24); // Remainder of hours
                return $"{days} days : {hours} hours";
            }

            int years = (int)(timeSpan.TotalDays / 365);
            int remainingDays = (int)(timeSpan.TotalDays % 365);
            return $"{years} years : {remainingDays} days";

       
    }

    private int GetCharacterSetSize(string password)
    { 
        bool hasLower = false;
        bool hasUpper = false;
        bool hasDigit = false;
        bool hasSpecial = false;

        int size = 0;

        foreach (char c in password)
        {
            if (char.IsLower(c))
            {
                hasLower = true;
            }
            else if (char.IsUpper(c))
            {
                hasUpper = true;
            }

            else if (char.IsDigit(c))
            {
                hasDigit = true;
            }
            else
            { 
                hasSpecial = true;
            }
        }

        if (hasLower)
        {
            size += 26;
        }

        if (hasUpper)
        {
            size += 26;
        }

        if (hasDigit)
        {
            size += 10;
        }

        if (hasSpecial)
        {
            size += 32;
        }

        return size;

    }
}
