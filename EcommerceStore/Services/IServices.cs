using EcommerceStore.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;

namespace EcommerceStore.Services
{
    public interface IServices
    {
        public User Search(User user);
        public void CreateUser(User user);
        public void CreateOrder(Order order, User user);
        public IEnumerable<Order> GetOrders();
    }
}
