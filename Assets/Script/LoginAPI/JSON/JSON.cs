using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Globalization;
/*
 * 2013 WyrmTale Games | MIT License
 *
 * Based on   MiniJSON.cs by Calvin Rien | https://gist.github.com/darktable/1411710
 * that was Based on the JSON parser by Patrick van Bergen | http://techblog.procurios.nl/k/618/news/view/14605/14863/How-do-I-write-my-own-parser-for-JSON.html
 *
 * Extended it so it includes/returns a JSON object that can be accessed using 
 * indexers. also easy custom class to JSON object mapping by implecit and explicit asignment  overloading
 *
 * Permission is hereby granted, free of charge, to any person obtaining
 * a copy of this software and associated documentation files (the
 * "Software"), to deal in the Software without restriction, including
 * without limitation the rights to use, copy, modify, merge, publish,
 * distribute, sublicense, and/or sell copies of the Software, and to
 * permit persons to whom the Software is furnished to do so, subject to
 * the following conditions:
 *
 * The above copyright notice and this permission notice shall be
 * included in all copies or substantial portions of the Software.
 *
 * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND,
 * EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF
 * MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT.
 * IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY
 * CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT,
 * TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE
 * SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
 */

//namespace WyrmTale {

public class JSON
{

    public Dictionary<string, object> fields = new Dictionary<string, object>();

    // Indexer to provide read/write access to the fields
    public object this[string fieldName]
    {
        // Read one byte at offset index and return it.
        get
        {
            if (fields.ContainsKey(fieldName))
                return (fields[fieldName]);
            return null;
        }
        // Write one byte at offset index and return it.
        set
        {
            if (fields.ContainsKey(fieldName))
                fields[fieldName] = value;
            else
                fields.Add(fieldName, value);
        }
    }

    public string ToString(string fieldName)
    {
        if (fields.ContainsKey(fieldName))
            return System.Convert.ToString(fields[fieldName]);
        else
            return "";
    }

    public int ToInt(string fieldName)
    {
        if (fields.ContainsKey(fieldName))
        {
            try
            {
                return System.Convert.ToInt32(fields[fieldName]);
            }
            catch (Exception ex)
            {
                Debug.LogWarning(fields[fieldName].ToString() + " không thể convert to Int" + ex.ToString());
                return 0;
            }
        }
        else
            return 0;
    }

    public long ToLong(string fieldName)
    {
        if (fields.ContainsKey(fieldName))
        {
            try
            {
                return System.Convert.ToInt64(fields[fieldName]);
            }
            catch (Exception ex)
            {
                Debug.LogError(fields[fieldName].ToString() + " không thể convert to long" + ex.ToString());
                return 0;
            }
        }
        else
            return 0;
    }

    public float ToFloat(string fieldName)
    {
        if (fields.ContainsKey(fieldName))
        {
            try
            {
                return System.Convert.ToSingle(fields[fieldName]);
            }
            catch (Exception ex)
            {
                Debug.LogError(fields[fieldName].ToString() + " không thể convert to Float" + ex.ToString());
                return 0.0f;
            }
        }
        else
            return 0.0f;
    }
    public bool ToBoolean(string fieldName)
    {
        if (fields.ContainsKey(fieldName))
        {
            try
            {
                return System.Convert.ToBoolean(fields[fieldName]);
            }
            catch (Exception ex)
            {
                Debug.LogError(fields[fieldName].ToString() + " không thể convert to boolean" + ex.ToString());
                return false;
            }
        }
        else
            return false;
    }

    public string serialized
    {
        get
        {
            return _JSON.Serialize(this);
        }
        set
        {
            JSON o = _JSON.Deserialize(value);
            if (o != null)
                fields = o.fields;
        }
    }

    public JSON ToJSON(string fieldName)
    {
        try
        {
            if (!fields.ContainsKey(fieldName))
                fields.Add(fieldName, new JSON());
            return (JSON)this[fieldName];
        }
        catch
        {
            Debug.Log("parse json erro");
            return null;
        }
    }

    // to serialize/deserialize a Vector2
    public static implicit operator Vector2(JSON value)
    {
        return new Vector3(
             System.Convert.ToSingle(value["x"]),
             System.Convert.ToSingle(value["y"]));
    }
    public static explicit operator JSON(Vector2 value)
    {
        checked
        {
            JSON o = new JSON();
            o["x"] = value.x;
            o["y"] = value.y;
            return o;
        }

    }


    // to serialize/deserialize a Vector3
    public static implicit operator Vector3(JSON value)
    {
        return new Vector3(
             System.Convert.ToSingle(value["x"]),
             System.Convert.ToSingle(value["y"]),
             System.Convert.ToSingle(value["z"]));
    }

    public static explicit operator JSON(Vector3 value)
    {
        checked
        {
            JSON o = new JSON();
            o["x"] = value.x;
            o["y"] = value.y;
            o["z"] = value.z;
            return o;
        }
    }

    // to serialize/deserialize a Quaternion
    public static implicit operator Quaternion(JSON value)
    {
        return new Quaternion(
             System.Convert.ToSingle(value["x"]),
             System.Convert.ToSingle(value["y"]),
             System.Convert.ToSingle(value["z"]),
             System.Convert.ToSingle(value["w"])
             );
    }
    public static explicit operator JSON(Quaternion value)
    {
        checked
        {
            JSON o = new JSON();
            o["x"] = value.x;
            o["y"] = value.y;
            o["z"] = value.z;
            o["w"] = value.w;
            return o;
        }
    }

    // to serialize/deserialize a Color
    public static implicit operator Color(JSON value)
    {
        return new Color(
             System.Convert.ToSingle(value["r"]),
             System.Convert.ToSingle(value["g"]),
             System.Convert.ToSingle(value["b"]),
             System.Convert.ToSingle(value["a"])
             );
    }
    public static explicit operator JSON(Color value)
    {
        checked
        {
            JSON o = new JSON();
            o["r"] = value.r;
            o["g"] = value.g;
            o["b"] = value.b;
            o["a"] = value.a;
            return o;
        }
    }

    // to serialize/deserialize a Color32
    public static implicit operator Color32(JSON value)
    {
        return new Color32(
             System.Convert.ToByte(value["r"]),
             System.Convert.ToByte(value["g"]),
             System.Convert.ToByte(value["b"]),
             System.Convert.ToByte(value["a"])
             );
    }
    public static explicit operator JSON(Color32 value)
    {
        checked
        {
            JSON o = new JSON();
            o["r"] = value.r;
            o["g"] = value.g;
            o["b"] = value.b;
            o["a"] = value.a;
            return o;
        }
    }

    // to serialize/deserialize a Rect
    public static implicit operator Rect(JSON value)
    {
        return new Rect(
             System.Convert.ToByte(value["left"]),
             System.Convert.ToByte(value["top"]),
             System.Convert.ToByte(value["width"]),
             System.Convert.ToByte(value["height"])
             );
    }
    public static explicit operator JSON(Rect value)
    {
        checked
        {
            JSON o = new JSON();
            o["left"] = value.xMin;
            o["top"] = value.yMax;
            o["width"] = value.width;
            o["height"] = value.height;
            return o;
        }
    }


    // get typed array out of the object as object[] 
    public T[] ToArray<T>(string fieldName)
    {
        if (fields.ContainsKey(fieldName))
        {
            if (fields[fieldName] is IEnumerable)
            {
                List<T> l = new List<T>();
                foreach (object o in (fields[fieldName] as IEnumerable))
                {
                    if (l is List<string>)
                        (l as List<string>).Add(System.Convert.ToString(o));
                    else
                    if (l is List<int>)
                        (l as List<int>).Add(System.Convert.ToInt32(o));
                    else
                    if (l is List<float>)
                        (l as List<float>).Add(System.Convert.ToSingle(o));
                    else
                    if (l is List<bool>)
                        (l as List<bool>).Add(System.Convert.ToBoolean(o));
                    else
                    if (l is List<Vector2>)
                        (l as List<Vector2>).Add((Vector2)((JSON)o));
                    else
                    if (l is List<Vector3>)
                        (l as List<Vector3>).Add((Vector3)((JSON)o));
                    else
                    if (l is List<Rect>)
                        (l as List<Rect>).Add((Rect)((JSON)o));
                    else
                    if (l is List<Color>)
                        (l as List<Color>).Add((Color)((JSON)o));
                    else
                    if (l is List<Color32>)
                        (l as List<Color32>).Add((Color32)((JSON)o));
                    else
                    if (l is List<Quaternion>)
                        (l as List<Quaternion>).Add((Quaternion)((JSON)o));
                    else
                    if (l is List<JSON>)
                        (l as List<JSON>).Add((JSON)o);
                }
                return l.ToArray();
            }
        }
        return new T[] { };
    }



    /// <summary>
    /// This class encodes and decodes JSON strings.
    /// Spec. details, see http://www.json.org/
    ///
    /// JSON uses Arrays and Objects. These correspond here to the datatypes IList and IDictionary.
    /// All numbers are parsed to doubles.
    /// </summary>
    sealed class _JSON
    {
        /// <summary>
        /// Parses the string json into a value
        /// </summary>
        /// <param name="json">A JSON string.</param>
        /// <returns>An List&lt;object&gt;, a Dictionary&lt;string, object&gt;, a double, an integer,a string, null, true, or false</returns>
        public static JSON Deserialize(string json)
        {
            // save the string for debug information
            if (json == null)
            {
                return null;
            }

            return Parser.Parse(json);
        }

        sealed class Parser : IDisposable
        {
            const string WHITE_SPACE = " \t\n\r";
            const string WORD_BREAK = " \t\n\r{}[],:\"";

            enum TOKEN
            {
                NONE,
                CURLY_OPEN,
                CURLY_CLOSE,
                SQUARED_OPEN,
                SQUARED_CLOSE,
                COLON,
                COMMA,
                STRING,
                NUMBER,
                TRUE,
                FALSE,
                NULL
            };

            StringReader json;

            Parser(string jsonString)
            {
                json = new StringReader(jsonString);
            }

            public static JSON Parse(string jsonString)
            {
                using (var instance = new Parser(jsonString))
                {
                    return (instance.ParseValue() as JSON);
                }
            }

            public void Dispose()
            {
                json.Dispose();
                json = null;
            }

            JSON ParseObject()
            {
                Dictionary<string, object> table = new Dictionary<string, object>();
                JSON obj = new JSON();
                obj.fields = table;

                // ditch opening brace
                json.Read();

                // {
                while (true)
                {
                    switch (NextToken)
                    {
                        case TOKEN.NONE:
                            return null;
                        case TOKEN.COMMA:
                            continue;
                        case TOKEN.CURLY_CLOSE:
                            return obj;
                        default:
                            // name
                            string name = ParseString();
                            if (name == null)
                            {
                                return null;
                            }

                            // :
                            if (NextToken != TOKEN.COLON)
                            {
                                return null;
                            }
                            // ditch the colon
                            json.Read();

                            // value
                            table[name] = ParseValue();
                            break;
                    }
                }
            }

            List<object> ParseArray()
            {
                List<object> array = new List<object>();

                // ditch opening bracket
                json.Read();

                // [
                var parsing = true;
                while (parsing)
                {
                    TOKEN nextToken = NextToken;

                    switch (nextToken)
                    {
                        case TOKEN.NONE:
                            return null;
                        case TOKEN.COMMA:
                            continue;
                        case TOKEN.SQUARED_CLOSE:
                            parsing = false;
                            break;
                        default:
                            object value = ParseByToken(nextToken);

                            array.Add(value);
                            break;
                    }
                }

                return array;
            }

            object ParseValue()
            {
                TOKEN nextToken = NextToken;
                return ParseByToken(nextToken);
            }

            object ParseByToken(TOKEN token)
            {
                switch (token)
                {
                    case TOKEN.STRING:
                        return ParseString();
                    case TOKEN.NUMBER:
                        return ParseNumber();
                    case TOKEN.CURLY_OPEN:
                        return ParseObject();
                    case TOKEN.SQUARED_OPEN:
                        return ParseArray();
                    case TOKEN.TRUE:
                        return true;
                    case TOKEN.FALSE:
                        return false;
                    case TOKEN.NULL:
                        return null;
                    default:
                        return null;
                }
            }

            string ParseString()
            {
                StringBuilder s = new StringBuilder();
                char c;

                // ditch opening quote
                json.Read();

                bool parsing = true;
                while (parsing)
                {

                    if (json.Peek() == -1)
                    {
                        parsing = false;
                        break;
                    }

                    c = NextChar;
                    switch (c)
                    {
                        case '"':
                            parsing = false;
                            break;
                        case '\\':
                            if (json.Peek() == -1)
                            {
                                parsing = false;
                                break;
                            }

                            c = NextChar;
                            switch (c)
                            {
                                case '"':
                                case '\\':
                                case '/':
                                    s.Append(c);
                                    break;
                                case 'b':
                                    s.Append('\b');
                                    break;
                                case 'f':
                                    s.Append('\f');
                                    break;
                                case 'n':
                                    s.Append('\n');
                                    break;
                                case 'r':
                                    s.Append('\r');
                                    break;
                                case 't':
                                    s.Append('\t');
                                    break;
                                case 'u':
                                    var hex = new StringBuilder();

                                    for (int i = 0; i < 4; i++)
                                    {
                                        hex.Append(NextChar);
                                    }

                                    s.Append((char)Convert.ToInt32(hex.ToString(), 16));
                                    break;
                            }
                            break;
                        default:
                            s.Append(c);
                            break;
                    }
                }

                return s.ToString();
            }

            object ParseNumber()
            {
                string number = NextWord;

                if (number.IndexOf('.') == -1)
                {
                    long parsedInt;
                    Int64.TryParse(number, out parsedInt);
                    return parsedInt;
                }

                double parsedDouble;
                Double.TryParse(number, out parsedDouble);
                return parsedDouble;
            }

            void EatWhitespace()
            {
                while (WHITE_SPACE.IndexOf(PeekChar) != -1)
                {
                    json.Read();

                    if (json.Peek() == -1)
                    {
                        break;
                    }
                }
            }

            char PeekChar
            {
                get
                {
                    return Convert.ToChar(json.Peek());
                }
            }

            char NextChar
            {
                get
                {
                    return Convert.ToChar(json.Read());
                }
            }

            string NextWord
            {
                get
                {
                    StringBuilder word = new StringBuilder();

                    while (WORD_BREAK.IndexOf(PeekChar) == -1)
                    {
                        word.Append(NextChar);

                        if (json.Peek() == -1)
                        {
                            break;
                        }
                    }

                    return word.ToString();
                }
            }

            TOKEN NextToken
            {
                get
                {
                    EatWhitespace();

                    if (json.Peek() == -1)
                    {
                        return TOKEN.NONE;
                    }

                    char c = PeekChar;
                    switch (c)
                    {
                        case '{':
                            return TOKEN.CURLY_OPEN;
                        case '}':
                            json.Read();
                            return TOKEN.CURLY_CLOSE;
                        case '[':
                            return TOKEN.SQUARED_OPEN;
                        case ']':
                            json.Read();
                            return TOKEN.SQUARED_CLOSE;
                        case ',':
                            json.Read();
                            return TOKEN.COMMA;
                        case '"':
                            return TOKEN.STRING;
                        case ':':
                            return TOKEN.COLON;
                        case '0':
                        case '1':
                        case '2':
                        case '3':
                        case '4':
                        case '5':
                        case '6':
                        case '7':
                        case '8':
                        case '9':
                        case '-':
                            return TOKEN.NUMBER;
                    }

                    string word = NextWord;

                    switch (word)
                    {
                        case "false":
                            return TOKEN.FALSE;
                        case "true":
                            return TOKEN.TRUE;
                        case "null":
                            return TOKEN.NULL;
                    }

                    return TOKEN.NONE;
                }
            }
        }

        /// <summary>
        /// Converts a IDictionary / IList object or a simple type (string, int, etc.) into a JSON string
        /// </summary>
        /// <param name="json">A Dictionary&lt;string, object&gt; / List&lt;object&gt;</param>
        /// <returns>A JSON encoded string, or null if object 'json' is not serializable</returns>
        public static string Serialize(JSON obj)
        {
            return Serializer.Serialize(obj);
        }

        sealed class Serializer
        {
            StringBuilder builder;

            Serializer()
            {
                builder = new StringBuilder();
            }

            public static string Serialize(JSON obj)
            {
                var instance = new Serializer();

                instance.SerializeValue(obj);

                return instance.builder.ToString();
            }

            void SerializeValue(object value)
            {
                if (value == null)
                {
                    builder.Append("null");
                }
                else if (value as string != null)
                {
                    SerializeString(value as string);
                }
                else if (value is bool)
                {
                    builder.Append(value.ToString().ToLower());
                }
                else if (value as JSON != null)
                {
                    SerializeObject(value as JSON);
                }
                else if (value as IDictionary != null)
                {
                    SerializeDictionary(value as IDictionary);
                }
                else if (value as IList != null)
                {
                    SerializeArray(value as IList);
                }
                else if (value is char)
                {
                    SerializeString(value.ToString());
                }
                else
                {
                    SerializeOther(value);
                }
            }

            void SerializeObject(JSON obj)
            {
                SerializeDictionary(obj.fields);
            }

            void SerializeDictionary(IDictionary obj)
            {
                bool first = true;

                builder.Append('{');

                foreach (object e in obj.Keys)
                {
                    if (!first)
                    {
                        builder.Append(',');
                    }

                    SerializeString(e.ToString());
                    builder.Append(':');

                    SerializeValue(obj[e]);

                    first = false;
                }

                builder.Append('}');
            }

            void SerializeArray(IList anArray)
            {
                builder.Append('[');

                bool first = true;

                foreach (object obj in anArray)
                {
                    if (!first)
                    {
                        builder.Append(',');
                    }

                    SerializeValue(obj);

                    first = false;
                }

                builder.Append(']');
            }

            void SerializeString(string str)
            {
                builder.Append('\"');

                char[] charArray = str.ToCharArray();
                foreach (var c in charArray)
                {
                    switch (c)
                    {
                        case '"':
                            builder.Append("\\\"");
                            break;
                        case '\\':
                            builder.Append("\\\\");
                            break;
                        case '\b':
                            builder.Append("\\b");
                            break;
                        case '\f':
                            builder.Append("\\f");
                            break;
                        case '\n':
                            builder.Append("\\n");
                            break;
                        case '\r':
                            builder.Append("\\r");
                            break;
                        case '\t':
                            builder.Append("\\t");
                            break;
                        default:
                            int codepoint = Convert.ToInt32(c);
                            if ((codepoint >= 32) && (codepoint <= 126))
                            {
                                builder.Append(c);
                            }
                            else
                            {
                                builder.Append("\\u" + Convert.ToString(codepoint, 16).PadLeft(4, '0'));
                            }
                            break;
                    }
                }

                builder.Append('\"');
            }

            void SerializeOther(object value)
            {
                if (value is float
                    || value is int
                    || value is uint
                    || value is long
                    || value is double
                    || value is sbyte
                    || value is byte
                    || value is short
                    || value is ushort
                    || value is ulong
                    || value is decimal)
                {
                    builder.Append(value.ToString());
                }
                else
                {
                    SerializeString(value.ToString());
                }
            }
        }
    }



    //more
    public const int TOKEN_NONE = 0;
    public const int TOKEN_CURLY_OPEN = 1;
    public const int TOKEN_CURLY_CLOSE = 2;
    public const int TOKEN_SQUARED_OPEN = 3;
    public const int TOKEN_SQUARED_CLOSE = 4;
    public const int TOKEN_COLON = 5;
    public const int TOKEN_COMMA = 6;
    public const int TOKEN_STRING = 7;
    public const int TOKEN_NUMBER = 8;
    public const int TOKEN_TRUE = 9;
    public const int TOKEN_FALSE = 10;
    public const int TOKEN_NULL = 11;

    private const int BUILDER_CAPACITY = 2000;
    /// <summary>
    /// Parses the string json into a value
    /// </summary>
    /// <param name="json">A JSON byte array.</param>
    /// <returns>An ArrayList, a Hashtable, a double, a string, null, true, or false</returns>
    public static object JsonDecode(byte[] json)
    {
        return JsonDecode(System.Text.ASCIIEncoding.ASCII.GetString(json));
    }

    /// <summary>
    /// Parses the string json into a value
    /// </summary>
    /// <param name="json">A JSON string.</param>
    /// <returns>An ArrayList, a Hashtable, a double, a string, null, true, or false</returns>
    public static object JsonDecode(string json)
    {
        bool success = true;

        return JsonDecode(json, ref success);
    }

    /// <summary>
    /// Parses the string json into a value; and fills 'success' with the successfullness of the parse.
    /// </summary>
    /// <param name="json">A JSON string.</param>
    /// <param name="success">Successful parse?</param>
    /// <returns>An ArrayList, a Hashtable, a double, a string, null, true, or false</returns>
    public static object JsonDecode(string json, ref bool success)
    {
        success = true;
        if (json != null)
        {
            char[] charArray = json.ToCharArray();
            int index = 0;
            object value = ParseValue(charArray, ref index, ref success);
            return value;
        }
        else
        {
            return null;
        }
    }

    /// <summary>
    /// Converts a Hashtable / ArrayList object into a JSON string
    /// </summary>
    /// <param name="json">A Hashtable / ArrayList</param>
    /// <returns>A JSON encoded string, or null if object 'json' is not serializable</returns>
    public static string JsonEncode(object json)
    {
        StringBuilder builder = new StringBuilder(BUILDER_CAPACITY);
        bool success = SerializeValue(json, builder);
        return (success ? builder.ToString() : null);
    }

    protected static Hashtable ParseObject(char[] json, ref int index, ref bool success)
    {
        Hashtable table = new Hashtable();
        int token;

        // {
        NextToken(json, ref index);

        bool done = false;
        while (!done)
        {
            token = LookAhead(json, index);
            if (token == JSON.TOKEN_NONE)
            {
                success = false;
                return null;
            }
            else if (token == JSON.TOKEN_COMMA)
            {
                NextToken(json, ref index);
            }
            else if (token == JSON.TOKEN_CURLY_CLOSE)
            {
                NextToken(json, ref index);
                return table;
            }
            else
            {

                // name
                string name = ParseString(json, ref index, ref success);
                if (!success)
                {
                    success = false;
                    return null;
                }

                // :
                token = NextToken(json, ref index);
                if (token != JSON.TOKEN_COLON)
                {
                    success = false;
                    return null;
                }

                // value
                object value = ParseValue(json, ref index, ref success);
                if (!success)
                {
                    success = false;
                    return null;
                }

                table[name] = value;
            }
        }

        return table;
    }

    protected static ArrayList ParseArray(char[] json, ref int index, ref bool success)
    {
        ArrayList array = new ArrayList();

        // [
        NextToken(json, ref index);

        bool done = false;
        while (!done)
        {
            int token = LookAhead(json, index);
            if (token == JSON.TOKEN_NONE)
            {
                success = false;
                return null;
            }
            else if (token == JSON.TOKEN_COMMA)
            {
                NextToken(json, ref index);
            }
            else if (token == JSON.TOKEN_SQUARED_CLOSE)
            {
                NextToken(json, ref index);
                break;
            }
            else
            {
                object value = ParseValue(json, ref index, ref success);
                if (!success)
                {
                    return null;
                }

                array.Add(value);
            }
        }

        return array;
    }

    protected static object ParseValue(char[] json, ref int index, ref bool success)
    {
        switch (LookAhead(json, index))
        {
            case JSON.TOKEN_STRING:
                return ParseString(json, ref index, ref success);
            case JSON.TOKEN_NUMBER:
                return ParseNumber(json, ref index, ref success);
            case JSON.TOKEN_CURLY_OPEN:
                return ParseObject(json, ref index, ref success);
            case JSON.TOKEN_SQUARED_OPEN:
                return ParseArray(json, ref index, ref success);
            case JSON.TOKEN_TRUE:
                NextToken(json, ref index);
                return true;
            case JSON.TOKEN_FALSE:
                NextToken(json, ref index);
                return false;
            case JSON.TOKEN_NULL:
                NextToken(json, ref index);
                return null;
            case JSON.TOKEN_NONE:
                break;
        }

        success = false;
        return null;
    }

    protected static string ParseString(char[] json, ref int index, ref bool success)
    {
        StringBuilder s = new StringBuilder(BUILDER_CAPACITY);
        char c;

        EatWhitespace(json, ref index);

        // "
        c = json[index++];

        bool complete = false;
        while (!complete)
        {

            if (index == json.Length)
            {
                break;
            }

            c = json[index++];
            if (c == '"')
            {
                complete = true;
                break;
            }
            else if (c == '\\')
            {

                if (index == json.Length)
                {
                    break;
                }
                c = json[index++];
                if (c == '"')
                {
                    s.Append('"');
                }
                else if (c == '\\')
                {
                    s.Append('\\');
                }
                else if (c == '/')
                {
                    s.Append('/');
                }
                else if (c == 'b')
                {
                    s.Append('\b');
                }
                else if (c == 'f')
                {
                    s.Append('\f');
                }
                else if (c == 'n')
                {
                    s.Append('\n');
                }
                else if (c == 'r')
                {
                    s.Append('\r');
                }
                else if (c == 't')
                {
                    s.Append('\t');
                }
                else if (c == 'u')
                {
                    int remainingLength = json.Length - index;
                    if (remainingLength >= 4)
                    {
                        // parse the 32 bit hex into an integer codepoint
                        uint codePoint;
                        if (!(success = UInt32.TryParse(new string(json, index, 4), NumberStyles.HexNumber, CultureInfo.InvariantCulture, out codePoint)))
                        {
                            return "";
                        }
                        // convert the integer codepoint to a unicode char and add to string
                        s.Append(Char.ConvertFromUtf32((int)codePoint));
                        // skip 4 chars
                        index += 4;
                    }
                    else
                    {
                        break;
                    }
                }

            }
            else
            {
                s.Append(c);
            }

        }

        if (!complete)
        {
            success = false;
            return null;
        }

        return s.ToString();
    }

    protected static object ParseNumber(char[] json, ref int index, ref bool success)
    {
        EatWhitespace(json, ref index);

        int lastIndex = GetLastIndexOfNumber(json, index);
        int charLength = (lastIndex - index) + 1;

        var token = new string(json, index, charLength);
        index = lastIndex + 1;
        if (token.Contains("."))
        {
            float number;
            success = float.TryParse(token, NumberStyles.Any, CultureInfo.InvariantCulture, out number);
            return number;
        }
        else if (token.Length <= 10)
        {
            int number;
            success = int.TryParse(token, out number);
            return number;
        }
        else
        {
            long number;
            success = long.TryParse(token, out number);
            return number;
        }
    }

    protected static int GetLastIndexOfNumber(char[] json, int index)
    {
        int lastIndex;

        for (lastIndex = index; lastIndex < json.Length; lastIndex++)
        {
            if ("0123456789+-.eE".IndexOf(json[lastIndex]) == -1)
            {
                break;
            }
        }
        return lastIndex - 1;
    }

    protected static void EatWhitespace(char[] json, ref int index)
    {
        for (; index < json.Length; index++)
        {
            if (" \t\n\r".IndexOf(json[index]) == -1)
            {
                break;
            }
        }
    }

    protected static int LookAhead(char[] json, int index)
    {
        int saveIndex = index;
        return NextToken(json, ref saveIndex);
    }

    protected static int NextToken(char[] json, ref int index)
    {
        EatWhitespace(json, ref index);

        if (index == json.Length)
        {
            return JSON.TOKEN_NONE;
        }

        char c = json[index];
        index++;
        switch (c)
        {
            case '{':
                return JSON.TOKEN_CURLY_OPEN;
            case '}':
                return JSON.TOKEN_CURLY_CLOSE;
            case '[':
                return JSON.TOKEN_SQUARED_OPEN;
            case ']':
                return JSON.TOKEN_SQUARED_CLOSE;
            case ',':
                return JSON.TOKEN_COMMA;
            case '"':
                return JSON.TOKEN_STRING;
            case '0':
            case '1':
            case '2':
            case '3':
            case '4':
            case '5':
            case '6':
            case '7':
            case '8':
            case '9':
            case '-':
                return JSON.TOKEN_NUMBER;
            case ':':
                return JSON.TOKEN_COLON;
        }
        index--;

        int remainingLength = json.Length - index;

        // false
        if (remainingLength >= 5)
        {
            if (json[index] == 'f' && json[index + 1] == 'a' && json[index + 2] == 'l' && json[index + 3] == 's' && json[index + 4] == 'e')
            {
                index += 5;
                return JSON.TOKEN_FALSE;
            }
        }

        // true
        if (remainingLength >= 4)
        {
            if (json[index] == 't' && json[index + 1] == 'r' && json[index + 2] == 'u' && json[index + 3] == 'e')
            {
                index += 4;
                return JSON.TOKEN_TRUE;
            }
        }

        // null
        if (remainingLength >= 4)
        {
            if (json[index] == 'n' && json[index + 1] == 'u' && json[index + 2] == 'l' && json[index + 3] == 'l')
            {
                index += 4;
                return JSON.TOKEN_NULL;
            }
        }

        return JSON.TOKEN_NONE;
    }

    protected static bool SerializeValue(object value, StringBuilder builder)
    {
        bool success = true;

        if (value is string)
        {
            success = SerializeString((string)value, builder);
        }
        else if (value is Hashtable)
        {
            success = SerializeObject((Hashtable)value, builder);
        }
        else if (value is ArrayList)
        {
            success = SerializeArray((ArrayList)value, builder);
        }
        else if (IsNumeric(value))
        {
            success = SerializeNumber(Convert.ToDouble(value), builder);
        }
        else if ((value is Boolean) && ((Boolean)value == true))
        {
            builder.Append("true");
        }
        else if ((value is Boolean) && ((Boolean)value == false))
        {
            builder.Append("false");
        }
        else if (value == null)
        {
            builder.Append("null");
        }
        else if (value.GetType().IsArray)
        {
            success = SerializeArray((object[])value, builder);
        }
        else
        {
            success = false;
        }
        return success;
    }

    protected static bool SerializeObject(Hashtable anObject, StringBuilder builder)
    {
        builder.Append("{");

        IDictionaryEnumerator e = anObject.GetEnumerator();
        bool first = true;
        while (e.MoveNext())
        {
            string key = e.Key.ToString();
            object value = e.Value;

            if (!first)
            {
                builder.Append(", ");
            }

            SerializeString(key, builder);
            builder.Append(":");
            if (!SerializeValue(value, builder))
            {
                return false;
            }

            first = false;
        }

        builder.Append("}");
        return true;
    }

    protected static bool SerializeArray(ArrayList anArray, StringBuilder builder)
    {
        builder.Append("[");

        bool first = true;
        for (int i = 0; i < anArray.Count; i++)
        {
            object value = anArray[i];

            if (!first)
            {
                builder.Append(", ");
            }

            if (!SerializeValue(value, builder))
            {
                return false;
            }

            first = false;
        }

        builder.Append("]");
        return true;
    }

    protected static bool SerializeArray(object[] anArray, StringBuilder builder)
    {
        builder.Append("[");

        bool first = true;
        for (int i = 0; i < anArray.Length; i++)
        {
            object value = anArray[i];

            if (!first)
            {
                builder.Append(", ");
            }

            if (!SerializeValue(value, builder))
            {
                return false;
            }

            first = false;
        }

        builder.Append("]");
        return true;
    }

    protected static bool SerializeString(string aString, StringBuilder builder)
    {
        builder.Append("\"");

        char[] charArray = aString.ToCharArray();
        for (int i = 0; i < charArray.Length; i++)
        {
            char c = charArray[i];
            if (c == '"')
            {
                builder.Append("\\\"");
            }
            else if (c == '\\')
            {
                builder.Append("\\\\");
            }
            else if (c == '\b')
            {
                builder.Append("\\b");
            }
            else if (c == '\f')
            {
                builder.Append("\\f");
            }
            else if (c == '\n')
            {
                builder.Append("\\n");
            }
            else if (c == '\r')
            {
                builder.Append("\\r");
            }
            else if (c == '\t')
            {
                builder.Append("\\t");
            }
            else
            {
                int codepoint = Convert.ToInt32(c);
                if ((codepoint >= 32) && (codepoint <= 126))
                {
                    builder.Append(c);
                }
                else
                {
                    builder.Append("\\u" + Convert.ToString(codepoint, 16).PadLeft(4, '0'));
                }
            }
        }

        builder.Append("\"");
        return true;
    }

    protected static bool SerializeNumber(double number, StringBuilder builder)
    {
        builder.Append(Convert.ToString(number, CultureInfo.InvariantCulture));
        return true;
    }

    /// <summary>
    /// Determines if a given object is numeric in any way
    /// (can be integer, double, null, etc). 
    /// 
    /// Thanks to mtighe for pointing out Double.TryParse to me.
    /// </summary>
    protected static bool IsNumeric(object o)
    {
        double result;

        return (o == null) ? false : Double.TryParse(o.ToString(), out result);
    }
}

//}