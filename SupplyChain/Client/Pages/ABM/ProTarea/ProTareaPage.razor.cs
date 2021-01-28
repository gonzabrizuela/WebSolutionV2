using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using SupplyChain;
using Syncfusion.Blazor.Grids;
using Syncfusion.Blazor.Navigations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading.Tasks;
using SupplyChain.Shared.Models;
namespace SupplyChain.Pages.ProTareax
{
    public class ProTareaxPageBase : ComponentBase
    {
        [Inject] protected HttpClient Http { get; set; }
        [Inject] protected IJSRuntime JsRuntime { get; set; }
        protected SfGrid<Protarea> Grid;

        public bool Enabled = true;
        public bool Disabled = false;

        protected List<Protarea> pts = new List<Protarea>();

        protected List<Object> Toolbaritems = new List<Object>(){
        "Search",
        "Add",
        "Edit",
        "Delete",
        "Print",
        new ItemModel { Text = "Copy", TooltipText = "Copy", PrefixIcon = "e-copy", Id = "copy" },
        "ExcelExport"
    };

        protected override async Task OnInitializedAsync()
        {
            pts = await Http.GetFromJsonAsync<List<Protarea>>("api/Protarea");

            await base.OnInitializedAsync();
        }

        public void ActionBeginHandler(ActionEventArgs<Protarea> args)
        {
            if (args.RequestType == Syncfusion.Blazor.Grids.Action.BeginEdit)
            {
                this.Enabled = false;
            }
            else
            {
                this.Enabled = true;
            }
        }
        public async Task ActionBegin(ActionEventArgs<Protarea> args)
        {
            if (args.RequestType == Syncfusion.Blazor.Grids.Action.Save)
            {
                HttpResponseMessage response;
                bool found = pts.Any(o => o.TAREAPROC == args.Data.TAREAPROC);
                

                if (!found)
                {
                    args.Data.TAREAPROC = pts.Max(s => s.TAREAPROC) + 1;
                    response = await Http.PostAsJsonAsync("api/Protarea", args.Data);
                }
                else
                {
                    response = await Http.PutAsJsonAsync($"api/Protarea/{args.Data.TAREAPROC}", args.Data);
                }

                if (response.StatusCode == System.Net.HttpStatusCode.Created)
                {

                }
            }

            if (args.RequestType == Syncfusion.Blazor.Grids.Action.Delete)
            {
                await EliminarCeldas(args);
            }
        }

        private async Task EliminarCeldas(ActionEventArgs<Protarea> args)
        {
            try
            {
                if (args.Data != null)
                {
                    bool isConfirmed = await JsRuntime.InvokeAsync<bool>("confirm", "Seguro de que desea eliminar la Areas?");
                    if (isConfirmed)
                    {
                        //servicios.Remove(servicios.Find(m => m.PEDIDO == args.Data.PEDIDO));
                        await Http.DeleteAsync($"api/ProTarea/{args.Data.TAREAPROC}");
                    }
                }
            }
            catch (Exception ex)
            {

            }
        }

        public async Task ClickHandler(Syncfusion.Blazor.Navigations.ClickEventArgs args)
        {
            if (args.Item.Text == "Copy")
            {
                if (this.Grid.SelectedRecords.Count > 0)
                {
                    foreach (Protarea selectedRecord in this.Grid.SelectedRecords)
                    {
                        bool isConfirmed = await JsRuntime.InvokeAsync<bool>("confirm", "Seguro de que desea copiar el area?");
                        if (isConfirmed)
                        {
                            Protarea Nuevo = new Protarea();

                            Nuevo.TAREAPROC = pts.Max(s => s.TAREAPROC) + 1;
                            Nuevo.DESCRIP = selectedRecord.DESCRIP;
                            
                        
                            Nuevo.OBSERVAC = selectedRecord.OBSERVAC;
                            Nuevo.CG_CIA = selectedRecord.CG_CIA;
                         
               

                            var response = await Http.PostAsJsonAsync("api/Protarea", Nuevo);

                            if (response.StatusCode == System.Net.HttpStatusCode.Created)
                            {
                                Grid.Refresh();
                                var pt = await response.Content.ReadFromJsonAsync<Protarea>();
                                await InvokeAsync(StateHasChanged);
                                Nuevo.TAREAPROC = pt.TAREAPROC;
                                pts.Add(Nuevo);
                                var itemsJson = JsonSerializer.Serialize(pt);
                                Console.WriteLine(itemsJson);
                                pts.OrderByDescending(o => o.TAREAPROC);
                            }

                        }
                    }
                }
            }
            if (args.Item.Text == "Excel Export")
            {
                await this.Grid.ExcelExport();
            }
        }

        public void Refresh()
        {
            Grid.Refresh();
        }
    }
}
