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
namespace SupplyChain.Pages.Protabx
{
    public class ProtabPageBase : ComponentBase
    {
        [Inject] protected HttpClient Http { get; set; }
        [Inject] protected IJSRuntime JsRuntime { get; set; }
        protected SfGrid<Protab> Grid;

        public bool Enabled = true;
        public bool Disabled = false;


        protected List<Protab> protabs = new List<Protab>();


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
            protabs = await Http.GetFromJsonAsync<List<Protab>>("api/Protab");

            await base.OnInitializedAsync();
        }

        public void ActionBeginHandler(ActionEventArgs<Protab> args)
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

        public async Task ActionBegin(ActionEventArgs<Protab> args)
        {
            if (args.RequestType == Syncfusion.Blazor.Grids.Action.Save)
            {
                HttpResponseMessage response;
                bool found = protabs.Any(p => p.PROCESO == args.Data.PROCESO);

                if (!found)
                {
                    
                    args.Data.PROCESO = protabs.Max(s => s.PROCESO) + 1;
                  
                    response = await Http.PostAsJsonAsync("api/Protab", args.Data);
                }
                else
                {
                    response = await Http.PutAsJsonAsync($"api/Protab/{args.Data.PROCESO}", args.Data);
                }

                if (response.StatusCode == System.Net.HttpStatusCode.Created)
                {

                }
            }

            if (args.RequestType == Syncfusion.Blazor.Grids.Action.Delete)
            {
                await EliminarCategoriaOperarios(args);
            }
        }

        private async Task EliminarCategoriaOperarios(ActionEventArgs<Protab> args)
        {
            try
            {
                if (args.Data != null)
                {
                    bool isConfirmed = await JsRuntime.InvokeAsync<bool>("confirm", "Seguro de que desea eliminar la clase?");
                    if (isConfirmed)
                    {
                        //operarios.Remove(operarios.Find(m => m.CG_OPER == args.Data.CG_OPER));
                        await Http.DeleteAsync($"api/Protab/{args.Data.PROCESO}");
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
                    foreach (Protab selectedRecord in this.Grid.SelectedRecords)
                    {
                        bool isConfirmed = await JsRuntime.InvokeAsync<bool>("confirm", "Seguro de que desea copiar la Clase?");
                        if (isConfirmed)
                        {
                            Protab Nuevo = new Protab();
                            Nuevo.PROCESO = protabs.Max(s => s.PROCESO) + 1;
                            Nuevo.DESCRIP = selectedRecord.DESCRIP;
                            Nuevo.OBSERVAC = selectedRecord.OBSERVAC;
                            Nuevo.CG_CIA = selectedRecord.CG_CIA;
                            Nuevo.USUARIO = selectedRecord.USUARIO;


                            var response = await Http.PostAsJsonAsync("api/Protab", Nuevo);

                            if (response.StatusCode == System.Net.HttpStatusCode.Created)
                            {
                                Grid.Refresh();
                                var p = await response.Content.ReadFromJsonAsync<Protab>();
                                await InvokeAsync(StateHasChanged);
                                Nuevo.PROCESO = p.PROCESO;
                                protabs.Add(Nuevo);
                                var itemsJson = JsonSerializer.Serialize(p);
                                Console.WriteLine(itemsJson);
                                //toastService.ShowToast($"Registrado Correctemente.Vale {StockGuardado.VALE}", TipoAlerta.Success);
                                protabs.OrderByDescending(p => p.PROCESO);
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
