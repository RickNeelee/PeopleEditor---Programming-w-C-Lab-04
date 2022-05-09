using JetBrains.Annotations;
using PeopleEditor.Tools.Managers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace PeopleEditor.ViewModels
{
   class EditingViewModel : INotifyPropertyChanged
    {
        #region Fields
        private Person _person = null;
        private string _name;
        private string _surname;
        private string _email;
        private DateTime _birthdate = DateTime.Today;
        #endregion

        #region Commands
        private RelayCommand<object> _submitCommand;
        private RelayCommand<object> _cancelCommand;
        #endregion

        internal EditingViewModel(Person person)
        {
            _person = person;
            Name = person.Name;
            Surname = person.Surname;
            Email = person.Email;
            Birthdate = person.Birthdate;
        }

        public string Name
        {
            get { return _name; }
            set
            {
                _name = value;
                OnPropertyChanged();
            }
        }

        public Action CloseAction { get; set; }

        public string Surname
        {
            get { return _surname; }
            set
            {
                _surname = value;
                OnPropertyChanged();
            }
        }

        public string Email
        {
            get 
            { return _email; }
            set
            {
                _email = value;
                OnPropertyChanged();
            }
        }

        internal EditingViewModel()
        {
        }

        public DateTime Birthdate
        {
            get 
            { return _birthdate; }
            set
            {
                _birthdate = value;
                OnPropertyChanged();
            }
        }
        public RelayCommand<object> Submit
        {
            get
            {
                return _submitCommand ?? (_submitCommand = new RelayCommand<object>(
                    Submitting, o => IsAbleToSubmit()));
            }
        }

        public RelayCommand<object> Close
        {
            get
            {
                return _cancelCommand ?? (_cancelCommand = new RelayCommand<object>(
                           Cancelling, o => IsAbleToSubmit()));
            }
        }

        private void Submitting(object obj)
        {
            try
            {
                if (_person != null)
                {

                    if (MessageBox.Show("Submit changes?", "Edit?",
                            MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
                    {
                        StationManager.DataStorage.EditPerson(_person, new Person(Name, Surname, Email, Birthdate));
                        CloseAction();
                    }
                }
                else
                {
                    StationManager.DataStorage.AddPerson(new Person(Name, Surname, Email, Birthdate));
                    CloseAction();
                }

            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }


        private void Cancelling(object obj)
        {
            try
            {
                if (MessageBox.Show("Are you sure?", "Cancel?",
                            MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
                    CloseAction();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private bool IsAbleToSubmit()
        {
            return !string.IsNullOrWhiteSpace(_name) && !string.IsNullOrWhiteSpace(_surname) && !string.IsNullOrWhiteSpace(_email);
        }

    }
}

