using System;
using Microsoft.Win32;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Wpf1.Entities;
using System.IO;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Data.Entity.Core.Objects;

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

        private void GetEmployees()
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
            RewriteEmployee();
            DataGridEmployee.IsReadOnly = true;
            isDirty = true;

        }

        private void RewriteEmployee()
        {
            DataEntitiesEmployee = new bd0604Entities();
            ListEmployee.Clear();
            GetEmployees();
        }

            private void EditCommandBinding_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            DataGridEmployee.IsReadOnly = false;
            DataGridEmployee.BeginEdit();
            isDirty = false;
        }

        private void SaveCommandBinding_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            DataEntitiesEmployee.SaveChanges();
            App.Context.SaveChanges();
            isDirty = true;
            DataGridEmployee.IsReadOnly = true;
        }

        private void EditCommandBinding_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = !isDirty;
        }

        private void NewCommandBinding_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            {
                var employees = new Employee
                {
                    ID = -1,
                    Surname = "Не задано",
                    Name = "Не задано",
                    Patronymic = "Не задано",
                    Telephone = "Не задано",
                    Email = "Не задано",
                    BirthDate = new DateTime(2023,1,1),
                    TitleID = 1
                    
                };
                App.Context.Employee.Add(employees);
                try
                {
                    ListEmployee.Add(employees);
                    DataGridEmployee.ScrollIntoView(employees);
                    DataGridEmployee.SelectedIndex = DataGridEmployee.Items.Count - 1;
                    DataGridEmployee.Focus();
                    DataGridEmployee.IsReadOnly = false;
                    isDirty = false;
                }
                catch (Exception ex)
                {
                    throw new ApplicationException("Ошибка добавления нового сотрудника в контекст данных" + ex.ToString());
                }
            }

        }

        private void DeleteCommandBinding_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            var emp = DataGridEmployee.SelectedItem as Employee;
            if (emp != null)
            {
                MessageBoxResult result = MessageBox.Show("Удалить сотрудника: " +
               emp.Surname + " " + emp.Name + " " + emp.Patronymic,"Предупреждение", MessageBoxButton.OKCancel);
                if (result == MessageBoxResult.OK)
                {
                    DataEntitiesEmployee.Employee.Remove(emp);
                    DataGridEmployee.SelectedIndex =
                    DataGridEmployee.SelectedIndex == 0 ? 1 : DataGridEmployee.SelectedIndex - 1;
                    ListEmployee.Remove(emp);
                    DataEntitiesEmployee.SaveChanges();
                }
            }
            else
            {
                MessageBox.Show("Выберите строку для удаления");
            }

        }

        private void FindTitle_Click(object sender, RoutedEventArgs e)
        {
            DataEntitiesEmployee = new bd0604Entities();
            ListEmployee.Clear();

            Title title = ComboBoxTitle.SelectedItem as Title;
            var employees = DataEntitiesEmployee.Employee;
            var queryEmployee = from employee in employees
                                where employee.TitleID == title.ID
                                orderby employee.Surname
                                select employee;
            try
            {
                foreach (Employee emp in queryEmployee)
                {
                    ListEmployee.Add(emp);
                }
                DataGridEmployee.ItemsSource = ListEmployee;
            }
            catch
            {
                MessageBox.Show("Должность не выбрана",null, MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void FindSurname_Click(object sender, RoutedEventArgs e)
        {
            string surname = TextBoxSurname.Text;
            DataEntitiesEmployee = new bd0604Entities();
            ListEmployee.Clear();
            var employees = DataEntitiesEmployee.Employee;
            var queryEmployee = from employee in employees
                                where employee.Surname == surname
                                select employee;
            foreach (Employee emp in queryEmployee)
            {
                ListEmployee.Add(emp);
            }
            if (ListEmployee.Count > 0)
            {
                DataGridEmployee.ItemsSource = ListEmployee;
            }
            else
                MessageBox.Show("Сотрудник с фамилией '" + surname + "' не найден",
                "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Warning);
        }

        private void ComboBoxTitle_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //FindTitle.IsEnabled = true;
            //FindSurname.IsEnabled = false;
            //TextBoxSurname.Text = "";
        }

        private void TextBoxSurname_TextChanged(object sender, TextChangedEventArgs e)
        {
            //FindTitle.IsEnabled = false;
            //FindSurname.IsEnabled = true;
            //ComboBoxTitle.SelectedIndex = -1;
            //КАК ЖЕ Я ЛЮБЛЮ МЕТОДИЧКУ НИФИГА НЕ РАБОТАЕТ

        }
    }
}
