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
        public string OrderNumber { get; set; }

        public DateTime OrderDate { get; set; }

        public string UserId { get; set; }

        public ApplicationUser User { get; set; }
        public OrderStatus OrderState { get; set; }

        public OrderPayment PaymentMethod { get; set; }


    }
}
