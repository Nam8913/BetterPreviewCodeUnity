using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BetterCodePreview
{
    public class LanguageDef
    {
        public readonly string name;
        public readonly HashSet<string> keywords;
        public readonly HashSet<string> contextualKeywords;
        public List<UseStyle> useStyles;

        public LanguageDef(string name, HashSet<string> keywords, HashSet<string> contextualKeywords)
        {
            this.name = name;
            this.keywords = keywords;
            this.contextualKeywords = contextualKeywords;
        }



        public enum TypeMemberCategory
        {
            Method,
            Property,
            Event,
            Field,
            NestedType
        }
    }
    public static class Languages
    {
        public static LanguageDef CSharp = new LanguageDef(
            "CSharp",
            new HashSet<string> 
            { 
                "abstract", "as", "base", "bool", "break", "byte", "case", "catch", 
                "char", "checked", "class", "const", "continue", "decimal", "default", 
                "delegate", "do", "double", "else", "enum", "event", "explicit", 
                "extern", "false", "finally", "fixed", "float", "for", "foreach", 
                "goto", "if", "implicit", "in", "int", "interface", "internal", 
                "is", "lock", "long", "namespace", "new", "null", "object", 
                "operator", "out", "override", "params", "private", "protected", 
                "public", "readonly", "ref", "return", "sbyte", "sealed", "short", 
                "sizeof", "stackalloc", "static", "string", "struct", "switch", 
                "this", "throw", "true", "try", "typeof", "uint", "ulong", 
                "unchecked", "unsafe", "ushort", "using", "virtual", "void", 
                "volatile", "while"
            },
            new HashSet<string> 
            { 
                "add", "alias", "async", "await", "dynamic", "from", "get", "global", 
                "group", "into", "join", "let", "orderby", "partial", "remove", "select", 
                "set", "value", "var", "where", "yield", "when", "ascending", 
                "descending", "by", "equals", "on"
            });
    }
}

