using Nancy;
using HairSalon.Objects;
using System.Collections.Generic;
using System;

namespace HairSalon
{
    public class HomeModule : NancyModule
    {
        public HomeModule()
        {
            Get["/"] = _ => {
                List<Stylist> allStylists = Stylist.GetAll();
                return View["index.cshtml", allStylists];
            };

            Get["/clients"] = _ => {
                List<Client> allClients = Client.GetAll();
                return View["clients.cshtml", allClients];
            };

            Get["/stylists/new"] = _ => {
                return View["stylist_form.cshtml"];
            };

            Post["/stylists/new/added"] = _ => {
                string stylistName = Request.Form["stylist-name"];
                string stylistPhone = Request.Form["stylist-phone"];
                string stylistDescription = Request.Form["stylist-description"];

                Stylist newStylist = new Stylist(stylistName, stylistPhone, stylistDescription);
                newStylist.Save();
                return View["stylist_added.cshtml", newStylist];
            };

            Get["/clients/new"] = _ => {
                List<Stylist> allStylists = Stylist.GetAll();
                return View["client_form.cshtml", allStylists];
            };

            Post["/clients/new/added"] = _ => {
                Dictionary<string, object> model = new Dictionary<string, object> ();
                string clientName = Request.Form["client-name"];
                string clientDescription = Request.Form["client-description"];
                int clientStylistId = int.Parse(Request.Form["client-stylist-id"]);

                Client newClient = new Client(clientName, clientDescription, clientStylistId);
                newClient.Save();

                Stylist assignedStylist = Stylist.Find(clientStylistId);
                model.Add("client", newClient);
                model.Add("stylist", assignedStylist);
                return View["client_added.cshtml", model];
            };

            Get["/stylist/{id}"] = parameters => {
                Stylist selectedStylist = Stylist.Find(parameters.id);
                return View["stylist.cshtml", selectedStylist];
            };

            Get["/client/{id}"] = parameters => {
                Dictionary<string, object> model = new Dictionary<string, object> ();
                Client selectedClient = Client.Find(parameters.id);
                Stylist selectedStylist = Stylist.Find(selectedClient.GetStylistId());
                model.Add("client", selectedClient);
                model.Add("stylist", selectedStylist);
                return View["client.cshtml", model];
            };
            //
            // Get["/stylist/{id}/new_client"] = parameters => {
            //     Stylist selectedStylist = Stylist.Find(parameters.id);
            //     return View["specific_stylist_client_form.cshtml", selectedStylist];
            // };
            //
            // Post["/stylist/{id}/new_client/added"] = parameters => {
            //     Stylist selectedStylist = Stylist.Find(parameters.id);
            //
            //     string clientName = Request.Form["client-name"];
            //     string clientDescription = Request.Form["client-description"];
            //     string clientStylistId = SelectedStylist.GetId();
            //
            //     Client newClient = newClient(clientName, clientDescription, clientStylistId);
            //     newClient.Save();
            //     return View["client_added.cshtml", newClient];
            // };
        }
    }
}
