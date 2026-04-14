namespace pitWeb.Services
{
    public class TaxService : ITaxService
    {

        public TaxService() { }

        public decimal CalculateTax(decimal income, decimal cost)
        {
            return (income - cost) * 0.19m;
        }
    }
}
