using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

namespace BetterCodePreview
{
    [CustomEditor(typeof(MonoScript))]
    public class CustomMonoScriptEditor : Editor
    {
        private string code;
        private Dictionary<string,string> memberTypes = new Dictionary<string,string>();
        private Dictionary<string,string> identifiers = new Dictionary<string,string>();
        MonoScript script;
         
       
        private readonly Regex memberTypeRegex = new Regex(@"\b(int|float|string|bool)\b");
        private readonly Regex identifierRegex = new Regex(@"\b([A-Za-z_][A-Za-z0-9_]*)\b");
        private void OnEnable()
        {
            script = (MonoScript)target;
            code = script.text;
 
            string[] lines = code.Split(new[] { '\n' }, StringSplitOptions.None);
            for (int i = 0; i < lines.Length; i++)
            {
                lines[i] = ProcessLine(lines[i]);
            }
 
            code = string.Join("\n", lines);
        }

        private string ProcessLine(string line)
        {
            // Tách comment ra khỏi chuỗi
            string comment = "";
            int commentIndex = line.IndexOf("//");
            if (commentIndex != -1)
            {
                // Nếu có comment, tách ra
                comment = line.Substring(commentIndex);
                line = line.Substring(0, commentIndex); // Lấy phần mã trước comment
            }

            var formattedLine = new System.Text.StringBuilder();

            // Thêm khoảng trắng và thụt lề
            formattedLine.Append(line.Substring(0, Math.Max(0, line.Length - line.TrimStart().Length)));

            // Thêm từ khóa đã định dạng
            string[] words = line.Split(new char[] { ' ', '\t' }, StringSplitOptions.None);
            for (int i = 0; i < words.Length; i++)
            {
                string word = words[i];

                if (Languages.CSharp.keywords.Contains(word))
                {
                    formattedLine.Append(AddTag(word, StyleDefaut.KeyWordsDefaultStyle) + " ");
                    continue;
                }

                if (memberTypeRegex.IsMatch(word))
                {
                    if (!memberTypes.ContainsKey(word))
                    {
                        memberTypes[word] = word;
                    }
                    formattedLine.Append(AddTag(word, StyleDefaut.MembDefaultStyle) + " ");
                }

                if (identifierRegex.IsMatch(word))
                {
                    if (!identifiers.ContainsKey(word))
                    {
                       identifiers[word] = word;
                    }
                    formattedLine.Append(AddTag(word, StyleDefaut.IdentifiersDefaultStyle) + " ");
                    continue;
                }

                if (Regex.IsMatch(word, "^\".*\"$"))
                {
                    formattedLine.Append(AddTag(word, StyleDefaut.StringDefaultStyle) + " ");
                    continue;
                }

                if (word == "=" || word == "+" || word == "-" || word == "*" || word == "/")
                {
                    formattedLine.Append(AddTag(word, StyleDefaut.SpecialCharDefaultStyle) + " ");
                    continue;
                }

                // Nếu không có định dạng, thêm từ gốc
                formattedLine.Append(word + " ");
            }

            if (!string.IsNullOrEmpty(comment))
            {
                comment = AddTag(comment.TrimStart(), StyleDefaut.CommentStyle);
            }

            // Kết hợp lại phần mã đã định dạng với comment
            return formattedLine.ToString().TrimEnd() + comment; // Trả về mã đã định dạng và comment đã định dạng
        }







        public override void OnInspectorGUI()
        {
            
            GUIStyle richTextStyle = new GUIStyle(GUI.skin.textArea);
            richTextStyle.richText = true;

            GUILayout.Label($"Preview for script: {script.name}");
            
            
            EditorGUILayout.TextArea(code,richTextStyle);
            

            base.OnInspectorGUI();
        }
        private string AddTag(string value, UseStyle style)
        {
            if (style == null) return value;

            string startTag = String.Format("<size={0}><color=#{1}>",style.size,style.color.ToHexString());
            string endTag = "</color></size>";

            if (style.isBold) { startTag = "<b>" + startTag; endTag += "</b>"; }
            if (style.isItalic) { startTag = "<i>" + startTag; endTag += "</i>"; }
            if (style.isStrikeThrough) { startTag = "<s>" + startTag; endTag += "</s>"; }
            if (style.isMark) { startTag = "<u>" + startTag; endTag += "</u>"; }
            return $"{startTag}{value}{endTag}";
        }
    }
}
