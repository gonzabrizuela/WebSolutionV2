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
namespace SupplyChain.Pages.Clases
{
    public class ClasePageBase : ComponentBase
    {
        [Inject] protected HttpClient Http { get; set; }
        [Inject] protected IJSRuntime JsRuntime { get; set; }
        protected SfGrid<Clase> Grid;

        public bool Enabled = true;
        public bool Disabled = false;


        protected List<Clase> clases = new List<Clase>();


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
            clases = await Http.GetFromJsonAsync<List<Clase>>("api/Clase");

            await base.OnInitializedAsync();
        }

        public void ActionBeginHandler(ActionEventArgs<Clase> args)
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

        public async Task ActionBegin(ActionEventArgs<Clase> args)
        {
            if (args.RequestType == Syncfusion.Blazor.Grids.Action.Save)
            {
                HttpResponseMessage response;
                bool found = clases.Any(p => p.CG_CLASE == args.Data.CG_CLASE);
                Clase ur = new Clase();

                if (!found)
                {
                    args.Data.CG_CIA = 1;
                    args.Data.USUARIO = "User";
                    response = await Http.PostAsJsonAsync("api/Clase", args.Data);
                    args.Data.CG_CLASE = clases.Max(s => s.CG_CLASE) + 1;
                }
                else
                {
                    response = await Http.PutAsJsonAsync($"api/Clase/{args.Data.CG_CLASE}", args.Data);
                }

                if (response.StatusCode == System.Net.HttpStatusCode.Created)
                {

                }
            }

            if (args.RequestType == Syncfusion.Blazor.Grids.Action.Delete)
            {
                await EliminarClase(args);
            }
        }

        private async Task EliminarClase(ActionEventArgs<Clase> args)
        {
            try
            {
                if (args.Data != null)
                {
                    bool isConfirmed = await JsRuntime.InvokeAsync<bool>("confirm", "Seguro de que desea eliminar la clase?");
                    if (isConfirmed)
                    {
                        //operarios.Remove(operarios.Find(m => m.CG_OPER == args.Data.CG_OPER));
                        await Http.DeleteAsync($"api/Clase/{args.Data.CG_CLASE}");
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
                    foreach (Clase selectedRecord in this.Grid.SelectedRecords)
                    {
                        bool isConfirmed = await JsRuntime.InvokeAsync<bool>("confirm", "Seguro de que desea copiar la Clase?");
                        if (isConfirmed)
                        {
                            Clase Nuevo = new Clase();

                            Nuevo.DES_CLASE = selectedRecord.DES_CLASE;
                            Nuevo.CG_CIA = 1;
                            Nuevo.USUARIO = "User";

                            var response = await Http.PostAsJsonAsync("api/Clase", Nuevo);
                            Nuevo.CG_CLASE = clases.Max(s => s.CG_CLASE) + 1;

                            if (response.StatusCode == System.Net.HttpStatusCode.Created)
                            {
                                Grid.Refresh();
                                var clase = await response.Content.ReadFromJsonAsync<Clase>();
                                await InvokeAsync(StateHasChanged);
                                Nuevo.CG_CLASE = clase.CG_CLASE;
                                clases.Add(Nuevo);
                                var itemsJson = JsonSerializer.Serialize(clase);
                                Console.WriteLine(itemsJson);
                                //toastService.ShowToast($"Registrado Correctemente.Vale {StockGuardado.VALE}", TipoAlerta.Success);
                                clases.OrderByDescending(p => p.CG_CLASE);
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
