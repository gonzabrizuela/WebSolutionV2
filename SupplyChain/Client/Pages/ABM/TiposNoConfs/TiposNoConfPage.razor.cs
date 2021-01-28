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

namespace SupplyChain.Pages.TiposNoConfs
{
    public class TiposNoConfsPageBase : ComponentBase
    {
        [Inject] protected HttpClient Http { get; set; }
        [Inject] protected IJSRuntime JsRuntime { get; set; }
        protected SfGrid<TiposNoConf> Grid;

        public bool Enabled = true;
        public bool Disabled = false;


        protected List<TiposNoConf> tiposnoconfs = new List<TiposNoConf>();


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
            tiposnoconfs = await Http.GetFromJsonAsync<List<TiposNoConf>>("api/TiposNoConf");


            await base.OnInitializedAsync();
        }

        public void ActionBeginHandler(ActionEventArgs<TiposNoConf> args)
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

        public async Task ActionBegin(ActionEventArgs<TiposNoConf> args)
        {
            if (args.RequestType == Syncfusion.Blazor.Grids.Action.Save)
            {
                HttpResponseMessage response;
                bool found = tiposnoconfs.Any(p => p.Cg_TipoNc == args.Data.Cg_TipoNc);
                TiposNoConf ur = new TiposNoConf();

                if (!found)
                {
                    args.Data.CG_CIA = 1;
                    args.Data.USUARIO = "User";
                    response = await Http.PostAsJsonAsync("api/TiposNoConf", args.Data);
                    args.Data.Cg_TipoNc = tiposnoconfs.Max(s => s.Cg_TipoNc) + 1;
                }
                else
                {
                    response = await Http.PutAsJsonAsync($"api/TiposNoConf/{args.Data.Cg_TipoNc}", args.Data);
                }

                if (response.StatusCode == System.Net.HttpStatusCode.Created)
                {

                }
            }

            if (args.RequestType == Syncfusion.Blazor.Grids.Action.Delete)
            {
                await EliminarTiposNoConf(args);
            }
        }

        private async Task EliminarTiposNoConf(ActionEventArgs<TiposNoConf> args)
        {
            try
            {
                if (args.Data != null)
                {
                    bool isConfirmed = await JsRuntime.InvokeAsync<bool>("confirm", "Seguro de que desea eliminar el Tipo no Conforme?");
                    if (isConfirmed)
                    {
                        //operarios.Remove(operarios.Find(m => m.CG_OPER == args.Data.CG_OPER));
                        await Http.DeleteAsync($"api/TiposNoConf/{args.Data.Cg_TipoNc}");
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
                    foreach (TiposNoConf selectedRecord in this.Grid.SelectedRecords)
                    {
                        bool isConfirmed = await JsRuntime.InvokeAsync<bool>("confirm", "Seguro de que desea copiar Tipo no Conforme?");
                        if (isConfirmed)
                        {
                            TiposNoConf Nuevo = new TiposNoConf();

                            Nuevo.Des_TipoNc = selectedRecord.Des_TipoNc;
                            Nuevo.CG_CIA = 1;
                            Nuevo.USUARIO = "User";

                            var response = await Http.PostAsJsonAsync("api/TiposNoConf", Nuevo);
                            Nuevo.Cg_TipoNc = tiposnoconfs.Max(s => s.Cg_TipoNc) + 1;

                            if (response.StatusCode == System.Net.HttpStatusCode.Created)
                            {
                                Grid.Refresh();
                                var TiposNoConf = await response.Content.ReadFromJsonAsync<TiposNoConf>();
                                await InvokeAsync(StateHasChanged);
                                Nuevo.Cg_TipoNc = TiposNoConf.Cg_TipoNc;
                                tiposnoconfs.Add(Nuevo);
                                var itemsJson = JsonSerializer.Serialize(TiposNoConf);
                                Console.WriteLine(itemsJson);
                                //toastService.ShowToast($"Registrado Correctemente.Vale {StockGuardado.VALE}", TipoAlerta.Success);
                                tiposnoconfs.OrderByDescending(p => p.Cg_TipoNc);
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
