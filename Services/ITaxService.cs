namespace pitWeb.Services
{
    public interface ITaxService
    {
        public abstract decimal CalculateTax(decimal income, decimal cost);
    }
}
