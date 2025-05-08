using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoaxarApp
{
    public class AnimalModel
    {

        public required string Name { get; set; }

        public string? ScientificName { get; set; }

        public string? MorphDescription { get; set; }

        public string? Distribution { get; set; }

    }
}
