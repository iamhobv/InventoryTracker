namespace RoboostAssessment.DTO.TransactionDTOs
{
    public class TransfereStockDTO
    {
        public string UserName { get; set; }
        //public int Quantity { get; set; }
        public int ProductId { get; set; }
        public int OldWarehouseID { get; set; }
        public int NewWarehouseID { get; set; }
    }
}
