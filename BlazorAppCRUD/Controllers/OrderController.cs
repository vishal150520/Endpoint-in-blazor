using BlazorAppCRUD.Data;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace BlazorAppCRUD.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : Controller
    {
        private readonly Orderservices _orderservices;

        public OrderController(Orderservices orderservices)
        {
            _orderservices = orderservices;
        }
        [HttpPost]
        [Route("CreateOrder")]
        public async Task<IActionResult> CreateOrder([FromBody] Order orderdto)
        {
            try
            {
                var createdTemplate = await _orderservices.CreateOrder(orderdto);
                return Ok(new ApiResponse<Order>((int)StatusCodes.Status200OK, true, "Create Order "));
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiResponse<Order>((int)StatusCodes.Status400BadRequest, false, ex.Message));
            }
        }
        [HttpPut]
        [Route("UpdateOrder")]
        public async Task<IActionResult> UpdateOrder([FromBody] Order orderdto)
        {
            try
            {

                var result = await _orderservices.UpdateOrder(orderdto);
                return Ok(new ApiResponse<Order>((int)StatusCodes.Status200OK, true, "Update Order "));
            }
            catch (Exception ex)
            {

                return BadRequest(new ApiResponse<Order>((int)StatusCodes.Status400BadRequest, false, ex.Message));
            }
        }
        [HttpGet]
        [Route("GetOrderDetailsById")]
        public async Task<IActionResult> GetOrderDetailsById(int OrderId)
        {
            try
            {
                var result = await _orderservices.GetOrderDetailsById(OrderId);
                return Ok(new ApiResponse<OrderDTO>(result, result != null ? "GetOrderDetailsById" : "Problem in fetching GetOrderDetailsById"));

            }
            catch (Exception ex)
            {
                return BadRequest(new ApiResponse<OrderDTO>((int)StatusCodes.Status400BadRequest, false, ex.Message));
            }

        }
        [HttpGet]
        [Route("GetAllOrders")]
        public async Task<IActionResult> GetAllOrders()
        {
            try
            {
                var result = await _orderservices.GetAllOrders();
                return Ok(new ApiResponse<List<OrderDTO>>((int)StatusCodes.Status200OK, true, "GetAll Details Order", result));
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiResponse<Order>((int)StatusCodes.Status400BadRequest, false, ex.Message));
            }
        }
        [HttpDelete]
        [Route("DeleteOrder")]
        public async Task<IActionResult> DeleteOrder(int Orderid)
        {
            try
            {
                var result = await _orderservices.DeleteOrder(Orderid);
                return Ok(new ApiResponse<List<OrderDTO>>((int)StatusCodes.Status200OK, true, "Delete Order"));
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiResponse<int>((int)StatusCodes.Status400BadRequest, false, ex.Message));
            }
        }

    }
}