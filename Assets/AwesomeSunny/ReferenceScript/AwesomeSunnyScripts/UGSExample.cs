using System.Collections;
using System.Collections.Generic;
using UGS;
using UnityEngine;
using static LocalizationManager;

public class UGSExample : MonoBehaviour
{ 
    public void LiveLoad()
    { 
        StateText.Instance.UpdateText("Reload Localization..", 2); 
        LiveLoadLocalization(() => {
            LiveLoadUnitData();
        });
    }

    public void ChangeLanguage(Language lang)
    {
        LocalizationManager.Instance.ChangeLanguage(lang);
        var entities = FindObjectsOfType<Entity>();
        foreach(var entity in entities) 
            entity.ReloadLocalization(); 
    }

    public void ChangeLangToEN()
    {
        ChangeLanguage(Language.English);
    }

    public void ChangeLangToKR()
    {
        ChangeLanguage(Language.Korean);
    }

    public void LiveLoadLocalization(System.Action nextDoCallback = null)
    {
        //First Load
        UnityGoogleSheet.LoadFromGoogle<string, Localization.Name>((list, dictionary) =>
        {
            list.ForEach(x => {
                Debug.Log($"{x.English} {x.Korean}");
            });
            nextDoCallback?.Invoke(); 
        }, true);
    }
    public void LiveLoadUnitData(System.Action callback = null )
    { 
        StateText.Instance.UpdateText("Reload UnitData..", 2);
        UnityGoogleSheet.LoadFromGoogle<int, SunnyLand.UnitEntity>((list, dictionary) =>
        {
            var entities = FindObjectsOfType<Entity>();
            StateText.Instance.UpdateText("Updated!", 2); 
            foreach (var entity in entities) 
                    entity.Reload();  
            callback?.Invoke();
        }, true);
    }

    public void Awake()
    {
        UnityGoogleSheet.LoadAllData();
    }
}
