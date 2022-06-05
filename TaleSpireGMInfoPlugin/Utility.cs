using BepInEx;
using System;
using System.Reflection;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace LordAshes
{
    public static class Utility
    {
        public static void Initialize(System.Reflection.MemberInfo plugin)
        {
            SceneManager.sceneLoaded += (scene, mode) =>
            {
                try
                {
                    if (scene.name == "UI")
                    {
                        TextMeshProUGUI betaText = GetUITextByName("BETA");
                        if (betaText)
                        {
                            betaText.text = "INJECTED BUILD - unstable mods";
                        }
                    }
                    else
                    {
                        TextMeshProUGUI modListText = GetUITextByName("TextMeshPro Text");
                        if (modListText)
                        {
                            BepInPlugin bepInPlugin = (BepInPlugin)Attribute.GetCustomAttribute(plugin, typeof(BepInPlugin));
                            if (modListText.text.EndsWith("</size>"))
                            {
                                modListText.text += "\n\nMods Currently Installed:\n";
                            }
                            modListText.text += "\nLord Ashes' " + bepInPlugin.Name + " - " + bepInPlugin.Version;
                        }
                    }
                }
                catch (Exception ex)
                {
                    Debug.Log(ex);
                }
            };
        }

        public static TextMeshProUGUI GetUITextByName(string name)
        {
            TextMeshProUGUI[] texts = UnityEngine.Object.FindObjectsOfType<TextMeshProUGUI>();
            for (int i = 0; i < texts.Length; i++)
            {
                if (texts[i].name == name)
                {
                    return texts[i];
                }
            }
            return null;
        }

        /// <summary>
        /// Method to obtain the Asset Loader Game Object based on a CreatureGuid
        /// </summary>
        /// <param name="cid">Creature Guid</param>
        /// <returns>AssetLoader Game Object</returns>
        public static GameObject GetRootObject(CreatureGuid cid)
        {
            CreatureBoardAsset asset = null;
            CreaturePresenter.TryGetAsset(cid, out asset);
            if (asset != null)
            {
                Type cba = typeof(CreatureBoardAsset);
                foreach (FieldInfo fi in cba.GetRuntimeFields())
                {
                    if (fi.Name == "_creatureRoot")
                    {
                        Transform obj = (Transform)fi.GetValue(asset);
                        return obj.gameObject;
                    }
                }
            }
            return null;
        }

        /// <summary>
        /// Method to obtain the Base Loader Game Object based on a CreatureGuid
        /// </summary>
        /// <param name="cid">Creature Guid</param>
        /// <returns>BaseLoader Game Object</returns>
        public static GameObject GetBaseObject(CreatureGuid cid)
        {
            CreatureBoardAsset asset = null;
            CreaturePresenter.TryGetAsset(cid, out asset);
            if (asset != null)
            {
                Type cba = typeof(CreatureBoardAsset);
                foreach (FieldInfo fi in cba.GetRuntimeFields())
                {
                    if (fi.Name == "_base")
                    {
                        CreatureBase obj = (CreatureBase)fi.GetValue(asset);
                        return obj.transform.GetChild(0).gameObject;
                    }
                }
            }
            return null;
        }

        /// <summary>
        /// Method to obtain the Asset Loader Game Object based on a CreatureGuid
        /// </summary>
        /// <param name="cid">Creature Guid</param>
        /// <returns>AssetLoader Game Object</returns>
        public static GameObject GetAssetObject(CreatureGuid cid)
        {
            CreatureBoardAsset asset = null;
            CreaturePresenter.TryGetAsset(cid, out asset);
            if (asset != null)
            {
                Type cba = typeof(CreatureBoardAsset);
                foreach (FieldInfo fi in cba.GetRuntimeFields())
                {
                    if (fi.Name == "_creatureRoot")
                    {
                        Transform obj = (Transform)fi.GetValue(asset);
                        return obj.GetChild(0).GetChild(2).GetChild(0).gameObject;
                    }
                }
            }
            return null;
        }
    }
}
