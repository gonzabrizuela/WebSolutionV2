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
namespace SupplyChain.Pages.TipoAreas
{
    public class TipoAreaPageBase : ComponentBase
    {
        [Inject] protected HttpClient Http { get; set; }
        [Inject] protected IJSRuntime JsRuntime { get; set; }
        protected SfGrid<TipoArea> Grid;

        public bool Enabled = true;
        public bool Disabled = false;


        protected List<TipoArea> tipoareas = new List<TipoArea>();


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
            tipoareas = await Http.GetFromJsonAsync<List<TipoArea>>("api/TipoArea");
       

            await base.OnInitializedAsync();
        }

        public void ActionBeginHandler(ActionEventArgs<TipoArea> args)
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

        public async Task ActionBegin(ActionEventArgs<TipoArea> args)
        {
            if (args.RequestType == Syncfusion.Blazor.Grids.Action.Save)
            {
                HttpResponseMessage response;
                bool found = tipoareas.Any(p => p.CG_TIPOAREA == args.Data.CG_TIPOAREA);
                TipoArea ur = new TipoArea();

                if (!found)
                {
                    args.Data.CG_CIA = 1;
                    args.Data.USUARIO = "User";
                    response = await Http.PostAsJsonAsync("api/TipoArea", args.Data);
                    args.Data.CG_TIPOAREA = tipoareas.Max(s => s.CG_TIPOAREA) + 1;
                }
                else
                {
                    response = await Http.PutAsJsonAsync($"api/TipoArea/{args.Data.CG_TIPOAREA}", args.Data);
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

        private async Task EliminarTipoDeArea(ActionEventArgs<TipoArea> args)
        {
            try
            {
                if (args.Data != null)
                {
                    bool isConfirmed = await JsRuntime.InvokeAsync<bool>("confirm", "Seguro de que desea eliminar el Tipo de Area?");
                    if (isConfirmed)
                    {
                        //operarios.Remove(operarios.Find(m => m.CG_OPER == args.Data.CG_OPER));
                        await Http.DeleteAsync($"api/TipoArea/{args.Data.CG_TIPOAREA}");
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
                    foreach (TipoArea selectedRecord in this.Grid.SelectedRecords)
                    {
                        bool isConfirmed = await JsRuntime.InvokeAsync<bool>("confirm", "Seguro de que desea copiar el Tipo de Area?");
                        if (isConfirmed)
                        {
                            TipoArea Nuevo = new TipoArea();

                            Nuevo.DES_TIPOAREA = selectedRecord.DES_TIPOAREA;
                            Nuevo.CG_CIA = 1;
                            Nuevo.USUARIO = "User";

                            var response = await Http.PostAsJsonAsync("api/TipoArea", Nuevo);
                            Nuevo.CG_TIPOAREA = tipoareas.Max(s => s.CG_TIPOAREA) + 1;

                            if (response.StatusCode == System.Net.HttpStatusCode.Created)
                            {
                                Grid.Refresh();
                                var tipoarea = await response.Content.ReadFromJsonAsync<TipoArea>();
                                await InvokeAsync(StateHasChanged);
                                Nuevo.CG_TIPOAREA = tipoarea.CG_TIPOAREA;
                                tipoareas.Add(Nuevo);
                                var itemsJson = JsonSerializer.Serialize(tipoarea);
                                Console.WriteLine(itemsJson);
                                //toastService.ShowToast($"Registrado Correctemente.Vale {StockGuardado.VALE}", TipoAlerta.Success);
                                tipoareas.OrderByDescending(p => p.CG_TIPOAREA);
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
