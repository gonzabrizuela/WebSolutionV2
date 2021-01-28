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

namespace SupplyChain.Pages.Lineass
{
    public class LineassPageBase : ComponentBase
    {
        [Inject] protected HttpClient Http { get; set; }
        [Inject] protected IJSRuntime JsRuntime { get; set; }
        protected SfGrid<Lineas> Grid;

        public bool Enabled = true;
        public bool Disabled = false;


        protected List<Lineas> lineas = new List<Lineas>();


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
            lineas = await Http.GetFromJsonAsync<List<Lineas>>("api/Lineas");

            await base.OnInitializedAsync();
        }

        public void ActionBeginHandler(ActionEventArgs<Lineas> args)
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

        public async Task ActionBegin(ActionEventArgs<Lineas> args)
        {
            if (args.RequestType == Syncfusion.Blazor.Grids.Action.Save)
            {
                HttpResponseMessage response;
                bool found = lineas.Any(p => p.CG_LINEA == args.Data.CG_LINEA);
                Lineas ur = new Lineas();

                if (!found)
                {
                    args.Data.CG_CIA = 1;
                    args.Data.USUARIO = "User";
                    response = await Http.PostAsJsonAsync("api/Lineas", args.Data);
                    args.Data.CG_LINEA = lineas.Max(s => s.CG_LINEA) + 1;
                }
                else
                {
                    response = await Http.PutAsJsonAsync($"api/Lineas/{args.Data.CG_LINEA}", args.Data);
                }

                if (response.StatusCode == System.Net.HttpStatusCode.Created)
                {

                }
            }

            if (args.RequestType == Syncfusion.Blazor.Grids.Action.Delete)
            {
                await EliminarLinea(args);
            }
        }

        private async Task EliminarLinea(ActionEventArgs<Lineas> args)
        {
            try
            {
                if (args.Data != null)
                {
                    bool isConfirmed = await JsRuntime.InvokeAsync<bool>("confirm", "Seguro de que desea eliminar la clase?");
                    if (isConfirmed)
                    {
                        //operarios.Remove(operarios.Find(m => m.CG_OPER == args.Data.CG_OPER));
                        await Http.DeleteAsync($"api/Lineas/{args.Data.CG_LINEA}");
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
                    foreach (Lineas selectedRecord in this.Grid.SelectedRecords)
                    {
                        bool isConfirmed = await JsRuntime.InvokeAsync<bool>("confirm", "Seguro de que desea copiar la Clase?");
                        if (isConfirmed)
                        {
                            Lineas Nuevo = new Lineas();

                            Nuevo.DES_LINEA = selectedRecord.DES_LINEA;
                            Nuevo.CG_CIA = 1;
                            Nuevo.USUARIO = "User";

                            var response = await Http.PostAsJsonAsync("api/Lineas", Nuevo);
                            Nuevo.CG_LINEA = lineas.Max(s => s.CG_LINEA) + 1;

                            if (response.StatusCode == System.Net.HttpStatusCode.Created)
                            {
                                Grid.Refresh();
                                var scrap = await response.Content.ReadFromJsonAsync<Lineas>();
                                await InvokeAsync(StateHasChanged);
                                Nuevo.CG_LINEA = scrap.CG_LINEA;
                                lineas.Add(Nuevo);
                                var itemsJson = JsonSerializer.Serialize(scrap);
                                Console.WriteLine(itemsJson);
                                //toastService.ShowToast($"Registrado Correctemente.Vale {StockGuardado.VALE}", TipoAlerta.Success);
                                lineas.OrderByDescending(p => p.CG_LINEA);
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
