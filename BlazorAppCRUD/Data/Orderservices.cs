using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace BlazorAppCRUD.Data
{
    public class Orderservices
    {
       
        private readonly ApplicationDbContext _appDBContext;
      

      
        public Orderservices(ApplicationDbContext appDBContext)
        {
            _appDBContext = appDBContext;
        }
       

       

       
        public async Task<bool> CreateOrder(Order order)
        {
            Order order1 = new Order()
            {
                OrderName = order.OrderName,
                State = order.State,

            };
            var res= _appDBContext.order.Add(order1);
            await _appDBContext.SaveChangesAsync();
            int primaryKeyValue = order1.OrderId;

            var windows = new List<Window>();
            foreach (var windowDTO in order.Windows)
            {
                var window = new Window
                {
                    WindowName = windowDTO.WindowName,
                    QuantityOfWindows = windowDTO.QuantityOfWindows,
                    TotalSubElements = windowDTO.TotalSubElements,
                    OrderId= primaryKeyValue,
                    // Map other window properties
                };
                _appDBContext.window.Add(window);
                await _appDBContext.SaveChangesAsync();

                int primaryKey = window.WindowId;

                var subElements = new List<SubElement>();
                foreach (var subElementDTO in windowDTO.SubElements)
                {
                    var subElement = new SubElement
                    {
                        Element = subElementDTO.Element,
                        Type = subElementDTO.Type,
                        Width = subElementDTO.Width,
                        Height = subElementDTO.Height,
                        WindowId = primaryKey,
                        // Map other sub-element properties
                    };
                    _appDBContext.subelement.Add(subElement);
                    await _appDBContext.SaveChangesAsync();
                }
            }
            // Add entities to the context and save changes
           
            return true;
        }
        public async Task<bool> UpdateOrder(Order updatedOrder)
        {
            // Get the existing order from the database
            var existingOrder = await _appDBContext.order
                .Include(o => o.Windows)
                .ThenInclude(w => w.SubElements)
                .FirstOrDefaultAsync(o => o.OrderId == updatedOrder.OrderId);

            if (existingOrder == null)
            {
                // Order not found
                return false;
            }

            // Update properties of the existing order
            existingOrder.OrderName = updatedOrder.OrderName;
            existingOrder.State = updatedOrder.State;

            // Update existing windows and add new ones
            foreach (var updatedWindow in updatedOrder.Windows)
            {
                var existingWindow = existingOrder.Windows.FirstOrDefault(w => w.WindowId == updatedWindow.WindowId);

                if (existingWindow != null)
                {
                    // Update properties of the existing window
                    existingWindow.WindowName = updatedWindow.WindowName;
                    existingWindow.QuantityOfWindows = updatedWindow.QuantityOfWindows;
                    existingWindow.TotalSubElements = updatedWindow.TotalSubElements;
                    // Update other properties as needed

                    // Update existing sub-elements and add new ones
                    foreach (var updatedSubElement in updatedWindow.SubElements)
                    {
                        var existingSubElement = existingWindow.SubElements.FirstOrDefault(se => se.SubElementId == updatedSubElement.SubElementId);

                        if (existingSubElement != null)
                        {
                            // Update properties of the existing sub-element
                            existingSubElement.Element = updatedSubElement.Element;
                            existingSubElement.Type = updatedSubElement.Type;
                            existingSubElement.Width = updatedSubElement.Width;
                            existingSubElement.Height = updatedSubElement.Height;
                            // Update other properties as needed
                        }
                        else
                        {
                            // Add new sub-element
                            existingWindow.SubElements.Add(new SubElement
                            {
                                Element = updatedSubElement.Element,
                                Type = updatedSubElement.Type,
                                Width = updatedSubElement.Width,
                                Height = updatedSubElement.Height,
                                // Set other properties
                            });
                        }
                    }
                }
                else
                {
                    // Add new window
                    existingOrder.Windows.Add(new Window
                    {
                        WindowName = updatedWindow.WindowName,
                        QuantityOfWindows = updatedWindow.QuantityOfWindows,
                        TotalSubElements = updatedWindow.TotalSubElements,
                        // Set other properties
                        SubElements = updatedWindow.SubElements.Select(se => new SubElement
                        {
                            Element = se.Element,
                            Type = se.Type,
                            Width = se.Width,
                            Height = se.Height,
                            // Set other properties
                        }).ToList()
                    });
                }
            }

            await _appDBContext.SaveChangesAsync();
            return true;
        }
        public async Task<List<Order>> GetAllOrders()
        {
            var orders = await _appDBContext.order
                .Include(o => o.Windows)
                    .ThenInclude(w => w.SubElements)
                .ToListAsync();

            // Map entities to DTOs
            var order = orders.Select(order => new Order
            {
                OrderId = order.OrderId,
                OrderName = order.OrderName,
                State = order.State,
                Windows = order.Windows.Select(window => new Window
                {
                    WindowId = window.WindowId,
                    WindowName = window.WindowName,
                    QuantityOfWindows = window.QuantityOfWindows,
                    TotalSubElements = window.TotalSubElements,
                    SubElements = window.SubElements.Select(subElement => new SubElement
                    {
                        SubElementId = subElement.SubElementId,
                        Element = subElement.Element,
                        Type = subElement.Type,
                        Width = subElement.Width,
                        Height = subElement.Height
                    }).ToList()
                }).ToList()
            }).ToList();

            return order;
        }
        public async Task<List<Order>> GetOrder()
        {
            return await _appDBContext.order.ToListAsync();
        }
        public async Task<List<Window>> GetWindows()
        {
            return await _appDBContext.window.ToListAsync();
        }
        public async Task<List<SubElement>> GetSubElement()
        {
            return await _appDBContext.subelement.ToListAsync();
        }
        public async Task<Order> GetOrderDetailsById(int orderId)
        {
            var order = await _appDBContext.order
                .Where(o => o.OrderId == orderId)
                .Include(o => o.Windows)
                    .ThenInclude(w => w.SubElements)
                .FirstOrDefaultAsync();

            if (order == null)
            {
                // Order not found
                return null;
            }

            // Map the entity to a DTO
            var orders = new Order
            {
                OrderId = order.OrderId,
                OrderName = order.OrderName,
                State = order.State,
                Windows = order.Windows.Select(window => new Window
                {
                    WindowId = window.WindowId,
                    WindowName = window.WindowName,
                    QuantityOfWindows = window.QuantityOfWindows,
                    TotalSubElements = window.TotalSubElements,
                    SubElements = window.SubElements.Select(subElement => new SubElement
                    {
                        SubElementId = subElement.SubElementId,
                        Element = subElement.Element,
                        Type = subElement.Type,
                        Width = subElement.Width,
                        Height = subElement.Height
                    }).ToList()
                }).ToList()
            };

            return orders;
        }
        public async Task<bool> DeleteOrder(int orderId)
        {
            // Find the order by orderId
            var order = await _appDBContext.order
                .Where(o => o.OrderId == orderId)
                .Include(o => o.Windows)
                    .ThenInclude(w => w.SubElements)
                .FirstOrDefaultAsync();

            if (order == null)
            {
                // Order not found
                return false;
            }

            // Remove order, associated windows, and sub-elements
            _appDBContext.order.Remove(order);
            await _appDBContext.SaveChangesAsync();

            return true;
        }
        public async Task<Order> GetOrder(int OrderId)
        {
            Order order = await _appDBContext.order.FirstOrDefaultAsync(c => c.OrderId.Equals(OrderId));
            return order;
        }
        public async Task<Window> GetWindows(int WindowId)
        {
            Window  windows = await _appDBContext.window.FirstOrDefaultAsync(c => c.WindowId.Equals(WindowId));
            return windows;
        }
        public async Task<SubElement> GetSubElement(int SubElementId)
        {
            SubElement SubElement = await _appDBContext.subelement.FirstOrDefaultAsync(c => c.SubElementId.Equals(SubElementId));
            return SubElement;
        }
    }
}
