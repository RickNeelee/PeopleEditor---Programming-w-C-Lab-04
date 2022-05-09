using PeopleEditor.ViewModels;
using System;
using System.Collections.Generic;
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
using System.Windows.Shapes;

namespace PeopleEditor
{
    /// <summary>
    /// Логика взаимодействия для EditingPeople.xaml
    /// </summary>
    public partial class EditingPeople : Window
    {
        public EditingPeople()
        {
            InitializeComponent();
            EditingViewModel one = new EditingViewModel();
            DataContext = one;
            if (one.CloseAction == null)
            {
               one.CloseAction = new Action(this.Close);
            }
        }

        public EditingPeople(Person person)
        {
            InitializeComponent();
            EditingViewModel one = new EditingViewModel(person);
            DataContext = one;
            if (one.CloseAction == null)
            {
                one.CloseAction = new Action(this.Close);
            }
        }
    }
}
