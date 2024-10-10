using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Autofill : MonoBehaviour
{
    private TMP_InputField emailInputField; // Variable for TMP_InputField
    private TMP_Text emailText; // Variable for TMP_Text

    public void autofill_details()
    {
        // Don't autofill if there is no stored email
        string savedEmail = PlayerPrefs.GetString("Email");
        if (!PlayerPrefs.HasKey("Email") || string.IsNullOrEmpty(savedEmail))
        {
            return;
        }

        // First identify the GameObject with the InputField, and check that its available
        GameObject field = null;
        string[] possible_tags = {"SendOTPForEmailVerification", "EmailForPwReset", "EmailForOTPPwReset", "AccSettingsEmail"};

        // Check all possible tags to find the email input field
        for(int i = 0 ; i < possible_tags.Length ; i++)
        {
            if(field != null)
            {
                break;
            }
            field = GameObject.FindWithTag(possible_tags[i]);
        }

        // If there are no tags, return
        if(field == null)
        {
            return;
        }

        // Try to get the TMP_InputField
        emailInputField = field.GetComponent<TMP_InputField>();
        if (emailInputField != null)
        {
            // Set the input field text if found
            emailInputField.text = savedEmail;
        }
        else
        {
            // If it's not a TMP_InputField, check for TMP_Text
            emailText = field.GetComponent<TMP_Text>();
            if (emailText != null)
            {
                // Set the text if found
                emailText.text = savedEmail;
            }
        }
    }

    private void OnEnable()
    {
        autofill_details();
    }
}