using EsibayeniSolution.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace EsibayeniSolution.BusinessLogic
{
    public class Item_Business
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public List<LivesStock> all()
        {
            return db.LivesStocks.Include(i => i.Category).ToList();
        }
        public bool add(LivesStock model)
        {
            try
            {
                db.LivesStocks.Add(model);
                db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            { return false; }
        }
        public bool edit(LivesStock model)
        {
            try
            {
                db.Entry(model).State = EntityState.Modified;
                db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            { return false; }
        }
        public bool delete(LivesStock model)
        {
            try
            {
                db.LivesStocks.Remove(model);
                db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            { return false; }
        }
        public LivesStock find_by_id(int? id)
        {
            return db.LivesStocks.Find(id);
        }
        public void updateStock_Received(int item_id, int quantity)
        {
            var item = db.LivesStocks.Find(item_id);
            //item.QuantityInStock += quantity;
            db.SaveChanges();
        }
        public void updateOrder(int id, decimal price)
        {
            var item = db.Order_Items.Find(id);
            item.price = price;
            item.replied = true;
            item.date_replied = DateTime.Now;
            item.status = "Supplier Replied with Pricing Details";
            db.SaveChanges();
        }

    }
}