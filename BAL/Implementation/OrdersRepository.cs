﻿using BAL.Constant;
using BAL.Pagination;
using BAL.Repository;
using BAL.RequestModels;
using Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace BAL.Implementation
{
    public class OrdersRepository : IGenericRepository<OrderModel>, IOrdersRepository
    {
        private SigmaproIisContext context;
        private ILogger<UnitOfWork> _logger;
        private readonly string _corelationId = string.Empty;
        public OrdersRepository(SigmaproIisContext _context, ILogger<UnitOfWork> logger)
        {
            context = _context;
            _logger = logger;
        }
        public async Task<ApiResponse<string>> DeleteAsync(Guid id)
        {
            try
            {
                var Orders = await context.Orders.FindAsync(id);

                if (Orders != null)
                {
                    Orders.Isdelete = true;
                    context.Orders.Update(Orders);
                    await context.SaveChangesAsync();

                    return ApiResponse<string>.Success(id.ToString(), "Order deleted successfully.");
                }

                return ApiResponse<string>.Fail("Order with the given ID not found.");
            }
            catch (Exception exp)
            {
                _logger.LogError($"CorelationId: {_corelationId} - Exception occurred in Method: {nameof(DeleteAsync)} Error: {exp?.Message}, Stack trace: {exp?.StackTrace}");
                return ApiResponse<string>.Fail("Order with the given ID not found.");
            }
        }

        public async Task<IEnumerable<OrderModel>> Find(Expression<Func<OrderModel, bool>> predicate)
        {
            return (IEnumerable<OrderModel>)await context.Set<OrderModel>().FindAsync(predicate);
        }

        public async Task<IEnumerable<OrderModel>> GetAllAsync()
        {
            return await context.Set<OrderModel>().ToListAsync();
        }

        public async Task<OrderModel> GetByIdAsync(int id)
        {
            return await context.Set<OrderModel>().FindAsync(id);
        }

        public async Task<ApiResponse<string>> InsertAsync(OrderModel entity)
        {
            try
            {
                await context.Set<OrderModel>().AddAsync(entity);
                await context.SaveChangesAsync();
                return ApiResponse<string>.Success(null, "Order inserted successfully.");
            }
            catch (Exception exp)
            {
                _logger.LogError($"CorelationId: {_corelationId} - Exception occurred in Method: {nameof(InsertAsync)} Error: {exp?.Message}, Stack trace: {exp?.StackTrace}");
                return ApiResponse<string>.Fail("An error occurred while Inserting the Order.");
            }

        }

        public async Task<ApiResponse<string>> UpdateAsync(OrderModel entity)
        {
            if (entity == null)
            {
                _logger.LogError($"CorelationId: {_corelationId} - Invalid input. EditOrderRequest object is null in Method: {nameof(UpdateAsync)}");
                return ApiResponse<string>.Fail("Invalid input. EditOrderRequest object is null.");
            }
            try
            {
                var updateOrders = await context.Orders.FindAsync(entity.Id);  
                
                if (updateOrders != null)
                {
                    var updateorderitems =await context.OrderItems.FindAsync(entity.OrderItemId);
                    updateOrders.OrderDate = entity.OrderDate;
                    updateOrders.OrderStatus = entity.OrderStatus; 
                    updateOrders.OrderTotal = entity.OrderTotal;
                    updateOrders.UpdatedBy = entity.UpdatedBy;
                    updateOrders.UpdatedDate = entity.UpdatedDate;
                    updateOrders.DiscountAmount = entity.DiscountAmount;
                    updateOrders.Incoterms = entity.Incoterms;
                    updateOrders.TaxAmount = entity.TaxAmount;
                    updateOrders.TermsConditionsId = entity.TermsConditionsId;                   
                    context.Orders.Update(updateOrders);
                    await context.SaveChangesAsync();
                    if (updateorderitems != null)
                    {
                        updateorderitems.OrderItemDesc = entity.OrderItemDesc;
                        updateorderitems.UpdatedDate = entity.UpdatedDate;
                        updateorderitems.OrderItemStatus = entity.OrderItemStatus;
                        updateorderitems.Quantity = entity.Quantity;
                        updateorderitems.ProductId = entity.ProductId;
                        updateorderitems.UnitPrice = entity.UnitPrice;
                        updateorderitems.UpdatedBy = entity.UpdatedBy;
                        context.OrderItems.Update(updateorderitems);
                        await context.SaveChangesAsync();
                    }


                    return ApiResponse<string>.Success(entity.Id.ToString(), "Order record updated successfully.");
                }
                return ApiResponse<string>.Fail("Order with the given ID not found.");

            }
            catch (Exception exp)
            {
                _logger.LogError($"CorelationId: {_corelationId} - Exception occurred in Method: {nameof(UpdateAsync)} Error: {exp?.Message}, Stack trace: {exp?.StackTrace}");
                return ApiResponse<string>.Fail("An error occurred while updating the order.");
            }
        }
        public async Task<PaginationModel<OrderModel>> GetAllAsync(SearchOrderParams search)
        {
            var orderModelList = new List<OrderModel>();            
            string keyword = search.keyword.IsNullOrEmpty() ? string.Empty : search.keyword.Trim().ToLower();           
            string orderdate = search.date_of_order.IsNullOrEmpty()? string.Empty : search.date_of_order.ToLower();            
            string orderstatus= search.order_status.IsNullOrEmpty() ? string.Empty : search.order_status.ToLower();
            string orderitemdesc = search.order_item_desc.IsNullOrEmpty() ? string.Empty : search.order_item_desc.ToLower();

            var orderlist = await context.Orders.
                            Join(context.OrderItems, ord => ord.Id.ToString(), oi => oi.OrderId.ToString(), (ord, oi) => new { orders = ord, items = oi }).                            
                            Join(context.Facilities, o => o.orders.FacilityId, f => f.Id, (o, f) => new { orders = o.orders, o.items, facility = f }).
                            Join(context.Products, fa => fa.items.ProductId, p => p.Id, (fa, p) => new { orders = fa.orders, fa.items,fa.facility, product = p }).                            
                            Join(context.Cvxes, pr=>pr.product.CvxCodeId,c=>c.Id,(pr,c)=>new { orders = pr.orders, pr.items, product = pr.product, pr.facility,cvx = c}).
                            Where(i =>(i.items.OrderItemDesc.ToLower().Contains(keyword) || i.facility.FacilityName.ToLower().Contains(keyword)||i.product.ProductName.ToLower().Contains(keyword)
                            ) && i.orders.Isdelete == false).
                            Select(i => new
                            {
                                i.orders.Id,
                                i.orders.OrderId,
                                i.product.ProductName,
                                i.items.OrderItemDesc,                                
                                i.cvx.CvxDescription,
                                i.items.OrderItemStatus,
                                i.items.Quantity,
                                i.product,
                                i.items.UnitPrice,
                                i.facility.FacilityName,
                                i.facility,
                                i.orders.DiscountAmount,
                                i.orders.Incoterms,
                                i.orders                              

                            }).ToListAsync();//ToPagedListAsync(search.pagenumber, search.pagesize);

            Parallel.ForEach(orderlist, async i =>
            {
                var model = new OrderModel()
                {
                    Id = i.orders.Id,
                    OrderId = i.OrderId,
                    FacilityId =i.facility.Id,
                   Facility=i.FacilityName,
                   DiscountAmount=i.DiscountAmount,
                   Incoterms=i.Incoterms,
                   OrderDate = i.orders.OrderDate,
                    OrderItemDesc = i.OrderItemDesc,
                    OrderItemStatus = i.OrderItemStatus,
                    OrderTotal =i.orders.OrderTotal,
                    UnitPrice = i.UnitPrice,
                    Quantity = i.Quantity,
                    TaxAmount = i.orders.TaxAmount,
                   OrderStatus=i.orders.OrderStatus,
                   TermsConditionsId = i.orders.TermsConditionsId,
                   UserId=i.orders.UserId,
                    Product = i.ProductName,
                    CVXDesc =i.CvxDescription,
                   CreatedBy=i.orders.CreatedBy,
                   CreatedDate=i.orders.CreatedDate,
                    UpdatedBy = i.orders.UpdatedBy,
                    ProductId = i.product.Id
                };
                orderModelList.Add(model);

            });
            Task.WhenAll();

            long? totalRows = orderModelList.Count();
            var response = orderModelList.Skip(search.pagesize * (search.pagenumber - 1)).Take(search.pagesize).ToList();
            return PaginationHelper.Paginate(response, search.pagenumber, search.pagesize, Convert.ToInt32(totalRows));

        }

        public async Task<PaginationModel<OrderModel>> GetAllOrders(int pagenumber,int pagesize)
        {
            var orderModelList = new List<OrderModel>();
            var orderlist = await context.Orders.
                            Join(context.OrderItems, ord => ord.Id.ToString(), oi => oi.OrderId.ToString(), (ord, oi) => new { orders = ord, items = oi }).
                            Join(context.Facilities, o => o.orders.FacilityId, f => f.Id, (o, f) => new { orders = o.orders, o.items, facility = f }).
                            Join(context.Products, fa => fa.items.ProductId, p => p.Id, (fa, p) => new { orders = fa.orders, fa.items, fa.facility, product = p }).                            
                            Join(context.Cvxes, pr => pr.product.CvxCodeId, c => c.Id, (pr, c) => new { orders = pr.orders, product = pr.product, pr.items,  pr.facility, cvx = c }).
                            Where(i=>i.orders.Isdelete==false).Select
                            (i => new
                            {
                                i.orders.Id,
                                i.orders.OrderId,
                                i.product.ProductName,
                                i.product.ProductId,
                                i.product.CvxCodeId,
                                i.product,
                                i.items.OrderItemDesc,                                
                                i.cvx.CvxDescription,
                                i.items.OrderItemStatus,
                                i.items.Quantity,
                                i.items.UnitPrice,
                                i.facility.FacilityName,
                                i.facility,
                                i.orders.DiscountAmount,
                                i.orders.Incoterms,
                                i.orders

                            }).ToPagedListAsync(pagenumber, pagesize);

            Parallel.ForEach(orderlist, async i =>
            {
                var model = new OrderModel()
                {
                    FacilityId = i.facility.Id,  
                    Facility=i.FacilityName,
                    DiscountAmount = i.DiscountAmount,
                    Incoterms = i.Incoterms,
                    OrderDate = i.orders.OrderDate,
                    OrderItemDesc = i.OrderItemDesc,
                    OrderItemStatus = i.OrderItemStatus,
                    Id = i.orders.Id,
                    OrderTotal = i.orders.OrderTotal,
                    UnitPrice = i.UnitPrice,
                    Quantity = i.Quantity,
                    TaxAmount = i.orders.TaxAmount,
                    OrderStatus = i.orders.OrderStatus,
                    TermsConditionsId = i.orders.TermsConditionsId,
                    UserId = i.orders.UserId,
                    Product = i.ProductName,
                    CVXDesc = i.CvxDescription,
                    CreatedBy = i.orders.CreatedBy,
                    CreatedDate = i.orders.CreatedDate,
                    UpdatedBy = i.orders.UpdatedBy,
                    OrderId = i.OrderId,
                    ProductId = i.product.Id
                };
                orderModelList.Add(model);

            });
            Task.WhenAll();

            long? totalRows = orderModelList.Count();
            var response = orderModelList.Skip(pagesize * (pagenumber - 1)).Take(pagesize).ToList();
            return PaginationHelper.Paginate(response, pagenumber, pagesize, Convert.ToInt32(totalRows));
           
        }

        public async Task<ApiResponse<string>> InsertOrdersAsync(RespOrderModel entity)
        {
            try
            {
                var neworders = new Order()
                {
                    FacilityId = entity.FacilityId,
                    UserId = entity.UserId,
                    DiscountAmount = entity.DiscountAmount,
                    Incoterms = entity.Incoterms,
                    OrderDate = entity.OrderDate,
                    OrderStatus = entity.OrderStatus,
                    OrderTotal = entity.OrderTotal,
                    TaxAmount = entity.TaxAmount,
                    TermsConditionsId = entity.TermsConditionsId,
                    CreatedBy = entity.CreatedBy,
                    CreatedDate = entity.CreatedDate,
                    UpdatedBy = entity.UpdatedBy,
                    UpdatedDate = entity.UpdatedDate,
                    Isdelete = entity.Isdelete
                };
                context.Orders.Add(neworders);
                await context.SaveChangesAsync();
                for (int i = 0; i < entity.OrderofItems.Count; i++)
                {
                    var neworditem = new OrderItem()
                    {
                        OrderItemDesc = entity.OrderofItems[i].OrderItemDesc,
                        OrderId = neworders.Id,
                        OrderItemStatus = entity.OrderofItems[i].OrderItemStatus,
                        ProductId = entity.OrderofItems[i].ProductId,
                        Isdelete = false,
                        Quantity = entity.OrderofItems[i].Quantity,
                        UnitPrice = entity.OrderofItems[i].UnitPrice,
                        CreatedBy = entity.CreatedBy,
                        CreatedDate = entity.CreatedDate,
                        UpdatedBy = entity.UpdatedBy,
                        UpdatedDate = entity.UpdatedDate,
                    };
                    context.OrderItems.Add(neworditem);
                    await context.SaveChangesAsync();
                }

                var neworderaddress = new Address()
                {
                    Line1 = entity.Address.Line1,
                    Line2 = entity.Address.Line2,
                    Suite = entity.Address.Suite,
                    CountryId = entity.Address.CountryId,
                    CountyId = entity.Address.CountryId,
                    StateId = entity.Address.CountryId,
                    CityId = entity.Address.CountryId,
                    ZipCode = entity.Address.ZipCode,
                    CreatedBy = entity.CreatedBy,
                    CreatedDate = entity.CreatedDate,
                    UpdatedBy = entity.UpdatedBy,
                    UpdatedDate = entity.UpdatedDate
                };
                context.Addresses.Add(neworderaddress);
                await context.SaveChangesAsync();
                var newshippment = new Shipment()
                {
                    ShipmentDate = entity.Shiping.ShipmentDate,
                    ExpectedDeliveryDate = entity.Shiping.Expecteddeliverydate,
                    PackageSize = entity.Shiping.PackageSize,
                    PakegeLength = entity.Shiping.PackageLength,
                    PakegeWidth = entity.Shiping.PackageWidth,
                    PakegeHeight = entity.Shiping.PackageHeight,
                    SizeUnitOfMesure = entity.Shiping.SizeUnitofMesure,
                    WeightUnitOfMeasure = entity.Shiping.WeightUnitofMeasure,
                    TypeOfPackagingMaterial = entity.Shiping.TypeofPackagingMaterial,
                    TypeOfPackage = entity.Shiping.TypeofPackage,
                    StoringTemparture = entity.Shiping.Storingtemparature,
                    TemperatureUnitOfMeasure = entity.Shiping.TemperatureUnitofmeasure,
                    NoOfPackages = entity.Shiping.NoofPackages,
                    TrackingNumber = entity.Shiping.TrackingNumber,
                    ReceiverId = entity.Shiping.RecieverId,
                    ReceivingHours = entity.Shiping.RecievingHours,
                    IsSignatureNeeded = entity.Shiping.IsSignatureneeded,
                    Isdelete = entity.Isdelete,
                    CreatedBy = entity.CreatedBy,
                    CreatedDate = entity.CreatedDate,
                    UpdatedBy = entity.UpdatedBy,
                    UpdatedDate = entity.UpdatedDate,
                    ShipmentAddressId = neworderaddress.Id
                };
                context.Shipments.Add(newshippment);
                await context.SaveChangesAsync();
                return ApiResponse<string>.Success(neworders.Id.ToString(), "Order created successfully.");
            }
            catch (Exception exp)
            {
                _logger.LogError($"CorelationId: {_corelationId} - Exception occurred in Method: {nameof(InsertAsync)} Error: {exp?.Message}, Stack trace: {exp?.StackTrace}");
                return ApiResponse<string>.Fail("An error occurred while creating the Order.");
            }

        }
    }
}
