using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pharmacy_IS.Model.Entities
{
    public class StoredItem
    {
        public int Id { get; set; }
        public int MedicamentId { get; set; }
        public int Quantity { get; set; }
        public DateTime ExpirationDate { get; set; }

        public StoredItem(int id, int medicamentId, int quantity, DateTime expirationDate)
        {
            Id = id;
            MedicamentId = medicamentId;
            Quantity = quantity;
            ExpirationDate = expirationDate;
        }

        public StoredItem()
        {
        }
    }
}
