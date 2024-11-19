using System.Diagnostics;

namespace NET_09_Assignment;

class Program
{
    static void Main(string[] args)
    {
        #region Set up
        ICollection<Course> courses = new List<Course>()
        {
            new Course("C#"),
            new Course("HTML"),
            new Course("React"),
        };
        ICollection<Student> students = new List<Student>()
        {
             new Student("James"),
             new Student("Mary"),
             new Student("John"),
             new Student("Michael"),
             new Student("Johnny"),
             new Student("Kathmander"),
             new Student("Laura"),
             new Student("Steve"),
        };
        
        // add courses
        foreach (Student student in students)
        {
            StudentAddCourse(courses.ElementAt(0), student);
            StudentAddCourse(courses.ElementAt(1), student);
            StudentAddCourse(courses.ElementAt(2), student);
        }
        
        // mark
        courses.ElementAt(0).MarkCourse(students.ElementAt(0).Id, 70);
        courses.ElementAt(0).MarkCourse(students.ElementAt(1).Id, 80);
        courses.ElementAt(0).MarkCourse(students.ElementAt(2).Id, 90);
        courses.ElementAt(0).MarkCourse(students.ElementAt(3).Id, 95);
        courses.ElementAt(0).MarkCourse(students.ElementAt(4).Id, 96);
        courses.ElementAt(0).MarkCourse(students.ElementAt(5).Id, 99);
        courses.ElementAt(0).MarkCourse(students.ElementAt(6).Id, 98);
        courses.ElementAt(0).MarkCourse(students.ElementAt(7).Id, 70);
        
        courses.ElementAt(1).MarkCourse(students.ElementAt(0).Id, 75);
        courses.ElementAt(1).MarkCourse(students.ElementAt(1).Id, 75);
        courses.ElementAt(1).MarkCourse(students.ElementAt(2).Id, 81);
        courses.ElementAt(1).MarkCourse(students.ElementAt(3).Id, 94);
        courses.ElementAt(1).MarkCourse(students.ElementAt(4).Id, 96);
        courses.ElementAt(1).MarkCourse(students.ElementAt(5).Id, 98);
        courses.ElementAt(1).MarkCourse(students.ElementAt(6).Id, 98);
        courses.ElementAt(1).MarkCourse(students.ElementAt(7).Id, 99);
        
        courses.ElementAt(2).MarkCourse(students.ElementAt(0).Id, 85);
        courses.ElementAt(2).MarkCourse(students.ElementAt(1).Id, 85);
        courses.ElementAt(2).MarkCourse(students.ElementAt(2).Id, 88);
        courses.ElementAt(2).MarkCourse(students.ElementAt(3).Id, 64);
        courses.ElementAt(2).MarkCourse(students.ElementAt(4).Id, 76);
        courses.ElementAt(2).MarkCourse(students.ElementAt(5).Id, 99);
        courses.ElementAt(2).MarkCourse(students.ElementAt(6).Id, 100);
        courses.ElementAt(2).MarkCourse(students.ElementAt(7).Id, 100);
        
        #endregion
        
        #region 01 unique elements
        Console.WriteLine("01 unique elements---------------------->");

        HashSet<double> scoresCS = courses.ElementAt(0).getMarks().ToHashSet<double>();
        HashSet<double> scoresHTML = courses.ElementAt(1).getMarks().ToHashSet<double>();
        HashSet<double> scoresReact = courses.ElementAt(2).getMarks().ToHashSet<double>();
        
        Console.WriteLine("scores for CSharp: ");
        PrintCollections<double>(scoresCS);
        Console.WriteLine("\nscores for HTML: ");
        PrintCollections<double>(scoresHTML);
        Console.WriteLine("\nscores for React: ");
        PrintCollections<double>(scoresReact);
        #endregion
        
        #region 02 <key, value> pairs
        Console.WriteLine("\n02 unique elements---------------------->");
        foreach (var student in students)
        {
            IDictionary<string, double?> grades = student.GetGrades();
            PrintCollections<KeyValuePair<string, double?>>(grades);
            Console.WriteLine("");
        }
        #endregion
        
        #region 03 Lookup <key, value> pairs
        Console.WriteLine("\n03 lookup---------------------->");
        foreach (var course in courses)
        {
            ILookup<double, string> lookup  = course.getStudentNamesWithSameMark();
            Console.WriteLine($"Course Name: {course.CourseName}");
            PrintLoopUp<double, string>(lookup);
        }
        #endregion
        
        #region 04 Undo and Redo
        Console.WriteLine("\n04 Undo and Redo---------------------->");
        static void QUERY(string[] queries)
        {
            Stack<string> unDos = new Stack<string>();
            Stack<string> redos = new Stack<string>();
            
            foreach (var query in queries)
            {
                string ele;
                if (query.StartsWith("WRITE"))
                {
                    ele = query.Split(" ")[1];
                    unDos.Push(ele);
                }
                else
                {
                    switch(query)
                    {
                        case "UNDO":
                            if (unDos.Count > 0)
                            {
                                string topDoEle = unDos.Pop();
                                redos.Push(topDoEle);
                            }
                            break;
                        case "REDO":
                            if (redos.Count > 0)
                            {
                                string topUndoEle = redos.Pop();
                                unDos.Push(topUndoEle);
                            }
                            break;
                        case "READ":
                            PrintStack<string>(unDos);
                            break;
                        default:
                            break;
                    }
                }
            }
        }
        
        // test
        string[] Q1 = { "WRITE x", "WRITE y", "UNDO", "WRITE z", "READ", "REDO", "READ"};
        string[] Q2 = { "WRITE A", "WRITE B", "WRITE C", "UNDO", "READ", "REDO", "READ" };
        QUERY(Q1);
        Console.WriteLine("");
        QUERY(Q2);
        
        #endregion
        
        #region helpers

        static void PrintCollections<T>(IEnumerable<T> collection)
        {
            foreach (var element in collection)
            {
                Console.Write($"{element} ");
            }
            Console.WriteLine("");
        }

        static void PrintLoopUp<T,K>(ILookup<T,K> lookup)
        {
            foreach (var group in lookup)
            {
                Console.Write($"{group.Key}: ");
                foreach (var value in group)
                {
                    Console.Write($"{value} ");
                }
                Console.WriteLine("");
            }
        }

        static void PrintStack<T>(in Stack<T> stack)
        {
            Stack<T> temp = new Stack<T>();
            foreach (var ele in stack)
            {
                temp.Push(ele);
            }
            foreach (var ele in temp)
            {
                Console.Write(ele);
            }
            Console.Write(" ");
        }

        static void StudentAddCourse(Course course, Student student)
        {
            StudentCourse studentCourse = new StudentCourse(course.Id,course,student.Id,student);
            course.StudentCourses.Add(studentCourse);
            student.StudentCourses.Add(studentCourse);
        }
        #endregion
    }
}