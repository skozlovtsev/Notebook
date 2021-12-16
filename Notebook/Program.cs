using System;
using System.Collections.Generic;

namespace Notebook
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Title = "Notebook";
            Notebook notebook = new Notebook();
            bool triger = true;
            while (triger)
            {
                Console.WriteLine("####################################\n" +
                                  "# 1 - создать         2 - удалить  #\n" +
                                  "# 3 - редактировать   4 - просмотр #\n" +
                                  "# 5 - подробнее       6 - выход    #\n" +
                                  "####################################\n");
                Console.Write(">>>");
                switch (Console.ReadLine())
                {
                    case "1":
                        Console.Clear();
                        string phoneNumber;
                        while (true)
                        {
                            Console.Write("Введите номер телефона: ");
                            phoneNumber = Console.ReadLine();
                            if (Validator.IsPhoneNumberValid(phoneNumber))
                            {
                                if (notebook.CheckPhone(phoneNumber))
                                {
                                    break;
                                }
                                else
                                {
                                    Console.BackgroundColor = ConsoleColor.Red;
                                    Console.WriteLine($"номер {phoneNumber} занят");
                                    Console.ResetColor();
                                }
                            }
                            else
                            {
                                Console.BackgroundColor = ConsoleColor.Red;
                                Console.WriteLine($"номер {phoneNumber} некорректен");
                                Console.ResetColor();
                            }
                        }
                        Console.Write("Введите фамилию: ");
                        string secondName = Console.ReadLine();
                        Console.Write("Введите имя: ");
                        string firstName = Console.ReadLine();
                        Console.Write("Желаете ввести дополнительную информацию? да/нет: ");
                        switch (Console.ReadLine())
                        {
                            case "да":
                                Console.Write("Введите страну: ");
                                string country = Console.ReadLine();
                                Console.Write("Введите дату рождения: ");
                                string birthdate = Console.ReadLine();
                                Console.Write("Введите организацию: ");
                                string organization = Console.ReadLine();
                                Console.Write("Введите должновть: ");
                                string position = Console.ReadLine();
                                Console.WriteLine("Вы можете ниписать дополнительные заметки:");
                                string otherNote = Console.ReadLine();
                                notebook.Add(new Note(phoneNumber, secondName, firstName, country, birthdate, organization, position, otherNote));
                                country = null;
                                birthdate = null;
                                organization = null;
                                position = null;
                                otherNote = null;
                                break;
                            case "нет":
                                notebook.Add(new Note(phoneNumber, secondName, firstName));
                                break;
                            default:
                                notebook.Add(new Note(phoneNumber, secondName, firstName));
                                break;
                        }
                        phoneNumber = null;
                        secondName = null;
                        firstName = null;
                        GC.Collect();
                        break;
                    case "2":
                        Console.Write("Введите номер телефона или имя и фамилию: ");
                        notebook.Del(Console.ReadLine());
                        break;
                    case "3":
                        Console.Clear();
                        Console.Write("Введите номер телефона или имя и фамилию: ");
                        notebook.Edit(Console.ReadLine());
                        break;
                    case "4":
                        Console.Clear();
                        notebook.ShowNotes();
                        break;
                    case "5":
                        Console.Clear();
                        Console.Write("Введите номер телефона или имя и фамилию: ");
                        notebook.ShowNote(Console.ReadLine());
                        break;
                    case "6":
                        triger = false;
                        break;
                    default:
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.WriteLine("для того чтобы узнать команды, напишите help");
                        Console.ResetColor();
                        break;
                }
            }
        }

    }

    class Notebook
    {
        private List<Note> notes = new List<Note>();
        private List<string> uniqueKeys = new List<string>();
        
        public bool CheckPhone(string phoneNumber)
        {
            return !uniqueKeys.Contains(phoneNumber);
        }
        public void Add(Note note)
        {
            uniqueKeys.Add(note.phoneNumber);
            notes.Add(note);
        }

        public void Edit(string key)
        {
            try
            {
                Note note = notes[Find(key)];
                Console.WriteLine($"Номер телефона: {note.phoneNumber}\nФамилия: {note.secondName}\nИмя: {note.firstName}\nСтрана: {note.country}\nДата рождения: {note.birthdate}\nОрганизация: {note.organization}\nДолжность: {note.position}\nЗаметки: {note.otherNote}");
                Console.SetCursorPosition(16, Console.CursorTop - 8);
                string phoneNumber;
                while (true)
                {
                    phoneNumber = Console.ReadLine();
                    if ((Validator.IsPhoneNumberValid(phoneNumber)) && (CheckPhone(phoneNumber)))
                    {
                        break;
                    }
                    else
                    {
                        Console.SetCursorPosition(16, Console.CursorTop - 1);
                    }
                }
                note.phoneNumber = phoneNumber;
                Console.SetCursorPosition(9, Console.CursorTop);
                string sn = Console.ReadLine();
                if (sn != "")
                {
                    note.secondName = sn;
                }
                Console.SetCursorPosition(5, Console.CursorTop);
                string fn = Console.ReadLine();
                if (fn != "")
                {
                    note.firstName = fn;
                }
                Console.SetCursorPosition(8, Console.CursorTop);
                string co = Console.ReadLine();
                if (co != "")
                {
                    note.country = co;
                }
                Console.SetCursorPosition(15, Console.CursorTop);
                string bd = Console.ReadLine();
                if (bd != "")
                {
                    note.birthdate = bd;
                }
                Console.SetCursorPosition(13, Console.CursorTop);
                string org = Console.ReadLine();
                if (org != "")
                {
                    note.organization = org;
                }
                Console.SetCursorPosition(11, Console.CursorTop);
                string pos = Console.ReadLine();
                if (pos != "")
                {
                    note.position = pos;
                }
                Console.SetCursorPosition(9, Console.CursorTop);
                string on = Console.ReadLine();
                if (on != "")
                {
                    note.otherNote = on;
                }
            }
            catch (System.ArgumentOutOfRangeException)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("такой записи нет в записной книжке");
                Console.ResetColor();
            }
        }
        public void Del(string key)
        {
            try
            {
                notes.RemoveAt(Find(key));
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"Запись '{key}' успешно удалина");
                Console.ResetColor();
            }
            catch (System.ArgumentOutOfRangeException)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("такой записи нет в записной книжке");
                Console.ResetColor();
            }
        }

        public void ShowNote(string key)
        {
            try
            {
                Note note = notes[Find(key)];
                Console.Clear();
                Console.WriteLine($"Номер телефона: {note.phoneNumber}\nФамилия: {note.secondName}\nИмя: {note.firstName}\nСтрана: {note.country}\nДата рождения: {note.birthdate}\nОрганизация: {note.organization}\nДолжность: {note.position}\nЗаметки: {note.otherNote}");
            }
            catch (System.ArgumentOutOfRangeException)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("такой записи нет в записной книжке");
                Console.ResetColor();
            }
        }
        public void ShowNotes()
        {
            Console.Clear();
            foreach (Note note in notes)
            {
                Console.WriteLine($"{note.phoneNumber}  #  {note.secondName} {note.firstName}");
                Console.WriteLine("");
            }
        }

        private int Find(string key)
        {
            for (int i = 0; i < notes.Count; i++)
            {
                if (notes[i] == key)
                {
                    return i;
                }
            }
            return -1;
        }
    }

    class Note
    {
        public string phoneNumber;
        public string secondName;
        public string firstName;
        public string country;
        public string birthdate;
        public string organization;
        public string position;
        public string otherNote;

        public Note(string phoneNumber, string secondName, string firstName, string country = "_", string birthdate = "_", string organization = "_", string position = "_", string otherNote = "...")
        {
            this.phoneNumber = phoneNumber;
            this.secondName = secondName;
            this.firstName = firstName;
            this.country = country;
            this.birthdate = birthdate;
            this.organization = organization;
            this.position = position;
            this.otherNote = otherNote;

        }

        public override bool Equals(object obj) => this.Equals(obj as string);
        public bool Equals(string other)
        {
            if ((other == phoneNumber) || (other == $"{firstName} {secondName}") || (other == $"{secondName} {firstName}"))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static bool operator ==(Note lhs, string rhs)
        {
            return lhs.Equals(rhs);
        }

        public static bool operator !=(Note lhs, string rhs) => !(lhs == rhs);

        public static bool operator ==(string lhs, Note rhs)
        {
            return rhs.Equals(lhs);
        }

        public static bool operator !=(string lhs, Note rhs) => !(lhs == rhs);
    }

    class Validator
    {
        public static bool IsPhoneNumberValid(string number)
        {
            long a = 0;
            if ((number.Length == 11) && (long.TryParse(number, out a)))
            {
                return true;
            }
            return false;
        }
    }
}
