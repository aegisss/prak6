using static System.Net.Mime.MediaTypeNames;
using System.Net.Http.Headers;
using System.Xml.Linq;

namespace ConsoleApp3
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.CursorVisible = false;
            bool first_menu = true;
            Console.Write("Введите путь до файла : ");
            string file = Console.ReadLine();
            Edit_Element element = new Edit_Element();
            if (file[file.Length - 1] == 'n')
            {
                Json json_file = new Json();
                List<string> arr = json_file.DS_Json(file);
                element.list1 = arr;
                drew(element.list1, first_menu, element);
            }
            else if (file[file.Length - 1] == 't')
            {
                Txt txt_file = new Txt();
                List<string> arr = txt_file.DS_Txt(file);
                element.list1 = arr;
                drew(element.list1, first_menu, element);
            }else if (file[file.Length - 1] == 'l')
            {
                Xml xml_file = new Xml();
                List<string> arr = xml_file.DS_Xml(file);
                element.list1 = arr;
                drew(element.list1, first_menu, element);
            }
        }
        static void drew(List<string>arr, bool first_menu, Edit_Element element)
        {

            bool start = true;
            while (start)
            {
                Console.Clear();
                foreach (var elem in arr)
                {
                    Console.WriteLine("  " + elem);
                }
                ConsoleKeyInfo key = arrow_menu(arr, element);
                if (key.Key == ConsoleKey.Escape)
                {
                    Console.Clear();
                    start = false;
                }else if (key.Key == ConsoleKey.Enter)
                {
                    if (first_menu)
                    {
                        string elem = element.edit_element(arr[element.y_position_cursor]);
                        arr[element.y_position_cursor] = elem;
                    }
                    else
                    {
                        safe_file(element,arr);
                    }
                }else if (key.Key == ConsoleKey.F1)
                {
                    change_save(element);
                }
            }
        }
        static ConsoleKeyInfo arrow_menu(List<string> arr, Edit_Element element)
        {
            Console.SetCursorPosition(0, element.y_position_cursor);
            Console.WriteLine("->");
            ConsoleKeyInfo key = Console.ReadKey();
            if (key.Key == ConsoleKey.DownArrow)
            {
                element.y_position_cursor++;
                if(element.y_position_cursor > arr.Count()-1)
                {
                    element.y_position_cursor = 0;
                }
            }
            else if (key.Key == ConsoleKey.UpArrow)
            {
                element.y_position_cursor--;
                if (element.y_position_cursor < 0)
                {
                    element.y_position_cursor = arr.Count()-1;
                }
            }
            return (key);
        }
        static string right_save_file()
        {
            Console.Clear();
            Console.Write("Введите путь сохранения файла: ");
            string file = Console.ReadLine();
            return (file);
        }
        static void change_save(Edit_Element element)
        {
            List<string> list2 = new List<string>()
            {
                "Json",
                "Txt",
                "Xml"
            };
            bool first_menu = false;
            drew(list2, first_menu, element);
        }
        static void safe_file(Edit_Element element, List<string> arr)
        {
            string file = right_save_file();
            if (arr[element.y_position_cursor] == "Json")
            {
                Json f = new Json();
                Console.Clear();
                f.S_Json(file, element.list1);
            }
            else if (arr[element.y_position_cursor] == "Txt")
            {
                Txt f = new Txt();
                Console.Clear();
                f.S_Txt(file, element.list1);
            }
            else
            {
                Xml f = new Xml();
                Console.Clear();
                f.S_Xml(file, element.list1);
            }

        }
    }
}