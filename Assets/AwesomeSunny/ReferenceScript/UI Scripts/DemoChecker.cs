
using System.Collections;
using System.Collections.Generic;
using UGS;
using UGS.Editor;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;

public class DemoChecker : MonoBehaviour, IPointerClickHandler
{
    TMPro.TextMeshProUGUI text;
    public bool clickable = false;
    public int checkType = 0;
    public bool valid = false;
    void Start()
    {

    }

    private void Valid()
    {
        this.text = GetComponent<TMPro.TextMeshProUGUI>();
        if (checkType == 0)
        {
            if (string.IsNullOrEmpty(UGSettingObjectWrapper.ScriptURL) || string.IsNullOrEmpty(UGSettingObjectWrapper.ScriptPassword))
            {
                text.text = "<color=#FFCB00>Missing<u><color=#0099FF>(Click)</color></u></color>";
                valid = false;
            }
            else
            {
                text.text = "<color=green>OK</color>";
                valid = true;
            }
        }
        if (checkType == 1)
        {
            if (string.IsNullOrEmpty(UGSettingObjectWrapper.GoogleFolderID))
            {
                text.text = "<color=#FFCB00>Copy Sunnyland Sheets<u><color=#0099FF>(Click)</color></u></color>";
                valid = false;
            }
            else
            {
                text.text = "<color=green>GoogleFolder ID Settinged!</color>";
                valid = true;
            }
        }
        if (checkType == 2)
        {
            var id = GoogleSheet.Reflection.TableUtils.GetSpreadSheetID(typeof(SunnyLand.UnitEntity));
            if (id == "1OpwchmwiL6nv8mznRbdUKMPOIJo0jG3Fy2PcKSd5qvo") // it's default table
            {
                text.text = "<color=#FFCB00>Generate Your Own Data!<u><color=#0099FF>(Click)</color></u></color>";
                valid = false;
            }
            else
            {
                text.text = "<color=green>OK</color>";
                valid = true;
            }
        }
    }
    // Update is called once per frame
    void Update()
    {
        Valid();
    }

    public void Solution()
    {
        if (checkType == 0)
        {
            Application.OpenURL("https://shlifedev.gitbook.io/unitygooglesheet/getting-start/apps-script");
        }
        if (checkType == 1)
        {
            Debug.Log("Solution 1");
            UnityGoogleSheet.CopyFolder("1rnmapRqCeAuLYOZL8v3ssuIs6_UXpve0", result =>
            {

                var res = EditorUtility.DisplayDialog("Google Sheet Copied!", "Complate Copy SunnyLand", "OK");
                if (res)
                {
                    UGSettingObjectWrapper.GoogleFolderID = result;
                     
                    EditorPrefsManager.Set<string>("UGSetting.ScriptPassword", UGSettingObjectWrapper.ScriptPassword);
                    EditorPrefsManager.Set<string>("UGSetting.GoogleFolderID", UGSettingObjectWrapper.GoogleFolderID);
                    EditorPrefsManager.Set<string>("UGSetting.ScriptURL", UGSettingObjectWrapper.ScriptURL); 
                }

            });
        }

        if (checkType == 2)
        {
            if (Application.isPlaying)
            {
                EditorUtility.DisplayDialog("Warning", "Exit Playmode and Generate Your own data!", "OK");
                Application.Quit(0);
                GoogleDriveExplorerGUI.Init();
            }
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (!valid)
        {
            Solution();
        }
    }
}
