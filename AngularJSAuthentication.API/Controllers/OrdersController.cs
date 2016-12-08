﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace AngularJSAuthentication.API.Controllers
{
    //API APENAS DE EXEMPLO, CONTENDO MÉTODOS PROTEGIDOS QUE NECESSITAM DE AUTENTICAÇÃO DE USUÁRIO 
    //NESTE EXEMPLO, TEMOS APENAS SOMENTE O GET DE UMA LISTA DE "ORDENS" CRIADAS EM MEMÓRIA

    [RoutePrefix("api/Orders")]
    public class OrdersController : ApiController
    {
        //API GET que necessita autenticação sua execução
        [Authorize]
        [Route("")]
        public IHttpActionResult Get()
        {
            return Ok(Order.CreateOrders());
        }
    }    

    //CLASSE EXEMPLO QUE JÁ PRÉ CARREGA UMA LISTA EM MEMÓRIA DE ORDENS
    public class Order
    {
        public int OrderID { get; set; }
        public string CustomerName { get; set; }
        public string ShipperCity { get; set; }
        public Boolean IsShipped { get; set; }

        public static List<Order> CreateOrders()
        {
            List<Order> OrderList = new List<Order> 
            {
                new Order {OrderID = 10248, CustomerName = "Taiseer Joudeh", ShipperCity = "Amman", IsShipped = true },
                new Order {OrderID = 10249, CustomerName = "Ahmad Hasan", ShipperCity = "Dubai", IsShipped = false},
                new Order {OrderID = 10250,CustomerName = "Tamer Yaser", ShipperCity = "Jeddah", IsShipped = false },
                new Order {OrderID = 10251,CustomerName = "Lina Majed", ShipperCity = "Abu Dhabi", IsShipped = false},
                new Order {OrderID = 10252,CustomerName = "Yasmeen Rami", ShipperCity = "Kuwait", IsShipped = true}
            };

            return OrderList;
        }
    }
}