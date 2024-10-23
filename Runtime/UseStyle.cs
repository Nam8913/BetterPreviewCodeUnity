using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    public static UseStyle KeyWordsDeaultStyle = new UseStyle("Dark-KeywordsStyle",ColorUTils.FromHex("79c0ff"),12);
    public static UseStyle ContextKeywordsDeaultStyle = new UseStyle("Dark-ContextKeywordsStyle",ColorUTils.FromHex("79c0ff"),12);
}