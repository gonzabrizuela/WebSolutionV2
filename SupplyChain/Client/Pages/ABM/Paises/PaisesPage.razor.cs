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
namespace SupplyChain.Pages.Paisex
{
    public class PaisesPageBase : ComponentBase
    {
        [Inject] protected HttpClient Http { get; set; }
        [Inject] protected IJSRuntime JsRuntime { get; set; }
        protected SfGrid<Pais> Grid;

        public bool Enabled = true;
        public bool Disabled = false;


        protected List<Pais> paises = new List<Pais>();


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
            paises = await Http.GetFromJsonAsync<List<Pais>>("api/Pais");

            await base.OnInitializedAsync();
        }

        public void ActionBeginHandler(ActionEventArgs<Pais> args)
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

        public async Task ActionBegin(ActionEventArgs<Pais> args)
        {
            if (args.RequestType == Syncfusion.Blazor.Grids.Action.Save)
            {
                HttpResponseMessage response;
                bool found = paises.Any(p => p.Cg_Pais == args.Data.Cg_Pais);

                if (!found)
                {
                    
                    args.Data.Cg_Pais = paises.Max(s => s.Cg_Pais) + 1;
                  
                    response = await Http.PostAsJsonAsync("api/Pais", args.Data);
                }
                else
                {
                    response = await Http.PutAsJsonAsync($"api/Pais/{args.Data.Cg_Pais}", args.Data);
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

        private async Task EliminarCategoriaOperarios(ActionEventArgs<Pais> args)
        {
            try
            {
                if (args.Data != null)
                {
                    bool isConfirmed = await JsRuntime.InvokeAsync<bool>("confirm", "Seguro de que desea eliminar la clase?");
                    if (isConfirmed)
                    {
                        //operarios.Remove(operarios.Find(m => m.CG_OPER == args.Data.CG_OPER));
                        await Http.DeleteAsync($"api/Pais/{args.Data.Cg_Pais}");
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
                    foreach (Pais selectedRecord in this.Grid.SelectedRecords)
                    {
                        bool isConfirmed = await JsRuntime.InvokeAsync<bool>("confirm", "Seguro de que desea copiar la Clase?");
                        if (isConfirmed)
                        {
                            Pais Nuevo = new Pais();
                            Nuevo.Cg_Pais = paises.Max(s => s.Cg_Pais) + 1;
                            Nuevo.Codigo = selectedRecord.Codigo;
                            Nuevo.Des_Pais = selectedRecord.Des_Pais;
                            Nuevo.Mercosur = selectedRecord.Mercosur;
                            Nuevo.Cuit = selectedRecord.Cuit;


                            var response = await Http.PostAsJsonAsync("api/Pais", Nuevo);

                            if (response.StatusCode == System.Net.HttpStatusCode.Created)
                            {
                                Grid.Refresh();
                                var p = await response.Content.ReadFromJsonAsync<Pais>();
                                await InvokeAsync(StateHasChanged);
                                Nuevo.Cg_Pais = p.Cg_Pais;
                                paises.Add(Nuevo);
                                var itemsJson = JsonSerializer.Serialize(p);
                                Console.WriteLine(itemsJson);
                                //toastService.ShowToast($"Registrado Correctemente.Vale {StockGuardado.VALE}", TipoAlerta.Success);
                                paises.OrderByDescending(p => p.Cg_Pais);
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
