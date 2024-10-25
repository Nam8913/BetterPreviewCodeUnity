using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;
namespace BetterCodePreview
{
    public class IconDictionaryCreator : AssetPostprocessor
    {
        private const string AssetsPath = "Editor/CustomFoulderIcon/Icons";
        internal static Dictionary<string, Texture> IconDictionary; 

        internal static void BuildDictionary()
        {
            var dictionary = new Dictionary<string, Texture>();

            var dir = new DirectoryInfo(Application.dataPath + "/" + AssetsPath);
            FileInfo[] info = dir.GetFiles("*.png");
            foreach(FileInfo f in info)
            {
                var texture = (Texture)AssetDatabase.LoadAssetAtPath($"Assets/Editor/CustomFoulderIcon/Icons/{f.Name}", typeof(Texture2D));
                dictionary.Add(Path.GetFileNameWithoutExtension(f.Name),texture);
            }

            FileInfo[] infoSO = dir.GetFiles("*.asset");
            foreach (FileInfo f in infoSO) 
            {
                var folderIconSO = (FolderIconSO)AssetDatabase.LoadAssetAtPath($"Assets/Editor/CustomFoulderIcon/Icons/{f.Name}", typeof(FolderIconSO));

                if (folderIconSO != null) 
                {
                    var texture = (Texture)folderIconSO.icon;

                    foreach (string folderName in folderIconSO.folderNames) 
                    {
                        if (folderName != null) 
                        {
                            dictionary.TryAdd(folderName, texture);
                        }
                    }
                }
            }
            
            IconDictionary = dictionary;
        }
    }
}
