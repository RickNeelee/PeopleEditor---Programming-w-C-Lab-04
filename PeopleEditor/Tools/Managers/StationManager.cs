using PeopleEditor.Tools.DataStorage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PeopleEditor.Tools.Managers
{
    internal static class StationManager
    {
        private static IDataStor _dataStorage;
        internal static IDataStor DataStorage
        {
            get { return _dataStorage; }
        }
        internal static void Initialize(IDataStor dataStorage)
        {
            _dataStorage = dataStorage;
        }

    }
}
