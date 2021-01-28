﻿using Microsoft.AspNetCore.Components;
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
namespace SupplyChain.Pages.Celda
{
    public class CeldaPageBase : ComponentBase
    {
        [Inject] protected HttpClient Http { get; set; }
        [Inject] protected IJSRuntime JsRuntime { get; set; }
        protected SfGrid<Celdas> Grid;

        public bool Enabled = true;
        public bool Disabled = false;

        protected List<Celdas> celdas = new List<Celdas>();

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
            celdas = await Http.GetFromJsonAsync<List<Celdas>>("api/Celda");

            await base.OnInitializedAsync();
        }

        public void ActionBeginHandler(ActionEventArgs<Celdas> args)
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
        public async Task ActionBegin(ActionEventArgs<Celdas> args)
        {
            if (args.RequestType == Syncfusion.Blazor.Grids.Action.Save)
            {
                HttpResponseMessage response;
                bool found = celdas.Any(o => o.CG_CELDA == args.Data.CG_CELDA);

                if (!found)
                {
                    args.Data.CG_CELDA = celdas.Max(s => s.CG_CELDA) + 1;
                    response = await Http.PostAsJsonAsync("api/Celda", args.Data);
                }
                else
                {
                    response = await Http.PutAsJsonAsync($"api/Celda/{args.Data.CG_CELDA}", args.Data);
                }

                if (response.StatusCode == System.Net.HttpStatusCode.Created)
                {

                }
            }

            if (args.RequestType == Syncfusion.Blazor.Grids.Action.Delete)
            {
                await EliminarCeldas(args);
            }
        }

        private async Task EliminarCeldas(ActionEventArgs<Celdas> args)
        {
            try
            {
                if (args.Data != null)
                {
                    bool isConfirmed = await JsRuntime.InvokeAsync<bool>("confirm", "Seguro de que desea eliminar la Celda?");
                    if (isConfirmed)
                    {
                        //servicios.Remove(servicios.Find(m => m.PEDIDO == args.Data.PEDIDO));
                        await Http.DeleteAsync($"api/Celda/{args.Data.CG_CELDA}");
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
                    foreach (Celdas selectedRecord in this.Grid.SelectedRecords)
                    {
                        bool isConfirmed = await JsRuntime.InvokeAsync<bool>("confirm", "Seguro de que desea copiar la celda?");
                        if (isConfirmed)
                        {
                            Celdas Nuevo = new Celdas();

                            Nuevo.CG_CELDA = celdas.Max(s => s.CG_CELDA) + 1;
                            Nuevo.DES_CELDA = selectedRecord.DES_CELDA;
                            Nuevo.CG_AREA = selectedRecord.CG_AREA;
                       
                            Nuevo.ILIMITADA = selectedRecord.ILIMITADA;
                           
                        
                            Nuevo.COEFI = selectedRecord.COEFI;
                      
                            Nuevo.CG_PROVE = selectedRecord.CG_PROVE;
                      
                            Nuevo.VALOR_AMOR = selectedRecord.VALOR_AMOR;
                            Nuevo.VALOR_MERC = selectedRecord.VALOR_MERC;
                            Nuevo.MONEDA = selectedRecord.MONEDA;
                            Nuevo.CANT_ANOS = selectedRecord.CANT_ANOS;
                            Nuevo.CANT_UNID = selectedRecord.CANT_UNID;
                            Nuevo.REP_ANOS = selectedRecord.REP_ANOS;
                            Nuevo.M2 = selectedRecord.M2;
                            Nuevo.ENERGIA = selectedRecord.ENERGIA;
                            Nuevo.COMBUST = selectedRecord.COMBUST;
                            Nuevo.AIRE_COMP = selectedRecord.AIRE_COMP;
                  
                            Nuevo.CG_TIPOCELDA = selectedRecord.CG_TIPOCELDA;
                            Nuevo.CG_DEPOSM = selectedRecord.CG_DEPOSM;
                        

                            var response = await Http.PostAsJsonAsync("api/Celda", Nuevo);

                            if (response.StatusCode == System.Net.HttpStatusCode.Created)
                            {
                                Grid.Refresh();
                                var celda = await response.Content.ReadFromJsonAsync<Celdas>();
                                await InvokeAsync(StateHasChanged);
                                Nuevo.CG_CELDA = celda.CG_CELDA;
                                celdas.Add(Nuevo);
                                var itemsJson = JsonSerializer.Serialize(celda);
                                Console.WriteLine(itemsJson);
                                celdas.OrderByDescending(o => o.CG_CELDA);
                            }

                        }
                    }
                }
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
