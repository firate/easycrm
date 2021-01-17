using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace EasyCRM.Entity.Models
{
    public class Address
    {
        public Address()
        {
            SalesOrderBillToAddress = new List<SalesOrder>();
            SalesOrderShipToAddress = new List<SalesOrder>();
        }
        public int Id { get; set; }
        public string AddressTitle { get; set; }
        public string AddressLine { get; set; }
        public string State { get; set; }
        public string PostalCode { get; set; }

        [Column("AccountID")]
        public int AccountId { get; set; }

        [ForeignKey(nameof(AccountId))]
        public Account Account { get; set; }

        [Column("CountryID")]
        public int CountryId { get; set; }

        [ForeignKey(nameof(CountryId))]
        public Country Country { get; set; }

        public bool IsMain { get; set; }

        [InverseProperty(nameof(Contact.SelectedAddress))]
        public List<Contact> Contacts { get; set; }

        [InverseProperty(nameof(SalesOrder.BillToAddress))]
        public List<SalesOrder> SalesOrderBillToAddress { get; set; }

        [InverseProperty(nameof(SalesOrder.ShipToAddress))]
        public List<SalesOrder> SalesOrderShipToAddress { get; set; }
    }
}
