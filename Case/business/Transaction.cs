using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Case.Data
{
    public class Transaction
    {
        [Key]
        public long TransactionId  { get; set; }
        public string MerchantCnpj;
        public long CheckoutCode;
        public string CipheredCardNumber;
        public long AmountInCents;
        public long Installments;
        public string AcquirerName;
        public string PaymentMethod;
        public string CardBrandName;
        public string Status;
        public string StatusInfo;
        public DateTime CreatedAt;
        public DateTime AcquirerAuthorizationDateTime;
    }
}
