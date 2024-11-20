

using Student_Info_Manager;

int numOfStudents;

// Ask user to enter the number of student
Console.WriteLine("Please enter the number of students:\n");
IntPaserValidation(out numOfStudents);

int counter = 1;
string? name;
int age;
string? course;
StudentList studentList = new StudentList();

while (numOfStudents > 0) {
    // Create student and save to student list
    Student student = new Student();
    bool nameIsValid = false;
    do
    {
        Console.WriteLine($"Please enter student{(counter > 9 ? "" : "0")}{counter} name: \n (Name length should greater than 0 and less than 20)");
        AvoidNullReadLine(out name);
        try {
            student.Name = name!;
            nameIsValid = true;
        } catch (ArgumentException err)
        {
            Console.WriteLine(err.Message);
        }
        
    } while (!nameIsValid);

    bool ageIsValid = false;
    do {
        Console.WriteLine($"Please enter student{(counter > 9 ? "" : "0")}{counter} age: \n (Age should greater or equeal to 16 and less than or equal to 35)");
        IntPaserValidation(out age);
        try {
            student.Age = age!;
            ageIsValid = true;
        }
        catch(ArgumentException err)
        {
           Console.WriteLine(err.Message);
        }
        
    } while (!ageIsValid);

    bool courseIsValid = false;
    do {
        Console.WriteLine($"Please enter student{(counter > 9 ? "" : "0")}{counter} course: \n (Course length should greater than 0 and less than 20)");
        AvoidNullReadLine(out course);
        try {
            student.Course = course!;
            courseIsValid = true;
        } catch (ArgumentException err)
        {
            Console.WriteLine(err.Message);
        }
    } while (!courseIsValid);
   
    studentList.AddStudent(student);
    numOfStudents--;
    counter++;
}

// Print recorded student list
studentList.PrintStudents();

// Sort recorded student list ordered by age and print
studentList.SortStudentsByAge();
Console.WriteLine("After sorting by age......");
studentList.PrintStudents();

// Search student by name
string? searchedStudenName;
Console.WriteLine("Please enter student name for searching:\n");

AvoidNullReadLine(out searchedStudenName);

studentList.SearchStudentByName(searchedStudenName!);


static void AvoidNullReadLine(out string? input)
{
    do
    {
        input = Console.ReadLine();
    }
    while (string.IsNullOrWhiteSpace(input));
}

static void IntPaserValidation(out int age)
{
    while (!int.TryParse(Console.ReadLine(), out age))
    {
        Console.WriteLine("Please enter a valid number:\n");
    }
}