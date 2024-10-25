using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BetterCodePreview
{
    public class UseStyle
    {
        public string nameStyle;
        public Color color;
        public int size;
        public bool isBold;
        public bool isItalic;
        //public bool isUnderlined; unity not support yet;
        public bool isStrikeThrough;
        public bool isMark;
        public UseStyle(string nameStyle, Color color, int size) : this(nameStyle,color,false,false,false,false,size){}
        public UseStyle(string nameStyle, Color color, bool isBold, bool isItalic, bool isStrikeThrough, bool isMark, int size)
        {
            this.nameStyle = nameStyle;
            this.color = color;
            this.size = size;
            this.isBold = isBold;
            this.isItalic = isItalic;
            this.isStrikeThrough = isStrikeThrough;
            this.isMark = isMark;
        }
    }
    public static class StyleDefaut
    {

        public static UseStyle KeyWordsDefaultStyle = new UseStyle("Dark-KeywordsStyle",ColorUTils.FromHex("79c0ff"),12);
        public static UseStyle ContextKeywordsDefaultStyle = new UseStyle("Dark-ContextKeywordsStyle",ColorUTils.FromHex("79c0ff"),12);
        public static UseStyle SpecialCharDefaultStyle = new UseStyle("Dark-SpecialCharStyle",Color.yellow,12);
        public static UseStyle StringDefaultStyle = new UseStyle("Dark-StringDefaultStyle",ColorUTils.FromHex("ff7b72"),12);
        public static UseStyle IdentifiersDefaultStyle = new UseStyle("Dark-IdentifiersDefaultStyle",ColorUTils.FromHex("a5d6ff"),12);
        public static UseStyle MembDefaultStyle = new UseStyle("Dark-MembDefaultStyle",ColorUTils.FromHex("7ee787"),12);
        public static UseStyle CommentStyle = new UseStyle("Dark-CommentStyle",Color.green,12);
    }
}
