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
namespace SupplyChain.Pages.TipoCeldas
{
    public class TipoCeldasPageBase : ComponentBase
    {
        [Inject] protected HttpClient Http { get; set; }
        [Inject] protected IJSRuntime JsRuntime { get; set; }
        protected SfGrid<TipoCelda> Grid;

        public bool Enabled = true;
        public bool Disabled = false;


        protected List<TipoCelda> tipoceldas = new List<TipoCelda>();


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
            tipoceldas = await Http.GetFromJsonAsync<List<TipoCelda>>("api/TipoCelda");
       

            await base.OnInitializedAsync();
        }

        public void ActionBeginHandler(ActionEventArgs<TipoCelda> args)
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

        public async Task ActionBegin(ActionEventArgs<TipoCelda> args)
        {
            if (args.RequestType == Syncfusion.Blazor.Grids.Action.Save)
            {
                HttpResponseMessage response;
                bool found = tipoceldas.Any(p => p.CG_TIPOCELDA == args.Data.CG_TIPOCELDA);
                TipoCelda ur = new TipoCelda();

                if (!found)
                {
                    args.Data.CG_CIA = 1;
                    args.Data.USUARIO = "User";
                    response = await Http.PostAsJsonAsync("api/TipoCelda", args.Data);
                    args.Data.CG_TIPOCELDA = tipoceldas.Max(s => s.CG_TIPOCELDA) + 1;
                }
                else
                {

                    response = await Http.PutAsJsonAsync($"api/TipoCelda/{args.Data.CG_TIPOCELDA}", args.Data);
                }

                if (response.StatusCode == System.Net.HttpStatusCode.Created)
                {

                }
            }

            if (args.RequestType == Syncfusion.Blazor.Grids.Action.Delete)
            {
                await EliminarTipoDeCelda(args);
            }
        }

        private async Task EliminarTipoDeCelda(ActionEventArgs<TipoCelda> args)
        {
            try
            {
                if (args.Data != null)
                {
                    bool isConfirmed = await JsRuntime.InvokeAsync<bool>("confirm", "Seguro de que desea eliminar el Tipo de Celda?");
                    if (isConfirmed)
                    {
                        //operarios.Remove(operarios.Find(m => m.CG_OPER == args.Data.CG_OPER));
                        await Http.DeleteAsync($"api/TipoCelda/{args.Data.CG_TIPOCELDA}");
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
                    foreach (TipoCelda selectedRecord in this.Grid.SelectedRecords)
                    {
                        bool isConfirmed = await JsRuntime.InvokeAsync<bool>("confirm", "Seguro de que desea copiar el Tipo de Celda?");
                        if (isConfirmed)
                        {
                            TipoCelda Nuevo = new TipoCelda();

                            Nuevo.DES_TIPOCELDA = selectedRecord.DES_TIPOCELDA;
                            Nuevo.CG_CIA = 1;
                            Nuevo.USUARIO = "User";

                            var response = await Http.PostAsJsonAsync("api/TipoCelda", Nuevo);
                            Nuevo.CG_TIPOCELDA = tipoceldas.Max(s => s.CG_TIPOCELDA) + 1;

                            if (response.StatusCode == System.Net.HttpStatusCode.Created)
                            {
                                Grid.Refresh();
                                var tipocelda = await response.Content.ReadFromJsonAsync<TipoCelda>();
                                await InvokeAsync(StateHasChanged);
                                Nuevo.CG_TIPOCELDA = tipocelda.CG_TIPOCELDA;
                                tipoceldas.Add(Nuevo);
                                var itemsJson = JsonSerializer.Serialize(tipocelda);
                                Console.WriteLine(itemsJson);
                                //toastService.ShowToast($"Registrado Correctemente.Vale {StockGuardado.VALE}", TipoAlerta.Success);
                                tipoceldas.OrderByDescending(p => p.CG_TIPOCELDA);
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
