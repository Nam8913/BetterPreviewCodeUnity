using System.Collections.Generic;
using UnityEngine; 
using UnityEditor;
using System.IO; 
namespace BetterCodePreview
{
    [CustomEditor(typeof(DefaultAsset))]
    public class FolderInspectorEditor : Editor
    {
        private Vector2 scrollPosition;
        public Dictionary<string,List<string>> keyValuePairs = new Dictionary<string,List<string>>();
        public bool ShowChildFileAndFolder;
        string path;
        private void OnEnable()
        {
            path = AssetDatabase.GetAssetPath(target);
        }
        public override void OnInspectorGUI()
        {
            
        
            if (AssetDatabase.IsValidFolder(path))
            {
                GUI.enabled = true;

                GUILayout.Label("Folder Name: " + target.name, EditorStyles.boldLabel);
                GUILayout.Label("Path: " + path);

                scrollPosition = EditorGUILayout.BeginScrollView(scrollPosition);
                // Nếu muốn, bạn có thể hiển thị thông tin các tệp trong thư mục
                string[] files = System.IO.Directory.GetFiles(path);
                if(files.Length == 0) return;
                GUILayout.Label("Files in folder:");
                foreach (string file in files)
                {
                    FileInfo fileInfo = new FileInfo(file);
                    if(fileInfo.Extension == ".meta")
                    {
                        continue;
                    }
                    if(!keyValuePairs.ContainsKey(fileInfo.Extension))
                    {
                        keyValuePairs.Add(fileInfo.Extension,new List<string>(){file});
                        GUILayout.Label(file);
                    }else
                    {
                        keyValuePairs[fileInfo.Extension].Add(file);
                        GUILayout.Label(file);
                    }
                
                }
                GUILayout.Space(50);
                Rect chartRect = GUILayoutUtility.GetRect(100, 100);
                float total = 0;

                foreach(var pair in keyValuePairs)
                {
                    total += pair.Value.Count;
                }
                float startAngle = 0f;
                int cout = 0;
                Dictionary<string,float> data = new Dictionary<string,float>();
                foreach(var pair in keyValuePairs)
                {
                    // Tính toán phần trăm của từng phần tử
                    float Percent = pair.Value.Count / total;
                    float angle = Percent * 360f;

                    data.Add(pair.Key, Percent);

                    // Vẽ từng phần của biểu đồ tròn
                    Handles.color = GetColor(cout);
                    Handles.DrawSolidArc(
                        chartRect.center,          // Tâm của biểu đồ
                        Vector3.forward,           // Hướng vẽ
                        Quaternion.Euler(0, 0, startAngle) * Vector3.up, // Điểm bắt đầu vẽ
                        angle,                     // Góc của phần quạt
                        chartRect.width / 4f       // Bán kính của biểu đồ
                    ); 
                    // Cập nhật góc bắt đầu cho phần tiếp theo
                    startAngle += angle;
                    cout++;
                }
                GUILayout.Space(80);
                Rect barRect = GUILayoutUtility.GetRect(200, 8);
                float currentX = barRect.x;
                cout = 0;
                foreach (var item in data)
                {
                    
                    // Tính toán phần trăm của từng phần tử
                    float percentage = item.Value;
                    float width = barRect.width * percentage;

                    // Vẽ thanh màu cho phần hiện tại
                    Rect segmentRect = new Rect(currentX, barRect.y, width, barRect.height);
                    EditorGUI.DrawRect(segmentRect, GetColor(cout));
    
                    currentX += width;
                    cout++;
                }
                
                GUILayout.Space(5);
                cout = 0;
                int size = 10;
                foreach (var item in data)
                {
                    
                    EditorGUILayout.BeginHorizontal();
                    //EditorGUILayout.ColorField(GetColor(cout), GUILayout.Width(20)); // Hiển thị ô màu
                    Rect colorRect = GUILayoutUtility.GetRect(size, size, GUILayout.ExpandWidth(false), GUILayout.ExpandHeight(false));
                    colorRect.y += size/2;
                    EditorGUI.DrawRect(colorRect,GetColor(cout));
                    GUILayout.Label($"{item.Key} {(item.Value/1)*100}%");
                    EditorGUILayout.EndHorizontal();
                    cout++;
                } 
                EditorGUILayout.EndScrollView();
                GUI.enabled = false;
            }
            else
            {
                // Nếu không phải thư mục, hiển thị Inspector mặc định
                DrawDefaultInspector();
            }
            
        }
        private Color GetColor(int index)
        {
            Color[] colors = new Color[] 
            { 
                Color.blue,
                Color.cyan,
                Color.gray,
                Color.green,
                Color.grey,
                Color.magenta,
                Color.red,
                Color.yellow,
                Color.red * 0.5f,
                Color.green * 0.5f, 
                Color.blue * 0.5f,
                new Color(1f, 0.5f, 0f), 
                new Color(1f, 0f, 1f), 
                new Color(0f, 1f, 1f), 
                new Color(0.5f, 0f, 0.5f)
            };
            return colors[index % colors.Length];
        }
    }
}

