using Domain.Entities.Base;

namespace Domain.Entities
{
    public class Department : BaseEntity
    {
        public Department(string name)
        {
            SetName(name);
        }

        public string Name { get; private set; }

        public void SetName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Name cannot be empty");

            Name = name.Trim();
        }

        public IList<Teacher> Teachers { get; } = new List<Teacher>();
        public IList<Student> Students { get; } = new List<Student>();
    }
}
