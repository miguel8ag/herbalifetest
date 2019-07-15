using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using Entities;

namespace WcfServiceTest
{
    // NOTA: puede usar el comando "Rename" del menú "Refactorizar" para cambiar el nombre de clase "Service1" en el código, en svc y en el archivo de configuración.
    // NOTE: para iniciar el Cliente de prueba WCF para probar este servicio, seleccione Service1.svc o Service1.svc.cs en el Explorador de soluciones e inicie la depuración.
    public class Service1 : IService1
    {
        private SchoolContext _ctx;
        public SchoolContext Ctx
        {
            get
            {
                if (_ctx == null)
                {
                    _ctx = new SchoolContext();
                }
                return _ctx;
            }
        }

        public bool DeleteStudent(int nControl)
        {
            Student student = Ctx.Students.FirstOrDefault(s => s.NControl == nControl);
            if (student != null)
            {
                Ctx.Students.Remove(student);
                Ctx.SaveChanges();
                return true;
            }
            else
            {
                throw new Exception(string.Format("Student with Control Number {0} does not exist.", nControl));
            }
        }

        public string GetData(int value)
        {
            return string.Format("You entered: {0}", value);
        }

        public CompositeType GetDataUsingDataContract(CompositeType composite)
        {
            if (composite == null)
            {
                throw new ArgumentNullException("composite");
            }
            if (composite.BoolValue)
            {
                composite.StringValue += "Suffix";
            }
            return composite;
        }

        public Student GetStudentByNControl(int nControl)
        {
            Student student = Ctx.Students.SingleOrDefault(s => s.NControl == nControl);
            return student;
        }

        public List<Student> GetStudents()
        {
            List<Student> students = Ctx.Students.ToList();
            return students;
        }

        public List<Student> GetStudentsByName(string searchName)
        {
            List<Student> students = Ctx.Students.Where(s => s.Name.Contains(searchName)).ToList();
            return students;
        }

        public bool InsertStudent(Student student)
        {
            if (student != null)
            {
                if (string.IsNullOrWhiteSpace(student.Name))
                {
                    throw new Exception("Student's Name cannot be empty.");
                }
                else
                {
                    Ctx.Students.Add(student);
                    Ctx.SaveChanges();
                    return true;
                }
            }
            else
            {
                throw new Exception("Student cannot be empty.");
            }
        }

        public bool UpdateStudent(Student student)
        {
            if (student != null)
            {
                if (string.IsNullOrWhiteSpace(student.Name))
                {
                    throw new Exception("Student's Name cannot be empty.");
                }
                else
                {
                    Student tmpStudent = Ctx.Students.SingleOrDefault(s => s.NControl == student.NControl);
                    if (tmpStudent != null)
                    {
                        tmpStudent.Name = student.Name;
                        tmpStudent.Topic = student.Topic;
                        Ctx.SaveChanges();
                        return true;
                    }
                    else
                    {
                        throw new Exception(string.Format("Student with Control Number {0} does not exist.", student.NControl));
                    }
                }
            }
            else
            {
                throw new Exception("Student cannot be empty.");
            }
        }
    }
}
