using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DataStructures.Models;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using Ninject;
using Ninject.Modules;

namespace DataStructures.Controllers
{
    class OrderModule : NinjectModule
    {
        public override void Load()
        {
            Bind<ITaxCalculator>().To<TaxCalculator>();
            Bind<IShippingCalculator>().To<ShippingCalculator>();
        }
    }

    interface ITaxCalculator {
        double CalculateTax();
    }

    class TaxCalculator : ITaxCalculator
    {
        public double CalculateTax()
        {
            return 5.45;
        }
    }

    interface IShippingCalculator
    {
        double CalculateShipping();
    }

    class ShippingCalculator : IShippingCalculator
    {
        public double CalculateShipping()
        {
            return 6.00;
        }
    }

    interface IOrderProcessor
    {
        void CreateOrder();
    }

    class OrderProcessor : IOrderProcessor
    {
        private ITaxCalculator taxCalculator;
        private IShippingCalculator shippingCalculator;

        public OrderProcessor(ITaxCalculator taxCalculator, IShippingCalculator shippingCalculator)
        {
            this.taxCalculator = taxCalculator;
            this.shippingCalculator = shippingCalculator;
        }

        public void CreateOrder()
        {
            var tax = taxCalculator.CalculateTax();
            var shipping = shippingCalculator.CalculateShipping();
        }
    }

    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            var kernel = new StandardKernel(new OrderModule());

            var orderProcessor = kernel.Get<OrderProcessor>();
            orderProcessor.CreateOrder();

            return View();
        }

        public ActionResult About()
        {
            return View();
        }
    }
}