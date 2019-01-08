using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.ModelBinding;
using Tlahui.Domain.Base.Entities;

namespace Tlahui.Web.API.Models.modelbinders
{

    

    public class APISearchModelBinder : IModelBinder
    {
     
        public bool BindModel(HttpActionContext actionContext, ModelBindingContext bindingContext)
        {
            List<string> ConditionFields = new List<string>() { "n", "o", "v", "t" };
            APISearch search = new APISearch();
            bindingContext.Model = search;
                    
            string ct = actionContext.Request.Content.ReadAsStringAsync().Result;
            List<string> parameters = actionContext.Request.RequestUri.Query.TrimStart('?').Split('&').ToList();

            foreach (string p in parameters) {
                List<string> par = p.Split('=').ToList();
                switch (par[0].ToLower()) {
                    case "p":
                        search.page = int.Parse(par[1]);
                        break;

                    case "g":
                        search.gmt= par[1];
                        break;

                    case "l":
                        search.lang = par[1];
                        break;

                    case "ps":
                        search.pagesize = int.Parse(par[1]);
                        break;

                    case "s":
                        search.sortby = par[1];
                        break;

                    case "d":
                        search.sortdirection = par[1];
                        break;

                    case "dd":
                        search.datetimeasdate = (par[1].ToLower() == "true") ? true : false;
                        break;

                    case "gd":
                        search.ShowDeleted = (par[1].ToLower() == "true") ? true : false;
                        break;

                    default:
                        if (par[0].StartsWith("f")) {
                            string idx = par[0].Replace("f", "");
                            bool complete = true;                           
                            APISearchTerm term = new APISearchTerm();
                            term.field = par[1];

                            foreach (string c in ConditionFields) {
                                string param = parameters.Where(x => x.StartsWith(c + idx)).Take(1).SingleOrDefault();
                                if (param != null)
                                {
                                    List<string> condpar = param.Split('=').ToList();

                                    switch (c) {
                                        case "n":
                                            term.not = condpar[1].ToLower()=="true" ? true: false;
                                            break;

                                        case "o":
                                            term.op = (APISearch.SearchComparer)int.Parse(condpar[1]);
                                            break;

                                        case "v":
                                            term.values = condpar[1].Split(',').ToList();
                                            break;


                                        case "t":
                                            term.type = (Domain.Shared.DataType)int.Parse(condpar[1]);
                                            break;
                                    }

                                    
                                }
                                else {
                                    complete = false;
                                    break;
                                }
                            }

                            if (complete) {
                                search.filters.Add(term);
                            }


                        }

                        break;
                }

                

            }

            return true;
        }


     
    }
}


