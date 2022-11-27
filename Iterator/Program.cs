using System.Collections;

namespace Iterator;

#nullable disable


class Student
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Surname { get; set; }
}



class StudentCollection : IIterable<Student>
{
    private readonly List<Student> _students = new();

    public void Add(Student student) => _students.Add(student);

    public List<Student> GetStudents() => _students;

    public IIterator<Student> GetEnumerator()
       => new StudentIterator(this);
}


class StudentIterator : IIterator<Student>
{
    private int _index = -1;
    private StudentCollection _students;

    public StudentIterator(StudentCollection students)
    {
        _students = students;
    }

    public Student Current => _students.GetStudents()[_index];

    public bool MoveNext() => ++_index < _students.GetStudents().Count;

    public void Reset() => _index = -1;

}


internal interface IIterable<T>
{
    IIterator<T> GetEnumerator();
}

public interface IIterator<T>
{
    T Current { get; }

    bool MoveNext();

    void Reset();
}


class Program
{

    static void Main()
    {
        StudentCollection students = new();
        students.Add(new Student { Id = 1, Name = "Iqbal", Surname = "Rahimli" });
        students.Add(new Student { Id = 2, Name = "Ilham", Surname = "Haciyev" });
        students.Add(new Student { Id = 2, Name = "Kamran", Surname = "Maxsudov" });
        students.Add(new Student { Id = 2, Name = "Tural", Surname = "Haji-Nabili" });



        var iterator = students.GetEnumerator();

        while (iterator.MoveNext())
        {
            Console.WriteLine((iterator.Current as Student).Name);
        }


        iterator.Reset();
        Console.WriteLine('\n');


        while (iterator.MoveNext())
        {
            Console.WriteLine((iterator.Current as Student).Name);
        }



        // IIterable == IEnumerable
        // IIterator == IEnumerator
    }
}