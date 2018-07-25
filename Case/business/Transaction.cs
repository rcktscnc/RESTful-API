using System;

namespace Case.Business
{
    public class Transaction
    {
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