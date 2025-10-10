using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoaxarApp
{
    public class CoaxarViewModel
    {
        public ObservableCollection<AnimalModel> Animals { get; set; } = new ObservableCollection<AnimalModel>();
    }
}
