using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MvvmExample
{
    public class ComputerScienceStudentViewModel  : INotifyPropertyChanged
    {
        /// <summary>
        /// Notifies when a property of this class changes.
        /// </summary>
        public event PropertyChangedEventHandler? PropertyChanged;

        /// <summary>
        /// The student that this class wraps.
        /// </summary>
        private Student _student;

        /// <summary>
        /// The student's first name.
        /// </summary>
        public string FirstName => _student.FirstName;

        /// <summary>
        /// The student's Last Name.
        /// </summary>
        public string LastName => _student.LastName;

        /// <summary>
        /// Course records.
        /// </summary>
        public IEnumerable<CourseRecord> CourseRecords => _student.CourseRecords;

        /// <summary>
        /// The Student's GPA.
        /// </summary>
        public double GPA => _student.GPA;

        /// <summary>The student's GPA</summary>
        public double ComputerScienceGPA
        {
            get
            {
                var points = 0.0;
                var hours = 0.0;
                foreach (var cr in _student.CourseRecords)
                {
                    if (cr.CourseName.Contains("CIS"))
                    {
                        points += (double)cr.Grade * cr.CreditHours;
                        hours += cr.CreditHours;
                    }
                }
                return points / hours;
            }
        }

        /// <summary>
        /// Handles the when a student property changes.
        /// </summary>
        /// <param name="sender">The object sending the event.</param>
        /// <param name="e">Information about the event.</param>
        private void HandleStudentPropertyChanged(object? sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(Student.GPA))
            {
                PropertyChanged?.Invoke(this, e);
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(ComputerScienceGPA)));
            }
        }

        /// <summary>
        /// Constructs a new ComptuerScienceStudnetViewModel.
        /// </summary>
        /// <param name="student">The student wrapped in this view.</param>
        public ComputerScienceStudentViewModel(Student student)
        {
            _student = student;
            student.PropertyChanged += HandleStudentPropertyChanged;
        }
    }
}
