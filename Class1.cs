using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Xml.Serialization;
using Newtonsoft.Json;

public class Figure
{
    public string Name;
    public int Height;
    public int Width;
}
public class Edit_Element
{
    public int y_position_cursor = 0;
    private int x_position_cursor = 0;
    public List<string> list1 = new List<string>();
    private string add_char_element(char a, string element)
    {
        List<char> list1 = new List<char>();
        if (element.Length < 1)
        {
            element = Convert.ToString(a);
        }
        else
        {
            foreach (var elem in element)
            {
                list1.Add(elem);
            }
            element = "";
            list1.Insert(x_position_cursor, a);
            foreach (char elem in list1)
            {
                element = element + elem;
            }
        }
        return (element);
    }
    private string remove_char_element(string element)
    {
        List<char> list1 = new List<char>();
        foreach (var elem in element)
        {
            list1.Add(elem);
        }
        element = "";
        list1.RemoveAt(x_position_cursor);
        foreach(var elem in list1)
        {
            element = element + elem;
        }
        return (element);
    }
    public string edit_element(string element)
    {
        string element2 = element;
        Console.CursorVisible = true;
        bool start = true;
        while (start)
        {
            Console.Clear();
            Console.WriteLine(element);
            Console.WriteLine(element.Length);
            Console.SetCursorPosition(x_position_cursor, 0);
            ConsoleKeyInfo key = Console.ReadKey();
            if (key.Key == ConsoleKey.RightArrow)
            {
                x_position_cursor++;
                if (x_position_cursor > element.Length)
                {
                    x_position_cursor = 0;
                }
            }
            else if (key.Key == ConsoleKey.LeftArrow)
            {
                x_position_cursor--;
                if (x_position_cursor < 0)
                {
                    x_position_cursor = element.Length;
                }
            }
            else if (key.Key == ConsoleKey.Escape)
            {
                start = false;
                Console.CursorVisible = false;
            }
            else if (key.Key == ConsoleKey.Backspace)
            {
                if(x_position_cursor < element.Length)
                {
                    element = remove_char_element(element);
                }
                x_position_cursor--;
                if (x_position_cursor < 0)
                {
                    x_position_cursor = 0;
                }
            }
            else {
                char a = key.KeyChar;
                if (char.IsNumber(element2[0]) && char.IsNumber(a))
                {
                    element = add_char_element(a, element);
                    x_position_cursor++;
                }
                else if (char.IsLetter(element2[0]) && char.IsLetter(a))
                {
                    element = add_char_element(a, element);
                    x_position_cursor++;
                }
            }
        }
        return (element);
    }

}
internal class Json
{
    private List<string> list1 = new List<string>();
    public List<string> DS_Json(string file)
    {
        List<Figure> list2 = JsonConvert.DeserializeObject<List<Figure>>(File.ReadAllText(file));
        foreach (var element in list2)
        {
            list1.Add(element.Name);
            list1.Add(Convert.ToString(element.Height));
            list1.Add(Convert.ToString(element.Width));
        }
        return (list1);
    }
    public void S_Json(string File_save, List<string> arr)
    {
        File_save = File_save + ".json";
        List<Figure> list2 = new List<Figure>();
        for (int i = 0; i < arr.Count() / 3; i++)
        {
            Figure f = new Figure();
            f.Name = arr[i * 3];
            f.Height = Convert.ToInt32(arr[i * 3 + 1]);
            f.Width = Convert.ToInt32(arr[i * 3 + 2]);
            list2.Add(f);
        }
        File.WriteAllText(File_save, JsonConvert.SerializeObject(list2));
    }
}
internal class Txt
{
    private List<Dictionary<string, dynamic>> list1 = new List<Dictionary<string, dynamic>>();
    public List<string> DS_Txt(string file)
    {
        List<string> list1 = new List<string>();
        file = File.ReadAllText(file);
        var file1 = file.Split();
        for (int i = 1; i < file1.Count(); i = i + 2)
        {
            list1.Add(file1[i]);
        }
        return (list1);
    }
    public void S_Txt(string file, List<string> arr)
    {
        file = file + ".txt";
        if (File.Exists(file))
        {
            File.Delete(file);
        }
        for (int i = 0; i < arr.Count() / 3; i++)
        {
            Dictionary<string, dynamic> list2 = new Dictionary<string, dynamic>();
            list2.Add("Name", arr[i * 3]);
            list2.Add("Height", Convert.ToInt32(arr[i * 3 + 1]));
            list2.Add("Width", Convert.ToInt32(arr[i * 3 + 2]));
            list1.Add(list2);
        }

        foreach (var element in list1)
        {
            foreach (var element2 in element)
            {
                Console.WriteLine(!File.Exists(file));
                if (File.Exists(file))
                {
                    File.AppendAllText(file, $"{element2.Key}: {element2.Value}" + "\n");
                }
                else
                {
                    File.WriteAllText(file, $"{element2.Key}: {element2.Value}" + "\n");
                }
            }
        }
    }
}
internal class Xml
{
    private int n = 0;
    private List<Figure> list1 = new List<Figure>();
    public List<string> DS_Xml(string file)
    {
        List<Figure> list1 = new List<Figure>();
        List<string> list2 = new List<string>();
        XmlSerializer xml = new XmlSerializer(typeof(List<Figure>));
        using (FileStream fs = new FileStream(file, FileMode.Open))
        {
            list1 = (List<Figure>)xml.Deserialize(fs);
        }
        foreach(var element in list1)
        {
            list2.Add(element.Name);
            list2.Add(Convert.ToString(element.Height));
            list2.Add(Convert.ToString(element.Width));
        }
        return (list2);
    }
    public void S_Xml(string file, List<string> arr)
    {
        file = file + ".xml";
        List<Figure> list2 = new List<Figure>();
        for (int i = 0; i < arr.Count() / 3; i++)
        {
            Figure f = new Figure();
            f.Name = arr[i * 3];
            f.Height = Convert.ToInt32(arr[i * 3 + 1]);
            f.Width = Convert.ToInt32(arr[i * 3 + 2]);
            list2.Add(f);
        }
        XmlSerializer xml = new XmlSerializer(typeof(List<Figure>));
        using (FileStream fs = new FileStream(file, FileMode.OpenOrCreate))
        {
            xml.Serialize(fs, list2);
        }
    }
}