 using System.Collections;
using System.Collections.Generic;
using UGS;
using UnityEngine;

public class LocalizationManager : MonoBehaviour
{
    private bool isLoaded = false;
    public enum Language
    {
        English,
        Korean
    }
    static LocalizationManager instance;
    public static LocalizationManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<LocalizationManager>();
            }
            return instance;
        }
    }

    /// <summary>
    /// current language
    /// </summary>
    public Language currentLanguage;
     
    void Awake()
    {
        UnityGoogleSheet.LoadAllData();
    }
 
    public void ChangeLanguage(Language lang)
    {
        this.currentLanguage = lang;
    }
    
    public string GetSunnyName(string itemID)
    {
        var localeMap = Localization.Name.NameMap;
        if (currentLanguage == Language.English)
            return localeMap[itemID].English;
        else if (currentLanguage == Language.Korean)
            return localeMap[itemID].Korean;

        return null;
    } 
}
