﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using fitness.Models;

namespace fitness.Controllers
{
    public class SchedulesController : Controller
    {
        private fitnessandlifestyle db = new fitnessandlifestyle();

        // GET: Schedules
        public ActionResult Index()
        {
            var schedules = db.Schedules.Include(s => s.DayPerWeek).Include(s => s.Exercise).Include(s => s.WorkOut);
            return View(schedules.ToList());
        }

        // GET: Schedules/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Schedule schedule = db.Schedules.Find(id);
            if (schedule == null)
            {
                return HttpNotFound();
            }
            return View(schedule);
        }

        // GET: Schedules/Create
        public ActionResult Create()
        {
            ViewBag.DayPerWeekId = new SelectList(db.DayPerWeeks, "Id", "Day");
            ViewBag.ExId = new SelectList(db.Exercises, "Id", "Title");
            ViewBag.WorkOutId = new SelectList(db.WorkOuts, "Id", "Content");
            return View();
        }

        // POST: Schedules/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,DayPerWeekId,WorkOutId,Status,reps,sets,ExId")] Schedule schedule)
        {
            if (ModelState.IsValid)
            {
                db.Schedules.Add(schedule);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.DayPerWeekId = new SelectList(db.DayPerWeeks, "Id", "Day", schedule.DayPerWeekId);
            ViewBag.ExId = new SelectList(db.Exercises, "Id", "Title", schedule.ExId);
            ViewBag.WorkOutId = new SelectList(db.WorkOuts, "Id", "Content", schedule.WorkOutId);
            return View(schedule);
        }

        // GET: Schedules/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Schedule schedule = db.Schedules.Find(id);
            if (schedule == null)
            {
                return HttpNotFound();
            }
            ViewBag.DayPerWeekId = new SelectList(db.DayPerWeeks, "Id", "Day", schedule.DayPerWeekId);
            ViewBag.ExId = new SelectList(db.Exercises, "Id", "Title", schedule.ExId);
            ViewBag.WorkOutId = new SelectList(db.WorkOuts, "Id", "Content", schedule.WorkOutId);
            return View(schedule);
        }

        // POST: Schedules/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,DayPerWeekId,WorkOutId,Status,reps,sets,ExId")] Schedule schedule)
        {
            if (ModelState.IsValid)
            {
                db.Entry(schedule).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.DayPerWeekId = new SelectList(db.DayPerWeeks, "Id", "Day", schedule.DayPerWeekId);
            ViewBag.ExId = new SelectList(db.Exercises, "Id", "Title", schedule.ExId);
            ViewBag.WorkOutId = new SelectList(db.WorkOuts, "Id", "Content", schedule.WorkOutId);
            return View(schedule);
        }

        // GET: Schedules/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Schedule schedule = db.Schedules.Find(id);
            if (schedule == null)
            {
                return HttpNotFound();
            }
            return View(schedule);
        }

        // POST: Schedules/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Schedule schedule = db.Schedules.Find(id);
            db.Schedules.Remove(schedule);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
