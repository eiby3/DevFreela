using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevFreela.Application.InputModels
{
    public class NewProjectInputModel
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public int idClient { get; set; }
        public int idFreelancer { get; set; }
        public decimal TotalCost { get; set; }
    }
}
