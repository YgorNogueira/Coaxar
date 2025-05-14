using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace CoaxarApp.ViewModels
{
    public class CoaxarViewModel
    {
        public ObservableCollection<AnimalModel> Animals { get; set; } = new ObservableCollection<AnimalModel>();

        public IEnumerable<string> Families => Animals
            .GroupBy(Animals => Animals.Family)
            .Select(group => group.Key)
            .Where(family => !string.IsNullOrEmpty(family));
    }
}
