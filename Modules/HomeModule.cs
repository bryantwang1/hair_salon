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
                return View["client_form.cshtml"];
            };

            Post["/clients/new/added"] = _ => {
                string clientName = Request.Form["client-name"];
                string clientDescription = Request.Form["client-description"];
                string clientStylistId = Request.Form["client-stylist-id"];

                Client newClient = newClient(clientName, clientDescription, clientStylistId);
                newClient.Save();
                return View["client_added.cshtml", newClient];
            }

            Get["/stylist/{id}"] = parameters => {
                Stylist selectedStylist = Stylist.Find(parameters.id);
                return View["stylist.cshtml", selectedStylist];
            };

            Get["/client/{id}"] = parameters => {
                Client selectedClient = Client.Find(parameters.id);
                return View["client.cshtml", selectedClient];
            };

            Get["/stylist/{id}/new_client"] = parameters => {
                Stylist selectedStylist = Stylist.Find(parameters.id);
                return View["specific_stylist_client_form.cshtml", selectedStylist];
            };

            Post["/stylist/{id}/new_client/added"] = parameters => {
                Stylist selectedStylist = Stylist.Find(parameters.id);

                string clientName = Request.Form["client-name"];
                string clientDescription = Request.Form["client-description"];
                string clientStylistId = SelectedStylist.GetId();

                Client newClient = newClient(clientName, clientDescription, clientStylistId);
                newClient.Save();
                return View["client_added.cshtml", newClient];
            };
        }
    }
}
