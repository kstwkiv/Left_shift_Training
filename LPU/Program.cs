using System;
using System.Collections.Generic;

namespace LPU
{
    class Program
    {
        public static void Main(string[] args)
        {
            Hostels h1 = new Hostels();
            List<Student> students = new List<Student>();

            while (true)
            {
                Console.WriteLine("\n===== LPU MANAGEMENT SYSTEM =====");
                Console.WriteLine("1. Enter Hostel Details");
                Console.WriteLine("2. Enter Student Details");
                Console.WriteLine("3. Show Hostel Details");
                Console.WriteLine("4. Show Student Details");
                Console.WriteLine("5. Exit");
                Console.Write("Enter choice: ");

                int choice;
                if (!int.TryParse(Console.ReadLine(), out choice))
                {
                    Console.WriteLine("Enter a valid choice:");
                    continue;
                }

                switch (choice)
                {
                    case 1:
                        Console.WriteLine("Enter hostel name");
                        h1.HostelName = Console.ReadLine();

                        Console.WriteLine("Enter Room No");
                        int room;
                        while (!int.TryParse(Console.ReadLine(), out room))
                        {
                            Console.WriteLine("Invalid Input. Enter a valid number:");
                        }
                        h1.RoomNo=room;
                        Console.WriteLine("Enter Bed Placement");
                        h1.Bed = Console.ReadLine();
                        break;

                    case 2:
                        if (string.IsNullOrEmpty(h1.HostelName))
                        {
                            Console.WriteLine("Enter hostel details first.");
                            break;
                        }

                        Student s1 = new Student();

                        Console.WriteLine("Enter your name");
                        s1.Sname = Console.ReadLine();

                        Console.WriteLine("Enter your registration number");
                        int regno;
                        while (!int.TryParse(Console.ReadLine(), out regno))
                        {
                            Console.WriteLine("Invalid Input. Enter a valid number:");
                        }
                        s1.RegNo=regno;

                        Console.WriteLine("Enter your department");
                        s1.Dept = Console.ReadLine();

                        s1.HostelName = h1.HostelName;
                        students.Add(s1);

                        Console.WriteLine("Student added successfully.");
                        break;

                    case 3:
                        h1.DisplayHostel();
                        break;

                    case 4:
                        if (students.Count == 0)
                        {
                            Console.WriteLine("No student data available");
                        }
                        else
                        {
                            foreach (Student s in students)
                            {
                                Console.WriteLine("------------------");
                                s.DisplayStudent();
                            }
                        }
                        break;

                    case 5:
                        return;

                    default:
                        Console.WriteLine("Invalid Choice.");
                        break;
                }
            }
        }
    }
}
