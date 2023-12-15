using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Xml.Serialization;

namespace StudentInformationEntry
{
    public partial class StudentInformation : Page
    {
        private ObservableCollection<Person> student = new ObservableCollection<Person>();
        private const string DataFileName = "peopleData.xml";

        public ObservableCollection<Person> Student
        {
            get { return student; }
        }

        public StudentInformation()
        {
            InitializeComponent();
            LoadStudentData();
            lstPeople.ItemsSource = Student;
        }

        private void CreateButton_Click(object sender, RoutedEventArgs e)
        {
            string firstName = txtFirstName.Text;
            string lastName = txtLastName.Text;
            string sex = (Gender.SelectedItem as ComboBoxItem)?.Content as string;

            if (!string.IsNullOrWhiteSpace(firstName) && !string.IsNullOrWhiteSpace(lastName) && !string.IsNullOrWhiteSpace(sex))
            {
                if (int.TryParse(intAge.Text, out int age) && age > 0) 
                {
                    Student.Add(new Person { FirstName = firstName, LastName = lastName, Sex = sex, Age = age });
                    ClearInputFields();
                    SaveStudentData();
                }
                else
                {
                    MessageBox.Show("Please enter valid age.");
                }
            }
            else
            {
                MessageBox.Show("Please enter both first name, last name and select gender.");
            }
        }

        private void UpdateButton_Click(object sender, RoutedEventArgs e)
        {
            Button btn = (Button)sender;
            Person selectedPerson = (Person)btn.DataContext;
            UpdateStudentWindow updateStudentWindow = new UpdateStudentWindow();

            updateStudentWindow.txtFirstName.Text = selectedPerson.FirstName;
            updateStudentWindow.txtLastName.Text = selectedPerson.LastName;
            updateStudentWindow.intAge.Text = selectedPerson.Age.ToString();
            updateStudentWindow.Gender = selectedPerson.Sex;

            if (updateStudentWindow.ShowDialog() == true)
            {
                selectedPerson.FirstName = updateStudentWindow.FirstName;
                selectedPerson.LastName = updateStudentWindow.LastName;
                if (int.TryParse(updateStudentWindow.intAge.Text, out int age))
                {
                    selectedPerson.Age = age;
                }
                else
                {
   
                    MessageBox.Show("Invalid age input. Please enter a valid integer.");
                    return; 
                }
                selectedPerson.Sex = updateStudentWindow.Gender;

                SaveStudentData();
            }
            LoadStudentData();

        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            Button btn = (Button)sender;
            Person selectedPerson = (Person)btn.DataContext;
            Student.Remove(selectedPerson);

            SaveStudentData();
            LoadStudentData();
        }

        private void ClearInputFields()
        {
            txtFirstName.Clear();
            txtLastName.Clear();
            intAge.Clear();
            Gender.SelectedItem = null;
        }

        private void SaveStudentData()
        {
            using (FileStream fileStream = new FileStream(DataFileName, FileMode.Create))
            {
                XmlSerializer serializer = new XmlSerializer(typeof(List<Person>));
                serializer.Serialize(fileStream, Student.ToList());
            }
        }

        private void LoadStudentData()
        {
            if (File.Exists(DataFileName))
            {
                using (FileStream fileStream = new FileStream(DataFileName, FileMode.Open))
                {
                    XmlSerializer serializer = new XmlSerializer(typeof(List<Person>));
                    var loadedStudent = (List<Person>)serializer.Deserialize(fileStream);

                    Student.Clear();
                    foreach (var person in loadedStudent)
                    {
                        Student.Add(person);
                    }
                }
            }
        }

        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            string searchText = txtSearch.Text.ToLower();

            if (!string.IsNullOrWhiteSpace(searchText))
            {
                IEnumerable<Person> searchResults = Student
                    .Where(person =>
                        person.FirstName.ToLower().Contains(searchText) ||
                        person.LastName.ToLower().Contains(searchText));

                lstPeople.ItemsSource = new ObservableCollection<Person>(searchResults);
            }
            else
            {
                lstPeople.ItemsSource = Student;
            }
        }

    }

    public class Person
    {
        private string firstName;
        private string lastName;
        private string sex;
        private int age;

        public string FirstName
        {
            get { return firstName; }
            set { firstName = value ?? throw new ArgumentNullException(nameof(FirstName)); }
        }

        public string LastName
        {
            get { return lastName; }
            set { lastName = value ?? throw new ArgumentNullException(nameof(LastName)); }
        }

        public string Sex
        {
            get { return sex; }
            set { sex = value ?? throw new ArgumentNullException(nameof(Sex)); }
        }

        public int Age
        {
            get { return age; }
            set { age = value; }
        }

    }
}
