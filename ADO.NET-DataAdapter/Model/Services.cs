using Microsoft.Data.SqlClient;
using System.Data;

namespace ADO.NET_DataAdapter.Model
{
    public class Services:IServices
    {
        private readonly string _connection;
        public Services(IConfiguration configuration)
        {
            this._connection = configuration["ConnectionStrings:DefaultConnection"];
        }
        public IEnumerable<student> GetStudents()
        {
            List<student> students = new List<student>();
            using(SqlConnection _con=new SqlConnection(_connection))
            {
                SqlDataAdapter adapter = new SqlDataAdapter("select * from student",_con);
                DataSet dataSet = new DataSet();
                adapter.Fill(dataSet);
                foreach(DataRow row in dataSet.Tables[0].Rows)
                {
                    students.Add(new student { 
                        Id = Convert.ToInt32(row["id"]),
                        Name = row["name"].ToString(),
                        Department = row["department"].ToString(),
                        Age = Convert.ToInt32(row["age"])
                    });
                }
            }
            return students;
        }
        public void UpdateStudent(student student)
        {
            using(SqlConnection _con = new SqlConnection(_connection))
            {
                SqlCommand selectcommand = new SqlCommand("select * from student where id=@id", _con);
                selectcommand.Parameters.AddWithValue("@id", student.Id);
                SqlDataAdapter adapter = new SqlDataAdapter(selectcommand);
                DataSet dataSet = new DataSet();
                adapter.Fill(dataSet);
                DataRow row = dataSet.Tables[0].Rows[0];
                row["name"]= student.Name;
                row["department"]=student.Department;
                row["age"]=student.Age;
                SqlCommandBuilder sqlCommandBuilder = new SqlCommandBuilder(adapter);
                adapter.Update(dataSet);
            }
        }
        public void DeleteStudent(int Id)
        {
            using(SqlConnection _con = new SqlConnection(_connection))
            {
                SqlCommand sqlCommand = new SqlCommand("select * from student where id=id", _con);
                sqlCommand.Parameters.AddWithValue("id", Id);
                SqlDataAdapter adapter = new SqlDataAdapter(sqlCommand);
                DataSet ds= new DataSet();
                adapter.Fill(ds);
                ds.Tables[0].Rows[0].Delete();
                SqlCommandBuilder sqlCommandBuilder= new SqlCommandBuilder(adapter);
                adapter.Update(ds);
            }
        }
        public string AddStuddent(student student)
        {
            using(SqlConnection _con=new SqlConnection(_connection))
            {
                SqlDataAdapter adapter= new SqlDataAdapter("select * from student", _con);
                DataSet dataSet= new DataSet();
                adapter.Fill(dataSet);
                DataRow newrow = dataSet.Tables[0].NewRow();
                newrow[1]=student.Name;
                newrow[2]=student.Department;
                newrow[3]=student.Age;
                dataSet.Tables[0].Rows.Add(newrow);
                SqlCommandBuilder cmdBuilder= new SqlCommandBuilder(adapter);
                adapter.Update(dataSet);
            }
            return "ok";
        }
    }
}
