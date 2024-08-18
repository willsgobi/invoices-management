using System.ComponentModel;

namespace InvoiceManagementApp.Enums
{
    public enum InvoiceStatus
    {
        [Description("Emitida")]
        Issued =1,
        [Description("Cobrança realizada")]
        Billed =2,
        [Description("Pagamento em atraso")]
        LatePayment =3,
        [Description("Pagamento realizado")]
        Paid =4
    }
}
