using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pharmacy_IS.Model.Entities
{
    public class Medicament
    {

        public string MedName { get; set; }
        public int ManufacturerId { get; set; }
        public int Amount { get; set; }
        public string MedType { get; set; }
        public string ActiveIngredients { get; set; }
        public bool IsPrescribed { get; set; }
        public string Description { get; set; }

        public Medicament(string medName, int manufacturerId, int amount, string medType, string activeIngredients, bool isPrescribed, string description)
        {
            MedName = medName;
            ManufacturerId = manufacturerId;
            Amount = amount;
            MedType = medType;
            ActiveIngredients = activeIngredients;
            IsPrescribed = isPrescribed;
            Description = description;
        }

        public Medicament()
        {
        }
    }
}
