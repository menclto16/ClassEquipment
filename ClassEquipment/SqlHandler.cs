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
        private static string classroomsDatabasePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "ClassroomEquipment.db");
        SQLiteConnection db = new SQLiteConnection(classroomsDatabasePath);

        public SqlHandler()
        {
            DbCreation();
        }
        public void DbCreation()
        {
            db.CreateTable<Classroom>();
            db.CreateTable<Equipment>();
        }
        public List<Classroom> GetClassroomsFromDb()
        {
            var query = db.Table<Classroom>();
            List<Classroom> results = query.ToList();
            return results;
        }
        public List<Equipment> GetEquipmentFromDb(int classroomIDInput = -1, int idInput = -1)
        {
            if (classroomIDInput != -1)
            {
                var query = db.Table<Equipment>().Where(v => v.ClassroomID.Equals(classroomIDInput));
                List<Equipment> results = query.ToList();
                return results;
            }
            else if(idInput != -1)
            {
                var query = db.Table<Equipment>().Where(v => v.ID.Equals(idInput));
                List<Equipment> results = query.ToList();
                return results;
            } else
            {
                var query = db.Table<Equipment>();
                List<Equipment> results = query.ToList();
                return results;
            }
            
        }
        public void AddClassroom(int classroomID)
        {
            var classroom = new Classroom()
            {
                ClassroomID = classroomID
            };
            db.Insert(classroom);
        }
        public void AddEquipment(int classroomIDInput, string nameInput, int valueInput)
        {
            var equipment = new Equipment()
            {
                ClassroomID = classroomIDInput,
                Name = nameInput,
                Value = valueInput
            };
            db.Insert(equipment);
        }
        public void UpdateEquipment(int idInput, int classroomIDInput, string nameInput, int valueInput)
        {
            var equipment = new Equipment()
            {
                ID = idInput,
                ClassroomID = classroomIDInput,
                Name = nameInput,
                Value = valueInput
            };
            db.Update(equipment);
        }
        public void DeleteClassroom(int classroomID)
        {
            var query1 = db.Table<Classroom>().Delete(v => v.ClassroomID.Equals(classroomID));
            var query2 = db.Table<Equipment>().Delete(v => v.ClassroomID.Equals(classroomID));
        }
        public void DeleteEquipment(int id)
        {
            var query = db.Table<Equipment>().Delete(v => v.ID.Equals(id));
        }
    }
}
