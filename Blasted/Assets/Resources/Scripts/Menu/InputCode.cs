using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InputCode : Menu
{
    bool _isActive = false;
    bool _antiSpam = true;
    public static string texte = "";

    public InputField inputText;

    public override void Select()
    {
        _isActive = !_isActive;
        MainMenu.EditChange?.Invoke(_isActive);
    }

    void SpecialKey(KeyCode key)
    {
        Debug.Log(key);

        if(key == KeyCode.Backspace)
        {
            texte = texte.Substring(0, texte.Length - 1);
        }

        UpdateInput();
    }

    void TextKey(KeyCode key)
    {
        if (texte.Length == 5) return;
        texte += key.ToString().ToUpper()[0];
        UpdateInput();
    }

    void UpdateInput()
    {
        inputText.text = texte;
    }

    private void Update()
    {
        if (_isActive)
        {
            // Find Key
            KeyCode keypressed = new KeyCode();
            foreach (KeyCode key in System.Enum.GetValues(typeof(KeyCode)))
            {
                if (Input.GetKey(key))
                {
                    keypressed = key;
                }
            }

            // Analyse Key
            if(keypressed != KeyCode.None && !_antiSpam)
            {
                _antiSpam = true;

                if (keypressed.ToString().Length > 1)
                {
                    SpecialKey(keypressed);
                    return;
                }

                int ascii = (int)keypressed.ToString().ToUpper()[0];
                if (ascii > 64 && ascii < 91)
                {
                    TextKey(keypressed);
                }

            }

            // Antispam
            if(keypressed == KeyCode.None) _antiSpam = false;

        }
    }
}
