using JetBrains.Annotations;
using PeopleEditor.Tools.Managers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace PeopleEditor.ViewModels
{
    internal class PeopleEditorViewModel : INotifyPropertyChanged
    {
        #region Fields

        private ObservableCollection<Person> _people;
        private Person _chosenPerson;

        #region Commands
        private RelayCommand<object> _edit;
        private RelayCommand<object> _add;
        private RelayCommand<object> _delete;
        private RelayCommand<object> _save;
        private RelayCommand<object> _sortName;
        private RelayCommand<object> _sortSurname;
        private RelayCommand<object> _sortBirthday;
        private RelayCommand<object> _sortEmail;
        private RelayCommand<object> _sortWestern;
        private RelayCommand<object> _sortChinese;
        private RelayCommand<object> _sortIsAdult;
        #endregion
        #endregion

        #region Properties
        public Person ChosenPerson
        {
            get 
            { 
                return _chosenPerson; 
            }
            set
            {
                _chosenPerson = value;
            }
        }

        public ObservableCollection<Person> People
        {
            get 
            { 
                return _people; 
            }
            private set
            {
                _people = value;
                OnPropertyChanged();
            }
        }
        

        public RelayCommand<object> Add
        {
            get
            {
                return _add ?? (_add = new RelayCommand<object>(
                           Adding));
            }
        }

        public RelayCommand<object> Delete
        {
            get
            {
                return _delete ?? (_delete = new RelayCommand<object>(
                           Deleting, o => IsAbleToSubmit()));
            }
        }

        public RelayCommand<object> Edit
        {
            get
            {
                return _edit ?? (_edit = new RelayCommand<object>(
                           Editing, o => IsAbleToSubmit()));
            }
        }

        public RelayCommand<object> Save
        {
            get
            {
                return _save ?? (_save = new RelayCommand<object>(
                           Saving));
            }
        }

        public RelayCommand<object> SortFirstName
        {
            get
            {
                return _sortName ?? (_sortName = new RelayCommand<object>(o =>
                    Sorting(o, 1)));
            }
        }
        public RelayCommand<object> SortLastName
        {
            get
            {
                return _sortSurname ?? (_sortSurname = new RelayCommand<object>(o =>
                           Sorting(o, 2)));
            }
        }
        public RelayCommand<object> SortEmail
        {
            get
            {
                return _sortEmail ?? (_sortEmail = new RelayCommand<object>(o =>
                           Sorting(o, 3)));
            }
        }
        public RelayCommand<object> SortBirthday
        {
            get
            {
                return _sortBirthday ?? (_sortBirthday = new RelayCommand<object>(o =>
                           Sorting(o, 4)));
            }
        }
        public RelayCommand<object> SortWestern
        {
            get
            {
                return _sortWestern ?? (_sortWestern = new RelayCommand<object>(o =>
                           Sorting(o, 5)));
            }
        }
        public RelayCommand<object> SortChinese
        {
            get
            {
                return _sortChinese ?? (_sortChinese = new RelayCommand<object>(o =>
                           Sorting(o, 6)));
            }
        }
        public RelayCommand<object> SortIsAdult
        {
            get
            {
                return _sortIsAdult ?? (_sortIsAdult = new RelayCommand<object>(o =>
                           Sorting(o, 7)));
            }
        }

        #endregion
        private void Adding(object obj)
        {

            EditingPeople window = new EditingPeople();
            window.ShowDialog();

            People = new ObservableCollection<Person>(StationManager.DataStorage.PeopleList);
        }

        

        private async void Deleting(object obj)
        {
            await Task.Run(() =>
            {
                if (MessageBox.Show($"Delete {_chosenPerson}?",
                "Delete?",
                    MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
                {
                    StationManager.DataStorage.DeletePerson(_chosenPerson);
                    _chosenPerson = null;
                    People = new ObservableCollection<Person>(StationManager.DataStorage.PeopleList);
                }
            });
            Thread.Sleep(400);

        }


       

        private void Editing(object obj)
        {

            EditingPeople window = new EditingPeople(_chosenPerson);
            window.ShowDialog();

            People = new ObservableCollection<Person>(StationManager.DataStorage.PeopleList);
        }
 

        private async void Sorting(object obj, int i)
        {
            await Task.Run(() =>
            {
                IOrderedEnumerable<Person> sortedPeople;
                switch (i)
                {
                    case 1:
                        sortedPeople = from u in _people
                                        orderby u.Name
                                        select u;
                        break;
                    case 2:
                        sortedPeople = from u in _people
                                        orderby u.Surname
                                        select u;
                        break;
                    case 3:
                        sortedPeople = from u in _people
                                        orderby u.Email
                                        select u;
                        break;
                    case 4:
                        sortedPeople = from u in _people
                                        orderby u.Birthdate
                                        select u;
                        break;
                    case 5:
                        sortedPeople = from u in _people
                                        orderby u.SunSign
                                        select u;
                        break;
                    case 6:
                        sortedPeople = from u in _people
                                        orderby u.ChineseSign
                                        select u;
                        break;
                    case 7:
                        sortedPeople = from u in _people
                                        orderby u.IsAdult
                                        select u;
                        break;
                    default:
                        sortedPeople = from u in _people
                                        orderby u.IsBirthday
                                        select u;
                        break;
                }
                People = new ObservableCollection<Person>(sortedPeople);
                StationManager.DataStorage.PeopleList = People.ToList();
                Thread.Sleep(300);
            });
        }

        private async void Saving(object obj)
        {
            await Task.Run(() =>
            {
                StationManager.DataStorage.SaveChanges();
                Thread.Sleep(150);
            });
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        internal PeopleEditorViewModel()
        {
            _people = new ObservableCollection<Person>(StationManager.DataStorage.PeopleList);
        }

        private bool IsAbleToSubmit()
        {
            return _chosenPerson != null;
        }

    }
}

