using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ClassEquipment
{
    /// <summary>
    /// Interakční logika pro MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        SqlHandler sqlHandler = new SqlHandler();
        public MainWindow()
        {
            InitializeComponent();

            UpdateClassroomList();
            UpdateEquipmentList();
        }
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if (ClassroomIDInput1.Text != "" & int.TryParse(ClassroomIDInput1.Text, out int n))
            {
                int classroomID = Int32.Parse(ClassroomIDInput1.Text);
                sqlHandler.AddClassroom(classroomID);

                UpdateClassroomList();
            }
        }
        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            
            if (ValueInput.Text != "" & int.TryParse(ClassroomIDInput2.Text, out int n1) & int.TryParse(ValueInput.Text, out int n2))
            {
                string name = EquipmentNameInput.Text;
                int value = Int32.Parse(ValueInput.Text);
                int classroomID = Int32.Parse(ClassroomIDInput2.Text);

                if (EditPlaceholder.Text != null)
                {
                    int id = Int32.Parse((string)EditPlaceholder.Text);
                    sqlHandler.UpdateEquipment(id, classroomID, name, value);

                    EditPlaceholder.Text = null;

                    EquipmentButton.Content = "Add Equipment";
                }
                else sqlHandler.AddEquipment(classroomID, name, value);

                UpdateEquipmentList();
            }
        }
        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            Button button = (Button)e.Source;
            int id = (int)button.DataContext;
            sqlHandler.DeleteClassroom(id);

            UpdateClassroomList();
            UpdateEquipmentList();
        }
        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            Button button = (Button)e.Source;
            int id = (int)button.DataContext;
            sqlHandler.DeleteEquipment(id);

            UpdateEquipmentList();
        }
        private void Button_Click_5(object sender, RoutedEventArgs e)
        {
            Button button = (Button)e.Source;
            int id = (int)button.DataContext;

            Equipment equipment = sqlHandler.GetEquipmentFromDb(idInput: id)[0];

            EquipmentNameInput.Text = equipment.Name;
            ClassroomIDInput2.Text = equipment.ClassroomID.ToString();
            ValueInput.Text = equipment.Value.ToString();
            EditPlaceholder.Text = id.ToString();
            EquipmentButton.Content = "Edit equipment";

            UpdateEquipmentList();
        }
        public void UpdateClassroomList()
        {
            List<Classroom> classrooms = sqlHandler.GetClassroomsFromDb();
            ClassroomList.ItemsSource = classrooms;
            ClassroomIDInput2.ItemsSource = classrooms;
        }
        public void UpdateEquipmentList()
        {
            List<Equipment> equipment = sqlHandler.GetEquipmentFromDb();
            EquipmentList.ItemsSource = equipment;
        }
    }
}
