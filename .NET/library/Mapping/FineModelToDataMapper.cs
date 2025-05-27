namespace OneBeyondApi.Mapping;

public static class FineModelToDataMapper
{
    public static DataTransfer.FineData? CreateMapped(Model.Fine? fine)
    {
        if (fine is null)
        {
            return null;
        }

        return new DataTransfer.FineData
        {
            FineId = fine.Id,
            BorrowerId = fine.BorrowerId,
            Amount = fine.Amount,
            DateIssued = fine.DateIssued,
            DatePaid = fine.DatePaid,
        };
    }
}