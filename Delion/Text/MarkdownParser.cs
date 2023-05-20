using Delion.Utilities;
using OpenTK.Mathematics;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.MemoryExtensions;

namespace Delion.Text
{
    /// <summary>
    /// Info about glyph modificators
    /// </summary>
    public struct MarkdownSymbolData
    {
        public char symbol;
        public Vector4b? Color;
        public FontMods Style;
    }

    /// <summary>
    /// parses markdowned string for TextRenderer.Parse is the main method to call to obtain MarkdownGlyphData array
    /// Tags:
    /// |#00eeaaFF| - |!#00eeaaFF|     | color
    /// |italic| - |!italic|           | italic
    /// |bold| - |!bold|               | bold
    /// </summary>
    public class MarkdownParser
    {
        public const int HexLength = 9;

        /// <summary>
        /// Translates #A1FF53FF to Vector4b bytes
        /// Returns true if succeded otherwise false
        /// </summary>
        public static bool HexToVector4b(ReadOnlySpan<char> hex, out Vector4b parsed)
        {
            if (hex.Length != 9 && hex[0] != '#')
            {
                parsed = new Vector4b();
                return false;
            }

            Vector4b color = new Vector4b();

            bool resR = byte.TryParse(hex.Slice(1, 2),NumberStyles.HexNumber, null, out color.R);
            bool resG = byte.TryParse(hex.Slice(3, 2), NumberStyles.HexNumber, null, out color.G);
            bool resB = byte.TryParse(hex.Slice(5, 2), NumberStyles.HexNumber, null, out color.B);
            bool resA = byte.TryParse(hex.Slice(7, 2), NumberStyles.HexNumber, null, out color.A);

            if(!resR || !resG || !resB || !resA)
            {
                parsed = new Vector4b();
                return false;
            }

            parsed = color;
            return true;

        }

        /// <summary>
        /// Returns an Memory of MarkdownGlyphData where each struct describes options of the char it is attached to
        /// Length of the Memory equals to the given string without tags;
        /// Parsing algorythm works like a stack.Open-tags are added to the stack, close-tags remove complement tags from the stack
        /// Parser iterates through all chars and set options to glyph according to current tags in the stack
        /// </summary>
        public Memory<MarkdownSymbolData> Parse(ReadOnlySpan<char> str)
        {
            MarkdownSymbolData[] data = new MarkdownSymbolData[str.Length];
            Stack<object> currentTags = new Stack<object>();

            int i = 0;
            int payloadCharSize = 0;
            while(i < str.Length)
            {
                if(str[i] == '|')
                {

                    if(str[i + 1] == '!')
                    {
                        i += 2;

                        if (MemoryExtensions.Equals(str[i..(i + 6)],"italic".AsSpan(), StringComparison.Ordinal))
                        {
                            if(currentTags.Peek().GetType() == typeof(FontMods) && 
                                (FontMods)currentTags.Peek() == FontMods.Italic)
                            {
                                currentTags.Pop();
                            }
                            i += 6 + 1;
                            continue;
                        }
                        else if (MemoryExtensions.Equals(str[i..(i + 4)], "bold".AsSpan(), StringComparison.Ordinal))
                        {
                            if (currentTags.Peek().GetType() == typeof(FontMods) &&
                                (FontMods)currentTags.Peek() == FontMods.Bold)
                            {
                                currentTags.Pop();
                            }
                            i += 4 + 1;
                            continue;
                        }
                        else if (HexToVector4b(str[i..(i + 9)], out Vector4b parsed))
                        {
                            if (currentTags.Peek().GetType() == typeof(Vector4b) &&
                                (Vector4b)currentTags.Peek() == parsed)
                            {
                                currentTags.Pop();
                            }
                            i += 9 + 1;
                            continue;
                        }

                        i -= 2;
                
                    }
                    else
                    {
                        i += 1;
                        var meme = str[i..(i + 6)];

                        if (MemoryExtensions.Equals(str[i..(i + 6)], "italic".AsSpan(), StringComparison.Ordinal))
                        {
                            currentTags.Push(FontMods.Italic);
                            i += 6 + 1;
                            continue;
                        }
                        else if (MemoryExtensions.Equals(str[i..(i + 4)], "bold".AsSpan(), StringComparison.Ordinal))
                        {
                            currentTags.Push(FontMods.Bold);
                            i += 4 + 1;
                            continue;
                        }
                        else if (HexToVector4b(str[i..(i + 9)], out Vector4b parsed))
                        {
                            currentTags.Push(parsed);
                            i += 9 + 1;
                            continue;
                        }

                        i -= 1;

                    }

                }

                Vector4b? color = null;
                FontMods modTag = 0;
                bool isItalic = false, isBold = false;

                data[payloadCharSize].symbol = str[i];

                foreach (object tag in currentTags)
                {
                    if (tag.GetType() == typeof(Vector4b))
                    {
                        color = color == null ? (Vector4b)tag : color;
                    }
                    else if(tag.GetType() == typeof(FontMods))
                    {
                        modTag = (FontMods)tag;

                        isItalic = modTag == FontMods.Italic ? true : isItalic;
                        isBold = modTag == FontMods.Bold ? true : isBold;
                    }
                }

                data[payloadCharSize].Color = color;

                if (isItalic && isBold)
                    data[payloadCharSize].Style = FontMods.BoldItalic;
                else if (isItalic)
                    data[payloadCharSize].Style = FontMods.Italic;
                else if (isBold)
                    data[payloadCharSize].Style = FontMods.Bold;
                else
                    data[payloadCharSize].Style = FontMods.Usual;

                i++;
                payloadCharSize++;

            }

            return new Memory<MarkdownSymbolData>(data, 0, payloadCharSize);
        }
    }
}
