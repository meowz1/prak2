using System;
using System.Collections.Generic;
using System.IO;
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

namespace Ejednevnik
{

    public partial class MainWindow : Window
    {
        public static Timetable timetable;
        public static DateTime selectedDate = DateTime.Today;
        
        public MainWindow()
        {
            InitializeComponent();
            timetable = new Timetable(selectedDate);
            updater();
            dateContainer.SelectedDate = selectedDate;
            
        }
        public void updater()
        {
            selectedDate = timetable.selectedDate;
            TaskContainer.Items.Clear();
            timetable.RefreshNotes();
            foreach (Note note in timetable.todayNotes)
            {
                TaskContainer.Items.Add(note.title);
            }
            titleBox.Text = "";
            descBox.Text = "";
        }

        private void creater(object sender, RoutedEventArgs e)
        {
            string title = titleBox.Text;
            string desc = descBox.Text;
            timetable.NewNote(title, desc, selectedDate);
            updater();
        }

        private void saves(object sender, RoutedEventArgs e)
        {
            string title = titleBox.Text;
            string desc = descBox.Text;
            timetable.EditNote(title, desc, selectedDate);
            updater();
        }

        private void izmenit(object sender, SelectionChangedEventArgs e)
        {
            if (TaskContainer.SelectedIndex != -1)
            {
                timetable.selectedTaskId = TaskContainer.SelectedIndex;
                Note selectedNote = timetable.todayNotes[timetable.selectedTaskId];
                titleBox.Text = selectedNote.title;
                descBox.Text = selectedNote.description;
            }
        }
        private void selecter(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                timetable.selectedDate = Convert.ToDateTime(dateContainer.Text);
                updater();
            }
            catch (NullReferenceException ex)
            {

            }
        }
        private void deleter(object sender, RoutedEventArgs e)
        {
            timetable.DeleteNote(todayId: timetable.selectedTaskId);
            updater();
        }
    }
}
