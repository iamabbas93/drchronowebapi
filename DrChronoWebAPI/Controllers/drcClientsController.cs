using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DrChronoWebAPI.Models;
using DotNetOpenAuth.OAuth2;

namespace DrChronoWebAPI.Controllers
{
    public class drcClientsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public drcClientsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: drcClients
        public async Task<IActionResult> Index()
        {
            return View(await _context.drcClients.ToListAsync());
        }

        // GET: drcClients/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var drcClient = await _context.drcClients
                .FirstOrDefaultAsync(m => m.id == id);
            if (drcClient == null)
            {
                return NotFound();
            }

            return View(drcClient);
        }

        // GET: drcClients/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: drcClients/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("id,clientId,clientSecret,authorizationEndpoint,tokenEndpoint,redirectUri,doctorId,password,userName")] drcClient drcClient)
        {
            if (ModelState.IsValid)
            {
                var doc = _context.drcClients.Where(x => x.doctorId == drcClient.doctorId).FirstOrDefault();
                if (doc == null)
                {
                    drcClient.createdBy = "TestCreated";
                    drcClient.createdDate = DateTime.Now;
                    _context.Add(drcClient);
                    await _context.SaveChangesAsync();
                }
                else
                {
                    doc.clientId = drcClient.clientId;
                    doc.clientSecret = drcClient.clientSecret;
                    doc.authorizationEndpoint = drcClient.authorizationEndpoint;
                    doc.tokenEndpoint = drcClient.tokenEndpoint;
                    doc.redirectUri = drcClient.redirectUri;
                    doc.doctorId = drcClient.doctorId;
                    doc.userName = drcClient.userName;
                    doc.password = drcClient.password;
                    doc.updateBy = "TestUpdate";
                    doc.updateDate = DateTime.Now;
                    _context.Entry(doc).State = EntityState.Modified;
                    _context.Update(doc);
                    await _context.SaveChangesAsync();
                }
               

               
            }
            return View(drcClient);
        }

        // GET: drcClients/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var drcClient = await _context.drcClients.FindAsync(id);
            if (drcClient == null)
            {
                return NotFound();
            }
            return View(drcClient);
        }

        // POST: drcClients/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("id,clientId,clientSecret,authorizationEndpoint,tokenEndpoint,redirectUri,doctorId")] drcClient drcClient)
        {
            if (id != drcClient.id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(drcClient);
                    await _context.SaveChangesAsync();
                    return Redirect(String.Format("https://drchrono.com/o/authorize/?response_type=code&client_id={0}&redirect_uri={1}", drcClient.clientId, drcClient.redirectUri));

                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!drcClientExists(drcClient.id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(drcClient);
        }

        // GET: drcClients/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var drcClient = await _context.drcClients
                .FirstOrDefaultAsync(m => m.id == id);
            if (drcClient == null)
            {
                return NotFound();
            }

            return View(drcClient);
        }

        // POST: drcClients/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var drcClient = await _context.drcClients.FindAsync(id);
            _context.drcClients.Remove(drcClient);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool drcClientExists(int id)
        {
            return _context.drcClients.Any(e => e.id == id);
        }
    }
}
