using System.Collections.ObjectModel;
using System.Data.Entity.Core.Objects;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Wpf1.Entities;

namespace Wpf1
{
    /// <summary>
    /// Логика взаимодействия для PageEmployee.xaml
    /// </summary>
    public partial class PageEmployee : Page
    {
        public class ListTitle : ObservableCollection<Title>
        {
            public ListTitle()
            {
                var titles = PageEmployee.DataEntitiesEmployee.Title;
                var queryTitle = from title in titles select title;
                foreach (Title titl in queryTitle)
                {
                    this.Add(titl);
                }
            }
        }
        public static bd0604Entities DataEntitiesEmployee { get; set; }
        public ObservableCollection<Employee> ListEmployee { get; }

        public PageEmployee()
        {
            DataEntitiesEmployee = new bd0604Entities();
            InitializeComponent();
            ListEmployee = new ObservableCollection<Employee>();

        }
        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            var employees = DataEntitiesEmployee.Employee;
            var queryEmployee = from employee in employees
                                orderby employee.Surname
                                select employee;
            foreach (Employee emp in queryEmployee)
            {
                ListEmployee.Add(emp);
            }
            DataGridEmployee.ItemsSource = ListEmployee;
        }

        private bool isDirty = true;
        private void UndoCommandBinding_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            MessageBox.Show("Отмена");
            isDirty = true;
        }
        private void EditCommandBinding_Executed(object sender,ExecutedRoutedEventArgs e)
        {
            DataGridEmployee.IsReadOnly = false;
            DataGridEmployee.BeginEdit();
            isDirty = false;
        }

        private void SaveCommandBinding_Executed(object sender,ExecutedRoutedEventArgs e)
        {
            DataEntitiesEmployee.SaveChanges();
            isDirty = true;
            DataGridEmployee.IsReadOnly = true;
        }

        private void EditCommandBinding_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = !isDirty;
        }
    }
}
