using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Case.Models
{
    public class Transaction : EntityBase
    {
        public long TransactionId { get; set; }
        public string MerchantCnpj { get; set; }
        public long CheckoutCode { get; set; }
        public string CipheredCardNumber { get; set; }
        public long AmountInCent { get; set; }
        public long Installments { get; set; }
        public string AcquirerName { get; set; }
        public string PaymentMethod { get; set; }
        public string CardBrandName { get; set; }
        public string Status { get; set; }
        public string StatusInfo { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime AcquirerAuthorizationDateTime { get; set; }
    }
}
