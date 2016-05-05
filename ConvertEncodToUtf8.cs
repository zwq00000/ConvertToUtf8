using System;
using System.IO;
using System.Text;

public class Program
{
    const string usage = "Convert .cs File Encoding To Utf-8\r\n";
    public static void Main(string[] args)
    {
        if (args.Length == 0)
        {
            Console.WriteLine(usage);
        }
        else
        {
            var path = args[0];
            Console.WriteLine("Convert Files " + path);
            Convert(path);
        }
    }

    public static void Convert(string path)
    {

        var files = Directory.GetFiles(path, "*.cs", SearchOption.AllDirectories);
        foreach (var file in files)
        {
            var encoding = GetEncoding(file);
            if (!encoding.Equals(Encoding.UTF8))
            {
                Console.WriteLine($"{Path.GetFileName(file)}\tEncoding:{encoding.EncodingName}");
                ConvertToUtf8(file);
            }
        }
    }

    private static void ConvertToUtf8(string file)
    {
        string text;
        using (var reader = new StreamReader(File.OpenRead(file),Encoding.Default, true))
        {
            text = reader.ReadToEnd();
        }
        using (var writer = new StreamWriter(File.OpenWrite(file), Encoding.UTF8))
        {
            writer.Write(text);
            writer.Flush();
        }
    }

    private static Encoding GetEncoding(string file)
    {
        using (var reader = new StreamReader(File.OpenRead(file),Encoding.Default, true))
        {
            reader.Read();
            return reader.CurrentEncoding;
        }
    }
}
