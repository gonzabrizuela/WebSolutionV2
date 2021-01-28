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
namespace SupplyChain.Pages.Areas
{
    public class AreasPageBase : ComponentBase
    {
        [Inject] protected HttpClient Http { get; set; }

        [Inject] protected IJSRuntime JsRuntime { get; set; }
        protected SfGrid<Area> Grid;

        public bool Enabled = true;
        public bool Disabled = false;


        protected List<Area> areas = new List<Area>();


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
            areas = await Http.GetFromJsonAsync<List<Area>>("api/Area");

            await base.OnInitializedAsync();
        }

        public void ActionBeginHandler(ActionEventArgs<Area> args)
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

        public async Task ActionBegin(ActionEventArgs<Area> args)
        {
            if (args.RequestType == Syncfusion.Blazor.Grids.Action.Save)
            {
                HttpResponseMessage response;
                bool found = areas.Any(p => p.CG_AREA == args.Data.CG_AREA);

                if (!found)
                {
                    args.Data.CG_AREA = areas.Max(s => s.CG_AREA) + 1;

                    args.Data.CG_CIA = 1;
                    args.Data.USUARIO = "User";
                    response = await Http.PostAsJsonAsync("api/Area", args.Data);
                }
                else
                {
                    response = await Http.PutAsJsonAsync($"api/Area/{args.Data.CG_AREA}", args.Data);
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

        private async Task EliminarCategoriaOperarios(ActionEventArgs<Area> args)
        {
            try
            {
                if (args.Data != null)
                {
                    bool isConfirmed = await JsRuntime.InvokeAsync<bool>("confirm", "Seguro de que desea eliminar la clase?");
                    if (isConfirmed)
                    {
                        //operarios.Remove(operarios.Find(m => m.CG_OPER == args.Data.CG_OPER));
                        await Http.DeleteAsync($"api/Areas/{args.Data.CG_AREA}");
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
                    foreach (Area selectedRecord in this.Grid.SelectedRecords)
                    {
                        bool isConfirmed = await JsRuntime.InvokeAsync<bool>("confirm", "Seguro de que desea copiar la Clase?");
                        if (isConfirmed)
                        {
                            Area Nuevo = new Area();

                            Nuevo.CG_AREA = areas.Max(s => s.CG_AREA) + 1;

                            Nuevo.DES_AREA = selectedRecord.DES_AREA;
                            Nuevo.CG_TIPOAREA = selectedRecord.CG_TIPOAREA;
                            Nuevo.CG_PROVE = selectedRecord.CG_PROVE;
                            Nuevo.CG_CIA = 1;
                            Nuevo.USUARIO = "User";

                            var response = await Http.PostAsJsonAsync("api/Area", Nuevo);

                            if (response.StatusCode == System.Net.HttpStatusCode.Created)
                            {
                                Grid.Refresh();
                                var a = await response.Content.ReadFromJsonAsync<Area>();
                                await InvokeAsync(StateHasChanged);
                                Nuevo.CG_AREA = a.CG_AREA;
                                areas.Add(Nuevo);
                                var itemsJson = JsonSerializer.Serialize(a);
                                Console.WriteLine(itemsJson);
                                //toastService.ShowToast($"Registrado Correctemente.Vale {StockGuardado.VALE}", TipoAlerta.Success);
                                areas.OrderByDescending(p => p.CG_AREA);
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
