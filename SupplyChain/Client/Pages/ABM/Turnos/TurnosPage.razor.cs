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
namespace SupplyChain.Pages.Turnoss
{
    public class TurnosPageBase : ComponentBase
    {
        [Inject] protected HttpClient Http { get; set; }
        [Inject] protected IJSRuntime JsRuntime { get; set; }
        protected SfGrid<Turnos> Grid;

        public bool Enabled = true;
        public bool Disabled = false;


        protected List<Turnos> turnitos = new List<Turnos>();


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
            turnitos = await Http.GetFromJsonAsync<List<Turnos>>("api/Turnos");
       

            await base.OnInitializedAsync();
        }

        public void ActionBeginHandler(ActionEventArgs<Turnos> args)
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

        public async Task ActionBegin(ActionEventArgs<Turnos> args)
        {
            if (args.RequestType == Syncfusion.Blazor.Grids.Action.Save)
            {
                HttpResponseMessage response;
                bool found = turnitos.Any(p => p.CG_TURNO == args.Data.CG_TURNO);
                Turnos ur = new Turnos();

                if (!found)
                {
                  
                    response = await Http.PostAsJsonAsync("api/Turnos", args.Data);
                    args.Data.CG_TURNO = turnitos.Max(s => s.CG_TURNO) + 1;
                }
                else
                {
                    response = await Http.PutAsJsonAsync($"api/Turnos/{args.Data.CG_TURNO}", args.Data);
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

        private async Task EliminarTipoDeArea(ActionEventArgs<Turnos> args)
        {
            try
            {
                if (args.Data != null)
                {
                    bool isConfirmed = await JsRuntime.InvokeAsync<bool>("confirm", "Seguro de que desea eliminar el Tipo de Area?");
                    if (isConfirmed)
                    {
                        //operarios.Remove(operarios.Find(m => m.CG_OPER == args.Data.CG_OPER));
                        await Http.DeleteAsync($"api/Turnos/{args.Data.CG_TURNO}");
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
                    foreach (Turnos selectedRecord in this.Grid.SelectedRecords)
                    {
                        bool isConfirmed = await JsRuntime.InvokeAsync<bool>("confirm", "Seguro de que desea copiar el Tipo de Area?");
                        if (isConfirmed)
                        {
                            Turnos Nuevo = new Turnos();

                           

                            var response = await Http.PostAsJsonAsync("api/Turnos", Nuevo);
                            Nuevo.CG_TURNO = turnitos.Max(s => s.CG_TURNO) + 1;

                            if (response.StatusCode == System.Net.HttpStatusCode.Created)
                            {
                                Grid.Refresh();
                                var t = await response.Content.ReadFromJsonAsync<Turnos>();
                                await InvokeAsync(StateHasChanged);
                                Nuevo.CG_TURNO = t.CG_TURNO;
                                turnitos.Add(Nuevo);
                                var itemsJson = JsonSerializer.Serialize(t);
                                Console.WriteLine(itemsJson);
                                //toastService.ShowToast($"Registrado Correctemente.Vale {StockGuardado.VALE}", TipoAlerta.Success);
                                turnitos.OrderByDescending(p => p.CG_TURNO);
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
