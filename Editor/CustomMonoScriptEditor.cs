using System;
using System.Collections.Generic;
using BetterCodePreview;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

namespace BetterCodePreview
{
    [CustomEditor(typeof(MonoScript))]
    public class CustomMonoScriptEditor : Editor
    {
        private string text;
        private Dictionary<(int,int),string> getSpaceLocal = new Dictionary<(int,int),string>();
        MonoScript script;
        private void OnEnable()
        {
            script = (MonoScript)target;
            text = script.text;

            string scriptContent = text;
            string[] wordsByLine = scriptContent.Split('\n');

            for (int i = 0; i < wordsByLine.Length; i++)
            {
                string sum = "";
                string finalRs = "";
                char[] charArr = wordsByLine[i].ToCharArray();
                int start = 0;
                int end = 0;

                for (int j = 0; j < charArr.Length; j++)
                {
                    if (j + 1 < charArr.Length)
                    {
                        string test = charArr[j].ToString() + charArr[j + 1].ToString();
                        if (test == "//")
                        {
                            break;
                        }
                    }

                    char charText = charArr[j];

                    if (charText == ' ')
                    {
                        if (start != 0)
                        {
                            // Khi gặp khoảng trắng và đã bắt đầu một từ
                            finalRs += ProcessWord(sum); // Xử lý từ hiện tại

                            // Reset
                            sum = "";
                            start = 0;
                            end = 0;
                        }
                        finalRs += ' ';
                    }
                    else
                    {
                        // Khi gặp ký tự không phải là khoảng trắng
                        if (start == 0)
                        {
                            start = j; // Đánh dấu bắt đầu từ
                        }
                        end = j;
                        sum += charText;
                    }

                    // Nếu là ký tự cuối cùng của dòng, xử lý từ cuối cùng
                    if (j == charArr.Length - 1 && !String.IsNullOrEmpty(sum))
                    {
                        finalRs += ProcessWord(sum);
                    }
                }

                wordsByLine[i] = finalRs; 
            }

            scriptContent = string.Join("\n", wordsByLine);
            text = scriptContent;
        }
        string ProcessWord(string word)
        {
            string result = word;
            if (Languages.CSharp.keywords.Contains(word))
            {
                result = AddTag(word, StyleDefaut.KeyWordsDeaultStyle);
            }
            else if (Languages.CSharp.contextualKeywords.Contains(word))
            {
                result = AddTag(word, StyleDefaut.ContextKeywordsDeaultStyle);
            }
            return result;
        }
        public override void OnInspectorGUI()
        {
            
            GUIStyle richTextStyle = new GUIStyle(GUI.skin.textArea);
            richTextStyle.richText = true;

            // Tuỳ chỉnh hiển thị code preview
            GUILayout.Label($"Preview for script: {script.name}");
            
            
            EditorGUILayout.TextArea(text,richTextStyle);

            base.OnInspectorGUI();
        }
        string[] SplitWords(string input)
        {
            char[] delimiters = new char[] { ' ', ',', '.', '?', '!', ';', ':', '\n', '\t' };
            return input.Split(delimiters, System.StringSplitOptions.RemoveEmptyEntries);
        }
        string AddTag(string value,UseStyle style)
        {
            if(style == null)
            {
                return value;
            }
            string sum;
            string startText = "";
            string endText = "";

            startText += String.Format("<size={0}>",style.size);
            endText = "</size>" + endText;

            if(style.color != null)
            {
                startText += String.Format("<color=#{0}>",style.color.ToHexString());
                endText = "</color>" + endText;
            }
            if(style.isBold)
            {
                startText += "<b>";
                endText = "</b>" + endText;
            }
            if(style.isItalic)
            {
                startText += "<i>";
                endText = "</i>" + endText;
            }
            if(style.isStrikeThrough)
            {
                startText += "<s>";
                endText = "</s>" + endText;
            }
            if(style.isMark)
            {
                startText += "<u>";
                endText = "</u>" + endText;
            }
            
            sum = string.Concat(startText, value, endText);
            Debug.Log(sum);
            return sum;
        }
    }
}
