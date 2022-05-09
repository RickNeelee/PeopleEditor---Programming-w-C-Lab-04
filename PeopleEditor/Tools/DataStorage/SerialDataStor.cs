
using PeopleEditor.Tools.Managers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PeopleEditor.Tools.DataStorage
{
    class SerialDataStor : IDataStor
    {
        private List<Person> _people;

        internal SerialDataStor()
        {
            try
            {
                _people = SerializationManager.Deserialize<List<Person>>(FileFolderManager.StorageFilePath);
            }
            catch (FileNotFoundException)
            {
                _people = new List<Person>();
                FillWithInitialPersons();
                SaveChanges();
            }
        }
        private void FillWithInitialPersons()
        {
            Random rand = new Random();
            for (int i = 0; i < 50; i++)
            {
                string[] names = new string[] { "Test","Yehor", "Lizuha", "Danya", "Andrew", "Ksenya", "Solomiya", "Kasya", "Zhenya", "Artem", "Sonya", "Sofa", "Eva" ,
                    "Rick", "Ihor", "Marina", "Dima", "Dusia", "Anna", "Rostyslav", "Nadia", "Max", "Vlad", "Vova", "Dasha",
                    "Anton", "Yarik", "Ruslan", "Demian", "Vania", "Kyrylo", "Alina", "Mykyta", "Alisa", "Julia", "Ilya", "Hlib",
                    "Blake", "Sam", "Olby", "Adrian", "Baxter", "Ruth", "Phoebe", "Mortimer", "Meg", "Robin", "Heather", "Isaac",
                    "Brooke", "Konstrakta", "Oleh", "Michael", "Andromache", "Monika", "Kristian", "Filip", "Keith", "Jim", "Amanda", "Chanel", "Natashka"};
                string[] surnames = new string[] { "Test","Kyrylin", "Sydorova", "Ustymenko", "Chernikov", "Vilkhova", "Yaremko", "Kriuchkova", "Kyrychenko", "Hromakovskiy", "Budikova", "Boldeskul", "Ryvkina" ,
                    "Neelee", "Zhuk", "Radzievska", "Pozharov", "Evdokimova", "Khmelnytska", "Skidchenko", "Khodakivska", "Repetskii", "Zholudenko", "Novak", "Yakovleva",
                    "Vakhitov", "Tymoshenko", "Hrabovskii", "Bilyavskii", "Sholudko", "Salata", "Yablonskii", "Bebra", "Lakhtiuk", "Kruta", "Nekrasova", "Hryshko",
                    "Hayden Stone", "Rose", "White", "Black", "Lively", "Ali", "Buffier", "Kardashian", "Jenner", "Gordienko", "Eilish", "Lipa",
                    "Pavlenko", "Scullion", "Psiuk", "Ben David", "York", "Liu", "Ochman", "Vudic", "Sub", "Woolfer", "van Stijn", "Terrero", "Spustymenko"};
                string[] words = new string[] { "test","hype", "eurovision", "cool", "hard", "mold", "join", "hero", "villain", "util", "kmc", "drug", "oolong" ,
                    "code", "key", "mohyla", "school", "phone", "int", "string", "esc", "date", "oop", "shake", "revo",
                    "kek", "jasaesj", "ukma", "rick", "neelee", "halo", "stefania", "slomo", "sentimentai", "rich", "diepte", "river",
                    "saudade", "rockstar", "llamame", "fulenn", "hope", "snap", "together", "banana", "eatyoursalad", "ela", "incorporesano", "disko",
                    "lightsoff", "missyou", "seoul", "lisbon", "kyiv", "shum", "sugar", "eldiablo", "matahari", "amnesia", "omaga", "tiktok"};
                //Random rnd = new Random();
                int index_nam = rand.Next(1,names.Length);
                int index_sur = rand.Next(1, surnames.Length);
                int index_char = rand.Next(1, words.Length);
                AddPerson(new Person($"{names[index_nam]}",
                $"{surnames[index_sur]}",
                $"{words[index_char]}{rand.Next(1, 101)}@coolhouse.com",
                    new DateTime(rand.Next(1970, 2021), rand.Next(1, 13), rand.Next(1, 27))));
            }

        }

        public List<Person> PeopleList
        {
            get
            {
                return _people.ToList();

            }
            set
            {
                _people = value;

            }
        }

        public void EditPerson(Person prevPerson, Person resPerson)
        {
            if (canAddOrChange(resPerson))
                _people[_people.IndexOf(prevPerson)] = resPerson;
            else throw new ArgumentException("Invalid Value!");
        }
        public void SaveChanges()
        {
            SerializationManager.Serialize(_people, FileFolderManager.StorageFilePath);
        }

        public void AddPerson(Person person)
        {
            if (canAddOrChange(person))
            {
                _people.Add(person);
                SaveChanges();
            }
            else throw new ArgumentException("Invalid Value!");
        }

        public void DeletePerson(Person person)
        {
            _people.Remove(person);
            SaveChanges();
        }

        private bool canAddOrChange(Person p)
        {
            return !string.IsNullOrWhiteSpace(p.Name) && !string.IsNullOrWhiteSpace(p.Surname) && !string.IsNullOrWhiteSpace(p.Email);
        }
    }
}

