using EventRegSystem.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using EventRegSystem.Models;
using EventRegSystem.Models.Domain;

namespace EventRegSystem.Controllers
{
    public class MainController : Controller
    {
        private readonly ILogger<MainController> _logger;
        TimeTableCRUD TT;
        EventsCRUD Events;
        ClientsCRUD clientsCRUD;

        public MainController(ILogger<MainController> logger)
        {
            _logger = logger;
            clientsCRUD = new ClientsCRUD();
            TT = new TimeTableCRUD();
            Events = new EventsCRUD();
        }

        public IActionResult Index()
        {
            //TT.AddEvent();
            return View(TT.GetEvents());
        }

        [HttpPost]
        public IActionResult Index(ReqRegistration req)
        {
            Console.WriteLine("Запись: " + req.TimeTableId + "|" + req.ClientName + "|" + req.phone);
            int result = Events.RegOnEvent(req);
            ViewBag.RegResult = result;
            Console.WriteLine("Result: " + result);
            return View(TT.GetEvents());
        }

        [HttpGet]
        public IActionResult AdminPanel(long? DeleteEventId, long? DeletePlayerId)
        {
            Console.WriteLine("DeleteId: " + DeleteEventId);
            if ((DeletePlayerId != null) && (DeleteEventId != null))
                Events.DeletePlayerFromEvent(DeletePlayerId.Value, DeleteEventId.Value);
            else if (DeleteEventId != null)
                TT.DeleteEvent(DeleteEventId.Value);
            
            ViewBag.TimeTableList = TT.GetEvents();
            return View(clientsCRUD.GetClients());
        }

        [HttpPost]
        public IActionResult AdminPanel(Client client, DateTime? RegTime)
        {
            ViewBag.TimeTableList = TT.GetEvents();
            if (client.Id != null)
            {
                if (RegTime != null)
                    client.RegDate = client.RegDate.Date + new TimeSpan(RegTime.Value.Hour, RegTime.Value.Minute, 0);
                clientsCRUD.AddClient(client);
                
            }
            
            return RedirectToAction("AdminPanel", clientsCRUD.GetClients());
        }

        [HttpPost]
        public IActionResult APAddEvent(TimeTable tt, DateTime? EventDate, DateTime? EventTime, string? HeadClientPhone)
        {
            if ((EventTime != null) && (EventDate != null))
                tt.EventDateTime = (EventDate.Value.Date + new TimeSpan(EventTime.Value.Hour, EventTime.Value.Minute, 0)).ToUniversalTime().AddHours(TimeZoneInfo.Local.BaseUtcOffset.Hours);
            
            ViewBag.AddEventResult = TT.AddEvent(tt, HeadClientPhone);
            
            
            ViewBag.TimeTableList = TT.GetEvents();
            return RedirectToAction("AdminPanel", clientsCRUD.GetClients());
        }

        [HttpPost]
        public IActionResult APUpdateEvent(TimeTable tt, DateTime? EventDate, DateTime? EventTime, string HeadClientPhone)
        {
            if ((EventTime != null) && (EventDate != null))
                tt.EventDateTime = (EventDate.Value.Date + new TimeSpan(EventTime.Value.Hour, EventTime.Value.Minute, 0)).ToUniversalTime().AddHours(TimeZoneInfo.Local.BaseUtcOffset.Hours);
            ViewBag.AddEventResult = TT.UpdateEvent(tt, HeadClientPhone);
            Console.WriteLine("UPD: "+tt.Name);

            ViewBag.TimeTableList = TT.GetEvents();
            return RedirectToAction("AdminPanel", clientsCRUD.GetClients());
        }

        public IActionResult Rules()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}