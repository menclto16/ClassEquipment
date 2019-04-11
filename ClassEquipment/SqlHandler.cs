using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;

namespace ClassEquipment
{
    public class SqlHandler
    {
        private string classroomsDatabasePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "ClassroomEquipment.db");

        public SqlHandler()
        {
            DbCreation();
        }
        public void DbCreation()
        {
            var db = new SQLiteConnection(classroomsDatabasePath);
            db.CreateTable<Classroom>();
            db.CreateTable<Equipment>();
        }
        public List<Classroom> GetClassroomsFromDb()
        {
            var db = new SQLiteConnection(classroomsDatabasePath);
            var query = db.Table<Classroom>();
            List<Classroom> results = query.ToList();
            return results;
        }
        public List<Equipment> GetEquipmentFromDb(int classroomID = -1)
        {
            if (classroomID != -1)
            {
                var db = new SQLiteConnection(classroomsDatabasePath);
                var query = db.Table<Equipment>().Where(v => v.ClassroomID.Equals(classroomID));
                List<Equipment> results = query.ToList();
                return results;
            }
            else
            {
                var db = new SQLiteConnection(classroomsDatabasePath);
                var query = db.Table<Equipment>();
                List<Equipment> results = query.ToList();
                return results;
            }
            
        }
        public void AddClassroom(int classroomID)
        {
            var db = new SQLiteConnection(classroomsDatabasePath);
            var classroom = new Classroom()
            {
                ClassroomID = classroomID
            };
            db.Insert(classroom);
        }
        public void AddEquipment(int classroomID, string name, int value)
        {
            var db = new SQLiteConnection(classroomsDatabasePath);
            var equipment = new Equipment()
            {
                ClassroomID = classroomID,
                Name = name,
                Value = value
            };
            db.Insert(equipment);
        }
        public void DeleteClassroom(int classroomID)
        {
            var db = new SQLiteConnection(classroomsDatabasePath);
            var query1 = db.Table<Classroom>().Delete(v => v.ClassroomID.Equals(classroomID));
            var query2 = db.Table<Equipment>().Delete(v => v.ClassroomID.Equals(classroomID));
        }
        public void DeleteEquipment(int id)
        {
            var db = new SQLiteConnection(classroomsDatabasePath);
            var query = db.Table<Equipment>().Delete(v => v.ID.Equals(id));
        }
        /*
        public List<Classroom> GetClassroomsFromDb(string class = "")
        {
            if (classroom != "")
            {
                var query = db.Table<Mark>().Where(v => v.Subject.Contains(subject));
                List<Mark> results = query.ToList();
                return results;
            }
            else
            {
                var query = db.Table<Mark>();
                List<Mark> results = query.ToList();
                return results;
            }
        }
        public List<Subject> GetSubjectsFromDb()
        {
            var db = new SQLiteConnection(marksDatabasePath);
            var query = db.Table<Subject>();
            List<Subject> results = query.ToList();
            return results;
        }
        public void AddMark(string subject, int value, int weight, string comment)
        {
            var db = new SQLiteConnection(marksDatabasePath);
            var mark = new Mark()
            {
                Subject = subject,
                Value = value,
                Weight = weight,
                Comment = comment
            };
            db.Insert(mark);
        }
        public void AddSubject(string name)
        {
            var db = new SQLiteConnection(marksDatabasePath);
            var subject = new Subject()
            {
                Name = name
            };
            try
            {
                db.Insert(subject);
            }
            catch (Exception)
            {
            }
        }
        */
    }
}
