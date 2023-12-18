using Domain.Common;
using Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Order : EntityBase
    {
        public int Id { get; set; }

        public string OrderNumber { get; set; }

        public DateTime OrderDate { get; set; }

        public string UserId { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Address { get; set; }

        public string City { get; set; }

        public string PhoneNumber { get; set; }

        public OrderStatus OrderState { get; set; }

        public OrderPayment PaymentMethod { get; set; }


    }
}
