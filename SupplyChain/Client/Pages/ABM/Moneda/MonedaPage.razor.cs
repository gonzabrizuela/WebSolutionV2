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
namespace SupplyChain.Pages.Monedass
{
    public class MonedaPageBase : ComponentBase
    {
        [Inject] protected HttpClient Http { get; set; }
        [Inject] protected IJSRuntime JsRuntime { get; set; }
        protected SfGrid<Monedas> Grid;

        public bool Enabled = true;
        public bool Disabled = false;


        protected List<Monedas> monedas = new List<Monedas>();


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
            monedas = await Http.GetFromJsonAsync<List<Monedas>>("api/Monedas");
       

            await base.OnInitializedAsync();
        }

        public void ActionBeginHandler(ActionEventArgs<Monedas> args)
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
        public class Moneda
        {
            public string ID { get; set; }
            public string Text { get; set; }
        }
        List<Moneda> MonedaData = new List<Moneda> {
            new Moneda() { ID= "Mon1", Text= "Peso Argentino"},
            new Moneda() { ID= "Mon2", Text= "Dolar"},
            new Moneda() { ID= "Mon3", Text= "Euro"}
        };

        public class EstaActivo
        {
            public bool BActivo { get; set; }
            public string Text { get; set; }
        }
        protected List<EstaActivo> ActivoData = new List<EstaActivo> {
            new EstaActivo() { BActivo= true, Text= "SI"},
            new EstaActivo() { BActivo= false, Text= "NO"}};

        public async Task ActionBegin(ActionEventArgs<Monedas> args)
        {
            if (args.RequestType == Syncfusion.Blazor.Grids.Action.Save)
            {
                HttpResponseMessage response;
                bool found = monedas.Any(p => p.MONEDA == args.Data.MONEDA);
                Turnos ur = new Turnos();

                if (!found)
                {
                  
                    response = await Http.PostAsJsonAsync("api/Monedas", args.Data);
                    args.Data.MONEDA = monedas.Max(s => s.MONEDA) + 1;
                }
                else
                {
                    response = await Http.PutAsJsonAsync($"api/Monedas/{args.Data.MONEDA}", args.Data);
                }

                if (response.StatusCode == System.Net.HttpStatusCode.Created)
                {

                }
            }

            if (args.RequestType == Syncfusion.Blazor.Grids.Action.Delete)
            {
                await EliminarTipoDeArea(args);
            }
        }

        private async Task EliminarTipoDeArea(ActionEventArgs<Monedas> args)
        {
            try
            {
                if (args.Data != null)
                {
                    bool isConfirmed = await JsRuntime.InvokeAsync<bool>("confirm", "Seguro de que desea eliminar el Tipo de Area?");
                    if (isConfirmed)
                    {
                        //operarios.Remove(operarios.Find(m => m.CG_OPER == args.Data.CG_OPER));
                        await Http.DeleteAsync($"api/Monedas/{args.Data.MONEDA}");
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
                    foreach (Monedas selectedRecord in this.Grid.SelectedRecords)
                    {
                        bool isConfirmed = await JsRuntime.InvokeAsync<bool>("confirm", "Seguro de que desea copiar el Tipo de Area?");
                        if (isConfirmed)
                        {
                            Monedas Nuevo = new Monedas();

                           

                            var response = await Http.PostAsJsonAsync("api/Monedas", Nuevo);
                            Nuevo.MONEDA = monedas.Max(s => s.MONEDA) + 1;

                            if (response.StatusCode == System.Net.HttpStatusCode.Created)
                            {
                                Grid.Refresh();
                                var m = await response.Content.ReadFromJsonAsync<Monedas>();
                                await InvokeAsync(StateHasChanged);
                                Nuevo.MONEDA = m.MONEDA;
                                monedas.Add(Nuevo);
                                var itemsJson = JsonSerializer.Serialize(m);
                                Console.WriteLine(itemsJson);
                                //toastService.ShowToast($"Registrado Correctemente.Vale {StockGuardado.VALE}", TipoAlerta.Success);
                                monedas.OrderByDescending(p => p.MONEDA);
                            }
                        }
                    }
                }
                Refresh();
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
