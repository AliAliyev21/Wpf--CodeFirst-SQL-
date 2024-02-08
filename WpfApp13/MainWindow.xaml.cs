using System;
using System.Collections.Generic;
using System.Data.Entity;
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
using WpfApp13.DataAccess;
using WpfApp13.Entities;

namespace WpfApp13
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            LoadData();
        }

        public async void LoadData()
        {
            List<Student> students = null;
            List<Course> courses = null;

            using (var context=new MyContext())
            {
                students=await context.Students.ToListAsync();
                if (students.Count == 0)
                {
                    context.Students.Add(new Student
                    {
                        Firstname="John",
                        Lastname="Johnlu"
                    });

                    context.Students.Add(new Student
                    {
                        Firstname = "Aysel",
                        Lastname = "Mammadova"
                    });

                    context.Students.Add(new Student
                    {
                        Firstname = "Mike",
                        Lastname = "Novruzlu"
                    });

                    context.Students.Add(new Student
                    {
                        Firstname = "Rafiq",
                        Lastname = "Rafiqli"
                    });

                    context.Students.Add(new Student
                    {
                        Firstname = "Ulvi",
                        Lastname = "Shabransky"
                    });

                    context.Courses.Add(new Course
                    {
                        CourseName = "Step IT Academy",
                        Address = "Koroglu Rahimov 70"
                    });

                    context.Courses.Add(new Course
                    {
                        CourseName = "Elvin Academy",
                        Address = "Port Baku Mall"
                    });

                    await context.SaveChangesAsync();

                    var studentsResult = context.Students.OrderBy(s => s.Id);

                    var s1=studentsResult.First();
                    var s2=studentsResult.Skip(1).First();
                    var s3=studentsResult.Skip(2).First();
                    var s4=studentsResult.Skip(3).First();
                    var s5=studentsResult.Skip(4).First();

                    var courseResult=context.Courses.OrderBy(c => c.Id);
                    var c1= courseResult.First();
                    var c2= courseResult.Skip(1).First();

                    s1.Courses.Add(c1);

                    s2.Courses.Add(c1);
                    s2.Courses.Add(c2);

                    s3.Courses.Add(c1);
                    s3.Courses.Add(c2);

                    s4.Courses.Add(c2);
                    s5.Courses.Add(c2);

                    await context.SaveChangesAsync();
                }

                students=await context.Students.ToListAsync();
                courses=await context.Courses.ToListAsync();

                studentGrid.ItemsSource= students;
                courseGrid.ItemsSource= courses;
            }
        }


        private async void courseGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var course = courseGrid.SelectedItem as Course;
            if(course != null)
            {
                using (var context = new MyContext())
                {
                    var courseItem= await context
                        .Courses
                        .Include(nameof(Course.Students))
                        .FirstOrDefaultAsync(c=>c.Id==course.Id);

                    if(courseItem != null)
                    {
                        var students= courseItem.Students.ToList();
                        studentGrid.ItemsSource= students;
                    }
                }
            }
        }
    }
}
