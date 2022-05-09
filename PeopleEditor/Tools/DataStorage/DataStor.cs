using System.Collections.Generic;

namespace PeopleEditor.Tools.DataStorage
{
    interface IDataStor
    {
        void AddPerson(Person person);
        void DeletePerson(Person person);
        void EditPerson(Person person, Person resPerson);
        void SaveChanges();
        List<Person> PeopleList { get; set; }
    }
}
