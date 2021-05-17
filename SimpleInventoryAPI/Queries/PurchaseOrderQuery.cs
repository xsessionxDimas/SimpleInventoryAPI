using Microsoft.EntityFrameworkCore;
using MySql.Data.MySqlClient;
using SimpleInventoryAPI.DBContext;
using SimpleInventoryAPI.QueryDTOs;
using System.Collections.Generic;
using System.Threading.Tasks;


namespace SimpleInventoryAPI.Queries
{
    public class PurchaseOrderQuery
    {
        private readonly SimpleInventoryDbContext dbContext;

        public PurchaseOrderQuery(SimpleInventoryDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<POModel> GetPurchaseOrder(int id)
        {
            var query = @"set @row_num=0;
                        select (@row_num:=@row_num+1) as RowNo,
                                a.Id,
	                            b.SupplierName as Supplier,
                                a.PurchaseOrderNumber,
                                a.IsDraft,
                                a.OrderDate,
                                a.SubTotal,
                                a.Discount,
                                a.Tax,
                                a.Additional,
                                a.GrandTotal,
                                a.Notes
                        from purchaseorders a
                        join suppliers b on a.supplierid = b.id
                        where a.Id = {0}";
            var sqlParameters = new List<MySqlParameter>
            {
                new MySqlParameter("@p0", id)
            };
            var raw = await dbContext.PO.FromSqlRaw(query, sqlParameters.ToArray()).ToListAsync();
            return raw[0];
        }

        public Task<List<POModel>> GetPurchaseOrders()
        {
            var query = @"set @row_num=0;
                        select (@row_num:=@row_num+1) as RowNo,
                                a.Id,
	                            b.SupplierName as Supplier,
                                a.PurchaseOrderNumber,
                                a.IsDraft,
                                a.OrderDate,
                                a.SubTotal,
                                a.Discount,
                                a.Tax,
                                a.Additional,
                                a.GrandTotal,
                                a.Notes
                        from purchaseorders a
                        join suppliers b on a.supplierid = b.id";
            return dbContext.PO.FromSqlRaw(query).ToListAsync();
        }

        public Task<List<POItemModel>> GetPurchaseOrderItemsByHeaderId(int headerId)
        {
            var query = @"set @row_num=0;
                          select (@row_num:=@row_num+1) as RowNo,
                                   a.ComponentId,
                                   b.PartNumber,
                                   b.PartDescription as PartDesc,
                                   a.Qty,
                                   a.Price,
                                   a.Discount,
                                   a.Total
                          from purchaseorderitem a
                          join components b on a.componentid = b.id
                          where a.PurchaseOrderId = {0}";
            var sqlParameters = new List<MySqlParameter>
            {
                new MySqlParameter("@p0", headerId)
            };
            return dbContext.POItems.FromSqlRaw(query, sqlParameters.ToArray()).ToListAsync();
        }
    }
}
