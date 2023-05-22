using Hospital.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace Hospital.Model.DB
{
    public class ChangeDB
    {
        private Command _command;
        public ChangeDB()
        {
            _command = new Command();
        }

        #region Get
        public ObservableCollection<Doctor> GetDoctor()
        {
            List<Doctor> temp;
            DataTable _dataTemp = new DataTable();
            _command.InitialTable(out _dataTemp, "Select * from Doctor");
            temp = (from DataRow dr in _dataTemp.Rows
                    select new Doctor()
                    {
                        Id = int.Parse(dr["Id"].ToString()),
                        Name = dr["Name"].ToString(),
                        Surname = dr["Surname"].ToString(),
                    }).ToList();

            return  new ObservableCollection<Doctor>(temp);
        }
        public ObservableCollection<Document> GetDocuments()
        {
            List<Document> temp;
            DataTable _dataTemp = new DataTable();
            _command.InitialTable(out _dataTemp, "Select * from Document");
            temp = (from DataRow dr in _dataTemp.Rows
                    select new Document()
                    {
                        Id = int.Parse(dr["Id"].ToString()),
                        Type = dr["Type"].ToString(),
                        Diagnosis = dr["Diagnosis"].ToString(),
                        DateStart = DateTime.Parse(dr["DateOfIssue"].ToString()),
                        DateEnd2 = DateTime.Parse(dr["DateEnd"].ToString())
                    }).ToList();

            return new ObservableCollection<Document>(temp);
        }
        public ObservableCollection<Patient> GetPatients()
        {
            List<Patient> temp;
            DataTable _dataTemp = new DataTable();
            _command.InitialTable(out _dataTemp, "Select * from Patient");
            temp = (from DataRow dr in _dataTemp.Rows
                    select new Patient()
                    {
                        Id = int.Parse(dr["Id"].ToString()),
                        Name = dr["Name"].ToString(),
                        Surname = dr["Surname"].ToString(),
                        Patronymic = dr["Patronymic"].ToString(),
                        Date = DateTime.Parse(dr["DateOfBirth"].ToString()),
                        Gender = dr["Gender"].ToString(),
                        Adress = dr["Adress"].ToString(),
                        Number = dr["Number"].ToString()
                    }).ToList();

            return new ObservableCollection<Patient>(temp);
        }
        public ObservableCollection<Roles> GetRoles()
        {
            List<Roles> temp;
            DataTable _dataTemp = new DataTable();
            _command.InitialTable(out _dataTemp, "Select * from Roles");
            temp = (from DataRow dr in _dataTemp.Rows
                    select new Roles()
                    {
                        Id = int.Parse(dr["Id"].ToString()),
                        Name = dr["Name"].ToString(),
                    }).ToList();

            return new ObservableCollection<Roles>(temp);
        }
        public ObservableCollection<Statuses> GetStatuses()
        {
            List<Statuses> temp;
            DataTable _dataTemp = new DataTable();
            _command.InitialTable(out _dataTemp, "Select * from Statuses");
            temp = (from DataRow dr in _dataTemp.Rows
                    select new Statuses()
                    {
                        Id = int.Parse(dr["Id"].ToString()),
                        DoctorId = dr["DoctorId"] == DBNull.Value ? -1 : int.Parse(dr["DoctorId"].ToString()),
                        PatientId = dr["PatientId"] == DBNull.Value ? -1 : int.Parse(dr["PatientId"].ToString()),
                        DocumentId = dr["DocumentId"] == DBNull.Value ? -1 : int.Parse(dr["DocumentId"].ToString()),
                        DisabilityStatus = dr["DisabilityStatus"].ToString(),
                        DateStart = DateTime.Parse(dr["DateOfIssue"].ToString()),
                        DateEnd2 = DateTime.Parse(dr["DateEnd"].ToString())
                    }).ToList();

            return new ObservableCollection<Statuses>(temp);
        }
        public ObservableCollection<Users> GetUsers()
        {
            List<Users> temp;
            DataTable _dataTemp = new DataTable();
            _command.InitialTable(out _dataTemp, "Select * from Users");
            temp = (from DataRow dr in _dataTemp.Rows
                    select new Users()
                    {
                        Id = int.Parse(dr["Id"].ToString()),
                        RoleId = int.Parse(dr["RoleId"].ToString()),
                        Login = dr["Login"].ToString(),
                        Password = dr["Password"].ToString(),
                        DoctorId = dr["DoctorId"] == DBNull.Value ? -1 : int.Parse(dr["DoctorId"].ToString()),
                    }).ToList();

            return new ObservableCollection<Users>(temp);
        }
        #endregion

        public void AddAll(BaseEntity baseEntity)
        {
            _command.Execute(Add(baseEntity));
        }
        public void UpdateAll(BaseEntity baseEntity)
        {
            _command.Execute(Update(baseEntity));
        }
        public void RemoveAll(BaseEntity baseEntity)
        {
            _command.Execute(Remove(baseEntity));
        }

        #region Converter class to sql query

        private string Add(BaseEntity entity)
        {
            List<PropertyInfo> props = entity.GetType().GetProperties().ToList();

            string command = $"Insert into {entity.GetType().Name} (";

            for (int i = 0; i < props.Count(); i++)
            {
                if (!props[i].PropertyType.IsSubclassOf(typeof(BaseEntity)) && props[i].PropertyType != typeof(DateTime))
                    command += props[i].Name + ", ";
            }

            command = command.Remove(command.Length - 2, 2);
            command += ") values (";

            for (int i = 0; i < props.Count(); i++)
            {
                if (!props[i].PropertyType.IsSubclassOf(typeof(BaseEntity)) && props[i].PropertyType != typeof(DateTime))
                    if (props[i].GetValue(entity) == null)
                    {
                        command += "'null', ";
                    }
                    else 
                        command += "N'" + props[i].GetValue(entity) + "', ";
            }

            command = command.Remove(command.Length - 2, 2);
            command += ");";
            return command;
        }

        private string Remove(BaseEntity entity)
        {
            PropertyInfo[] props = entity.GetType().GetProperties();
            return $"Delete from {entity.GetType().Name} where Id = {props[props.Count() - 1].GetValue(entity)}";           
        }

        private string Update(BaseEntity entity)
        {
            List<PropertyInfo> props = entity.GetType().GetProperties().ToList();
            string command = $"Update {entity.GetType().Name} set ";

            foreach (PropertyInfo p in props)
            {
                if (!p.PropertyType.IsSubclassOf(typeof(BaseEntity)) && p.PropertyType != typeof(DateTime))
                    if (p.GetValue(entity) == null)
                    {
                        command += $"{p.Name} = null, ";
                    }
                    else
                        command += $"{p.Name} = N'" + p.GetValue(entity) + "', ";
            }
            command = command.Remove(command.Length - 2, 2);

            command += $" where Id = {props[props.Count() - 1].GetValue(entity)}";

            return command;
        }

        #endregion
    }
}
