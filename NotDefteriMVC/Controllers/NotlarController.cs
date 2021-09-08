﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NotDefteriMVC.Data;
using NotDefteriMVC.Data.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace NotDefteriMVC.Controllers
{
    [Authorize]
    public class NotlarController : Controller
    {
        private readonly ApplicationDbContext db;

        public NotlarController(ApplicationDbContext db)
        {
            this.db = db;
        }
        public IActionResult Yeni()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Yeni(Note note)
        {
            if (ModelState.IsValid)
            {
                note.AuthorId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
                db.Notes.Add(note);
                db.SaveChanges();
                return RedirectToAction("Index", "Home");
            }

            return View();
        }
        public IActionResult Duzenle(int id)
        {
            NoteViewModel vm = new NoteViewModel();
            Note note = db.Notes.Where(x => x.Id == id && x.AuthorId == User.FindFirst(ClaimTypes.NameIdentifier).Value).FirstOrDefault();
            if (note == null)
                return NotFound();
            vm.NoteContent = note.Content;
            vm.NoteTitle = note.Title;
            return View(vm);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Duzenle(NoteViewModel vm)
        {
            if (ModelState.IsValid)
            {
                Note note = db.Notes.Where(x => x.Id == vm.Id && x.AuthorId == User.FindFirst(ClaimTypes.NameIdentifier).Value).FirstOrDefault();
                if (note == null)
                    return NotFound();
                note.Title = vm.NoteTitle;
                note.Content = vm.NoteContent;
                db.Update(note);
                db.SaveChanges();
                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        public IActionResult Sil(int id)
        {
            Note note = db.Notes.Where(x => x.Id == id && x.AuthorId == User.FindFirst(ClaimTypes.NameIdentifier).Value).FirstOrDefault();
            if (note == null)
                return NotFound();

            db.Remove(note);
            db.SaveChanges();
            return RedirectToAction("Index", "Home");
        }
    }
}
