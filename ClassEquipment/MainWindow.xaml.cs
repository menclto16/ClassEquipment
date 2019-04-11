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
            int classroomID = Int32.Parse(ClassroomIDInput1.Text);
            sqlHandler.AddClassroom(classroomID);

            UpdateClassroomList();
        }
        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            string name = EquipmentNameInput.Text;
            int value = Int32.Parse(ValueInput.Text);
            int classroomID = Int32.Parse(ClassroomIDInput2.Text);
            sqlHandler.AddEquipment(classroomID, name, value);

            UpdateEquipmentList();
        }
        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            Button button = (Button)e.Source;
            int id = (int)button.DataContext;
            sqlHandler.DeleteClassroom(id);

            UpdateClassroomList();
        }
        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            Button button = (Button)e.Source;
            int id = (int)button.DataContext;
            sqlHandler.DeleteEquipment(id);

            UpdateEquipmentList();
        }
        public void UpdateClassroomList()
        {
            List<Classroom> classrooms = sqlHandler.GetClassroomsFromDb();
            ClassroomList.ItemsSource = classrooms;
        }
        public void UpdateEquipmentList()
        {
            List<Equipment> equipment = sqlHandler.GetEquipmentFromDb();
            EquipmentList.ItemsSource = equipment;
        }
    }
}
