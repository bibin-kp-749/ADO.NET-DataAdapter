namespace ADO.NET_DataAdapter.Model
{
    public interface IServices
    {
        public IEnumerable<student> GetStudents();
        public void UpdateStudent(student student);
        public void DeleteStudent(int Id);
    }
}
