using BasketSchools.Domain;

namespace BasketSchools.Repository;

public class StudentRepo : IRepository<int, Student>
{
    private List<Student> _students = new();
    
    public StudentRepo()
    {
        ReadFile();
    }
    
    public Student? FindOne(int id)
    {
        if (id == 0)
            throw new ArgumentException("ID cannot be null or zero.");
        
        return _students.FirstOrDefault(s => s.Id == id);
    }
    
    public IEnumerable<Student> FindAll()
    {
        return _students;
    }
    
    public Student? Save(Student student)
    {
        if (student == null)
            throw new ArgumentException("Student cannot be null.");
        
        if (FindOne(student.Id) != null)
            return student;
        
        _students.Add(student);
        return null;
    }
    
    public Student? Delete(int id)
    {
        var student = FindOne(id);
        if (student == null)
            return null;
        
        _students.Remove(student);
        return student;
    }
    
    public Student? Update(Student student)
    {
        if (student == null)
            throw new ArgumentException("Student cannot be null.");
        
        var existing = FindOne(student.Id);
        if (existing == null)
            return student;
        
        existing.Name = student.Name;
        existing.School = student.School;
        
        return null;
    }
    
    public void ReadFile()
    {
        var lines = File.ReadAllLines(
            "/Users/stefansandru/Documents/BasketSchools/BasketSchools/DataFiles/students.csv");
        foreach (var line in lines)
        {
            var values = line.Split(',');
            var student = new Student
            {
                Id = int.Parse(values[0]),
                Name = (values[1]),
                School = (values[2]),
            };
            _students.Add(student);
        }
    }
}