﻿using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Project0.Library.Models
{
    [DataContract]
    public class Order //no rewriting history, fields are readonly
    {
        //private Pizza _pizza;
        //private int _amount;
        //replaced by dictionary

        [DataMember]
        public DateTime OrderTime { get; } //DateTime cannot be null
        [DataMember]
        public PizzaStore Store { get; }
        [DataMember]
        public Customer Customer { get; }
        [DataMember]
        public Address Address { get; } = null; //Null address means carry-out

        [DataMember]
        public Dictionary<Pizza, int> OrderItems { get; }
        public decimal TotalPrice { get; } = 0; //Should be calculable based on Pizza and Amount.

        //[DataMember]
        //public Pizza[] Pizza
        //{
        //    get => _pizza;
        //    set
        //    {
        //        _pizza = value ?? throw new ArgumentNullException(nameof(value), "Order's pizza type must not be null."); ;
        //    }
        //}

        //[DataMember]
        //public int Amount
        //{
        //    get => _amount;
        //    set
        //    {
        //        if (value <= 0)
        //        {
        //            throw new ArgumentOutOfRangeException("Order cannot contain less than one of a pizza.");
        //        }
        //        _amount = value;
        //    } //int cannot be null, no check.
        //}


        //public Order(PizzaStore store, Customer cust, Pizza pizza, int amount, DateTime now)
        //{
        //    Store = store;
        //    Customer = cust;
        //    Pizza = pizza;
        //    Amount = amount;
        //    OrderTime = now;
        //}

        //public Order(PizzaStore store, Customer cust, Address deliveryAdd, Pizza pizza, int amount, DateTime now)
        //{
        //    Store = store;
        //    Customer = cust;
        //    Address = deliveryAdd;
        //    Pizza = pizza;
        //    Amount = amount;
        //    OrderTime = now;
        //}


        public Order(PizzaStore store, Customer cust, Dictionary<Pizza, int> orderItems, DateTime now) //carryout order
        {
            Store = store ?? throw new ArgumentNullException(nameof(store), "Order's store must not be null."); ;
            Customer = cust ?? throw new ArgumentNullException(nameof(cust), "Order's customer must not be null."); ;

            OrderItems = new Dictionary<Pizza, int>(orderItems); //don't want modifiable order history

            foreach(var pizza in orderItems) //total price is the sum of the number of each pizza times its price
            {
                TotalPrice += pizza.Value * pizza.Key.Price;
            }

            OrderTime = now;
        }

        public Order(PizzaStore store, Customer cust, Address deliveryAdd, Dictionary<Pizza, int> orderItems, DateTime now)
        {
            Store = store ?? throw new ArgumentNullException(nameof(store), "Order's store must not be null."); ;
            Customer = cust ?? throw new ArgumentNullException(nameof(cust), "Order's customer must not be null.");

            Address = deliveryAdd; //null address means carry out

            OrderItems = new Dictionary<Pizza, int>(orderItems); //don't want modifiable order history
            foreach (var pizza in orderItems) //total price is the sum of the number of each pizza times its price
            {
                TotalPrice += pizza.Value * pizza.Key.Price;
            }

            OrderTime = now;
        }

    }
}
