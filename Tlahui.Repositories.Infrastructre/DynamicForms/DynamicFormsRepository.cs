using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DynamicForms.Entities;
using Tlahui.Domain.Common.Entities;
using Tlahui.Domain.Infraestructure.Entities;

namespace Tlahui.Repositories.Infrastructure.DynamicForms
{
    public class DynamicFormsRepository : IDynamicFormsRepository
    {

        private const string DEFAULT_LANG = "es";
        private const string DEFAULT_LOCALE = "MX";

        private Tlahui.Context.WebAPI.WebAPIContext context;
        public DynamicFormsRepository(DbContext Db)
        {
            this.context = (Tlahui.Context.WebAPI.WebAPIContext)Db;
        }

        public Task<UITable> GetTableMetadata(string ResourceId, string Language, string Locale)
        {
            UITable table = new UITable();
            List<LocalizableResource> list = 
            context.LocalizableResources.Where(x => x.ResourceGroupId == ResourceId &&
            x.Language == Language && x.Culture == Locale).ToList();

            List<DynamicTableMetadata> metadata = context.DynamicTableMetadataProperties.Where(x => x.ResourceGroupId == ResourceId).ToList();


            if (list.Count == 0) {
                list = context.LocalizableResources.Where(x => x.ResourceGroupId == ResourceId &&
                x.Language == DEFAULT_LANG && x.Culture == DEFAULT_LOCALE).ToList();
            }

            foreach (LocalizableResource r in list) {
                if (r.ResourceId == r.ResourceGroupId)
                {
                    table.Traslation = r.Traslation;
                    table.Plural = r.Plural;
                    table.Language = r.Language;
                    table.Culture = r.Culture;
                    table.ShortId = r.ShortId;
                    table.ResourceId = r.ResourceId;
                    
                }
                else {
                    UIColumn col = new UIColumn();
                    col.Traslation = r.Traslation;
                    col.Plural = r.Plural;
                    col.Language = r.Language;
                    col.Culture = r.Culture;
                    col.ShortId = r.ShortId;
                    col.ResourceId = r.ResourceId;

                    DynamicTableMetadata m = metadata.Where(x => x.ResourceId == r.ResourceId).Take(1).SingleOrDefault();
                    if (m != null)
                    {
                        col.AlwaysHidden = m.AlwaysHidden;
                        col.DisplayByDefault=m.DisplayByDefault ;
                        col.DisplayIndex=m.DisplayIndex ;
                        col.IsID=m.IsID ;
                        col.OutpuFormat=m.OutpuFormat ;
                        col.Searchable = m.Searchable ;
                        col.Type = (Domain.Shared.DataType)m.Type;
                        col.DefaultSort = m.DeafultSort;
                        col.DictionaryKey = m.DictionaryKey;
                        col.DictionaryValue = m.DictionaryValue;
                        col.DictionaryValueIndex = m.DictionaryValueIndex;
                        col.APIDictionaryEndpoint = m.APIDictionaryEndpoint;
                        col.BoolDisplayType = m.BoolDisplayType;
                        col.MarkDeletedField = m.MarkDeletedField;

                    }
                    else {
                        col.AlwaysHidden = true;
                        col.DisplayByDefault = false;
                        col.DisplayIndex = 0;
                        col.IsID = false;
                        col.OutpuFormat = "";
                        col.Searchable = false;
                        col.Type = Domain.Shared.DataType.text;
                        col.DefaultSort = false;
                        col.DictionaryKey = false;
                        col.DictionaryValue = false;
                        col.DictionaryValueIndex = 0;
                        col.APIDictionaryEndpoint = "";
                        col.BoolDisplayType = Domain.Shared.BooleanDisplayType.none;
                        col.MarkDeletedField =  false;
                    }


                    table.Columns.Add(col);
                }

                
            }
            

            return Task.FromResult(table);

        }


        private UIForm DemoForm() {

            UIFormField field;
            UIForm Form = new UIForm
            {
                Traslation = "Objeto demo",
                Plural = "Objetos demo",
                Language = "ES",
                Culture = "MX",
                ShortId = "demo",
                ResourceId = "demo"
            };


            //--------------  Listas
            field = new UIFormField
            {
                Traslation = "Campo Lista Fija",
                Plural = "Campos Lista Fija",
                Language = Form.Language,
                Culture = Form.Culture,
                ShortId = "dropdownfijo",
                ResourceId = Form.ShortId + ".dropdownfijo",
                ActionAvailable = true,
                AddActionAvailable = true,
                APIDictionaryEndpoint = "",
                BoolDisplayType = Domain.Shared.BooleanDisplayType.none,
                Col = 1,
                ControlType = Domain.Shared.ControlType.select_fixed_elements,
                DataSourceType = Domain.Shared.DataSourceType.user_input,
                DefaultValue = "",
                DeleteActionAvailable = true,
                DictionaryKey = false,
                DictionaryValue = false,
                DictionaryValueIndex = 0,
                Height = "25",
                Row = 0,
                Type = Domain.Shared.DataType.text,
                UpdateActionAvailable = true,
                Width = "33",
                MaxLen = 0,
                MinLen = 0,
                MaxValue = "",
                MinValue = "",
                Required = true,
                Pairs = new List<APIKeyValuePair>() { new APIKeyValuePair() { Key ="1", Value="Valor uno" },
                new APIKeyValuePair() { Key ="2", Value="Valor dos" }, new APIKeyValuePair() { Key ="3", Value="Valor tres" }}

            };
            Form.FormFields.Add(field);


            field = new UIFormField
            {
                Traslation = "Campo Lista API",
                Plural = "Campos Lista API",
                Language = Form.Language,
                Culture = Form.Culture,
                ShortId = "dropdownarest",
                ResourceId = Form.ShortId + ".dropdownarest",
                ActionAvailable = true,
                AddActionAvailable = true,
                APIDictionaryEndpoint = "store/categories/catalog?filter=",
                BoolDisplayType = Domain.Shared.BooleanDisplayType.none,
                Col = 2,
                ControlType = Domain.Shared.ControlType.select_localapi_elements,
                DataSourceType = Domain.Shared.DataSourceType.user_input,
                DefaultValue = "",
                DeleteActionAvailable = true,
                DictionaryKey = false,
                DictionaryValue = false,
                DictionaryValueIndex = 0,
                Height = "25",
                Row = 0,
                Type = Domain.Shared.DataType.text,
                UpdateActionAvailable = true,
                Width = "33",
                MaxLen = 0,
                MinLen = 0,
                MaxValue = "",
                MinValue = "",
                Required = true,
                Pairs = null

            };
            Form.FormFields.Add(field);


            field = new UIFormField
            {
                Traslation = "Campo Lista COMPLETE API",
                Plural = "Campos Lista COMPLETE API",
                Language = Form.Language,
                Culture = Form.Culture,
                ShortId = "dropdownarestac",
                ResourceId = Form.ShortId + ".dropdownarestac",
                ActionAvailable = true,
                AddActionAvailable = true,
                APIDictionaryEndpoint = "store/categories/catalog?filter=",
                BoolDisplayType = Domain.Shared.BooleanDisplayType.none,
                Col = 3,
                ControlType = Domain.Shared.ControlType.select_localapi_autocomplete_elements,
                DataSourceType = Domain.Shared.DataSourceType.user_input,
                DefaultValue = "",
                DeleteActionAvailable = true,
                DictionaryKey = false,
                DictionaryValue = false,
                DictionaryValueIndex = 0,
                Height = "25",
                Row = 0,
                Type = Domain.Shared.DataType.text,
                UpdateActionAvailable = true,
                Width = "33",
                MaxLen = 0,
                MinLen = 0,
                MaxValue = "",
                MinValue = "",
                Required = true,
                Pairs = null

            };
            Form.FormFields.Add(field);

            ////--------------  Date Time Pickers

            //field = new UIFormField
            //{
            //    Traslation = "Campo Datepicker",
            //    Plural = "Campos Datepicker",
            //    Language = Form.Language,
            //    Culture = Form.Culture,
            //    ShortId = "datepiker",
            //    ResourceId = Form.ShortId + ".datepiker",
            //    ActionAvailable = true,
            //    AddActionAvailable = true,
            //    APIDictionaryEndpoint = "",
            //    BoolDisplayType = Domain.Shared.BooleanDisplayType.none,
            //    Col = 2,
            //    ControlType = Domain.Shared.ControlType.datepicker,
            //    DataSourceType = Domain.Shared.DataSourceType.user_input,
            //    DefaultValue = "",
            //    DeleteActionAvailable = true,
            //    DictionaryKey = false,
            //    DictionaryValue = false,
            //    DictionaryValueIndex = 0,
            //    Height = "25",
            //    Row = 1,
            //    Type = Domain.Shared.DataType.date,
            //    UpdateActionAvailable = true,
            //    Width = "50",
            //    MaxLen = 50,
            //    MinLen = 3,
            //    MaxValue = "2020-12-31T23:59:59",
            //    MinValue = "1925-01-01T00:00:00",
            //    Required = true
            //};
            //Form.FormFields.Add(field);


            //field = new UIFormField
            //{
            //    Traslation = "Campo DateTimepicker",
            //    Plural = "Campos DateTimepicker",
            //    Language = Form.Language,
            //    Culture = Form.Culture,
            //    ShortId = "datetimepiker",
            //    ResourceId = Form.ShortId + ".datetimepiker",
            //    ActionAvailable = true,
            //    AddActionAvailable = true,
            //    APIDictionaryEndpoint = "",
            //    BoolDisplayType = Domain.Shared.BooleanDisplayType.none,
            //    Col = 1,
            //    ControlType = Domain.Shared.ControlType.datetimepicker,
            //    DataSourceType = Domain.Shared.DataSourceType.user_input,
            //    DefaultValue = "",
            //    DeleteActionAvailable = true,
            //    DictionaryKey = false,
            //    DictionaryValue = false,
            //    DictionaryValueIndex = 0,
            //    Height = "25",
            //    Row = 1,
            //    Type = Domain.Shared.DataType.datetime,
            //    UpdateActionAvailable = true,
            //    Width = "50",
            //    MaxLen = 50,
            //    MinLen = 3,
            //    MaxValue = "2020-12-31T23:59:59",
            //    MinValue = "1925-01-01T00:00:00",
            //    Required = false
            //};
            //Form.FormFields.Add(field);


            //field = new UIFormField
            //{
            //    Traslation = "Campo Timepicker",
            //    Plural = "Campos Timepicker",
            //    Language = Form.Language,
            //    Culture = Form.Culture,
            //    ShortId = "timepiker",
            //    ResourceId = Form.ShortId + ".timepiker",
            //    ActionAvailable = true,
            //    AddActionAvailable = true,
            //    APIDictionaryEndpoint = "",
            //    BoolDisplayType = Domain.Shared.BooleanDisplayType.none,
            //    Col = 1,
            //    ControlType = Domain.Shared.ControlType.timepicker,
            //    DataSourceType = Domain.Shared.DataSourceType.user_input,
            //    DefaultValue = "",
            //    DeleteActionAvailable = true,
            //    DictionaryKey = false,
            //    DictionaryValue = false,
            //    DictionaryValueIndex = 0,
            //    Height = "25",
            //    Row = 2,
            //    Type = Domain.Shared.DataType.time,
            //    UpdateActionAvailable = true,
            //    Width = "100",
            //    MaxLen = 50,
            //    MinLen = 3,
            //    MaxValue = "18:00",
            //    MinValue = "09:00",
            //    Required = false
            //};
            //Form.FormFields.Add(field);


            //////--------------  Checkbox

            ////field = new UIFormField
            ////{
            ////    Traslation = "Campo Checkbox",
            ////    Plural = "Campos Checkbox",
            ////    Language = Form.Language,
            ////    Culture = Form.Culture,
            ////    ShortId = "checkbox",
            ////    ResourceId = Form.ShortId + ".checkbox",
            ////    ActionAvailable = true,
            ////    AddActionAvailable = true,
            ////    APIDictionaryEndpoint = "",
            ////    BoolDisplayType = Domain.Shared.BooleanDisplayType.none,
            ////    Col = 1,
            ////    ControlType = Domain.Shared.ControlType.checkbox,
            ////    DataSourceType = Domain.Shared.DataSourceType.user_input,
            ////    DefaultValue = "",
            ////    DeleteActionAvailable = true,
            ////    DictionaryKey = false,
            ////    DictionaryValue = false,
            ////    DictionaryValueIndex = 0,
            ////    Height = "25",
            ////    Row = 1,
            ////    Type = Domain.Shared.DataType.boolean,
            ////    UpdateActionAvailable = true,
            ////    Width = "50",
            ////    MaxLen = 50,
            ////    MinLen = 3,
            ////    MaxValue = "",
            ////    MinValue = "",
            ////    Required = true
            ////};
            ////Form.FormFields.Add(field);


            ////----------------- Campos de texto

            //field = new UIFormField
            //{
            //    Traslation = "Campo texto-texto",
            //    Plural = "Campos texto-texto",
            //    Language = Form.Language,
            //    Culture = Form.Culture,
            //    ShortId = "texto-texto",
            //    ResourceId = Form.ShortId + ".texto-texto",
            //    ActionAvailable = true,
            //    AddActionAvailable = true,
            //    APIDictionaryEndpoint = "",
            //    BoolDisplayType = Domain.Shared.BooleanDisplayType.none,
            //    ControlType = Domain.Shared.ControlType.inputbox,
            //    DataSourceType = Domain.Shared.DataSourceType.user_input,
            //    DefaultValue = "",
            //    DeleteActionAvailable = true,
            //    DictionaryKey = false,
            //    DictionaryValue = false,
            //    DictionaryValueIndex = 0,
            //    Height = "25",
            //    Row = 3,
            //    Col = 1,
            //    Type = Domain.Shared.DataType.text,
            //    UpdateActionAvailable = true,
            //    Width = "100",
            //    MaxLen = 50,
            //    MinLen = 3,
            //    MaxValue = "",
            //    MinValue = "",
            //    Required = true
            //};
            //Form.FormFields.Add(field);


            //field = new UIFormField
            //{
            //    Traslation = "Campo texto-entero",
            //    Plural = "Campos texto-entero",
            //    Language = Form.Language,
            //    Culture = Form.Culture,
            //    ShortId = "texto-entero",
            //    ResourceId = Form.ShortId + ".texto-entero",
            //    ActionAvailable = true,
            //    AddActionAvailable = true,
            //    APIDictionaryEndpoint = "",
            //    BoolDisplayType = Domain.Shared.BooleanDisplayType.none,
            //    ControlType = Domain.Shared.ControlType.inputbox,
            //    DataSourceType = Domain.Shared.DataSourceType.user_input,
            //    DefaultValue = "",
            //    DeleteActionAvailable = true,
            //    DictionaryKey = false,
            //    DictionaryValue = false,
            //    DictionaryValueIndex = 0,
            //    Height = "25",
            //    Row = 4,
            //    Col = 1,
            //    Type = Domain.Shared.DataType.integer_number,
            //    UpdateActionAvailable = true,
            //    Width = "50",
            //    MaxLen = 0,
            //    MinLen = 0,
            //    MaxValue = "100",
            //    MinValue = "0",
            //    Required = true
            //};
            //Form.FormFields.Add(field);


            //field = new UIFormField
            //{
            //    Traslation = "Campo texto-decimal",
            //    Plural = "Campos texto-decimal",
            //    Language = Form.Language,
            //    Culture = Form.Culture,
            //    ShortId = "texto-decimal",
            //    ResourceId = Form.ShortId + ".texto-decimal",
            //    ActionAvailable = true,
            //    AddActionAvailable = true,
            //    APIDictionaryEndpoint = "",
            //    BoolDisplayType = Domain.Shared.BooleanDisplayType.none,
            //    ControlType = Domain.Shared.ControlType.inputbox,
            //    DataSourceType = Domain.Shared.DataSourceType.user_input,
            //    DefaultValue = "",
            //    DeleteActionAvailable = true,
            //    DictionaryKey = false,
            //    DictionaryValue = false,
            //    DictionaryValueIndex = 0,
            //    Height = "25",
            //    Row = 4,
            //    Col = 2,
            //    Type = Domain.Shared.DataType.decimal_number,
            //    UpdateActionAvailable = true,
            //    Width = "50",
            //    MaxLen = 0,
            //    MinLen = 0,
            //    MaxValue = "10",
            //    MinValue = "-10",
            //    Required = true
            //};
            //Form.FormFields.Add(field);

            //field = new UIFormField
            //{
            //    Traslation = "Campo texto-fecha",
            //    Plural = "Campos texto-fecha",
            //    Language = Form.Language,
            //    Culture = Form.Culture,
            //    ShortId = "texto-fecha",
            //    ResourceId = Form.ShortId + ".texto-fecha",
            //    ActionAvailable = true,
            //    AddActionAvailable = true,
            //    APIDictionaryEndpoint = "",
            //    BoolDisplayType = Domain.Shared.BooleanDisplayType.none,
            //     ControlType = Domain.Shared.ControlType.inputbox,
            //    DataSourceType = Domain.Shared.DataSourceType.user_input,
            //    DefaultValue = "",
            //    DeleteActionAvailable = true,
            //    DictionaryKey = false,
            //    DictionaryValue = false,
            //    DictionaryValueIndex = 0,
            //    Height = "25",
            //    Row = 5,
            //    Col = 1,
            //    Type = Domain.Shared.DataType.date,
            //    UpdateActionAvailable = true,
            //    Width = "33",
            //    MaxLen = 0,
            //    MinLen = 0,
            //    MaxValue = "2020-01-01T00:00:00",
            //    MinValue = "2000-01-01T00:00:00",
            //    Required = true
            //};
            //Form.FormFields.Add(field);

            //field = new UIFormField
            //{
            //    Traslation = "Campo texto-fecha-hora",
            //    Plural = "Campos texto-fecha-hora",
            //    Language = Form.Language,
            //    Culture = Form.Culture,
            //    ShortId = "texto-fecha-hora",
            //    ResourceId = Form.ShortId + ".texto-fecha-hora",
            //    ActionAvailable = true,
            //    AddActionAvailable = true,
            //    APIDictionaryEndpoint = "",
            //    BoolDisplayType = Domain.Shared.BooleanDisplayType.none,
            //    ControlType = Domain.Shared.ControlType.inputbox,
            //    DataSourceType = Domain.Shared.DataSourceType.user_input,
            //    DefaultValue = "",
            //    DeleteActionAvailable = true,
            //    DictionaryKey = false,
            //    DictionaryValue = false,
            //    DictionaryValueIndex = 0,
            //    Height = "25",
            //    Row = 5,
            //    Col = 2,
            //    Type = Domain.Shared.DataType.datetime,
            //    UpdateActionAvailable = true,
            //    Width = "33",
            //    MaxLen = 0,
            //    MinLen = 0,
            //    MaxValue = "2020-01-01T00:00:00",
            //    MinValue = "1971-01-31T00:00:00",
            //    Required = false
            //};
            //Form.FormFields.Add(field);

            //field = new UIFormField
            //{
            //    Traslation = "Campo texto-hora",
            //    Plural = "Campos texto-hora",
            //    Language = Form.Language,
            //    Culture = Form.Culture,
            //    ShortId = "texto-hora",
            //    ResourceId = Form.ShortId + ".texto-hora",
            //    ActionAvailable = true,
            //    AddActionAvailable = true,
            //    APIDictionaryEndpoint = "",
            //    BoolDisplayType = Domain.Shared.BooleanDisplayType.none,
            //    ControlType = Domain.Shared.ControlType.inputbox,
            //    DataSourceType = Domain.Shared.DataSourceType.user_input,
            //    DefaultValue = "",
            //    DeleteActionAvailable = true,
            //    DictionaryKey = false,
            //    DictionaryValue = false,
            //    DictionaryValueIndex = 0,
            //    Height = "25",
            //    Row = 5,
            //    Col = 3,
            //    Type = Domain.Shared.DataType.time,
            //    UpdateActionAvailable = true,
            //    Width = "33",
            //    MaxLen = 0,
            //    MinLen = 0,
            //    MaxValue = "18:00",
            //    MinValue = "09:00",
            //    Required = true
            //};
            //Form.FormFields.Add(field);



            return Form;

        }


        public Task<UIForm> GetFormMetadata(string ResourceId, string Language, string Locale)
        {
            return Task.FromResult(DemoForm());

            UIForm Form = new UIForm();
            List<LocalizableResource> list =
            context.LocalizableResources.Where(x => x.ResourceGroupId == ResourceId &&
            x.Language == Language && x.Culture == Locale).ToList();

            List<DynamicFormMetadata> metadata = context.DynamicFormMetadataProperties.Where(x => x.ResourceGroupId == ResourceId).ToList();


            if (list.Count == 0)
            {
                list = context.LocalizableResources.Where(x => x.ResourceGroupId == ResourceId &&
                x.Language == DEFAULT_LANG && x.Culture == DEFAULT_LOCALE).ToList();
            }

            foreach (LocalizableResource r in list)
            {
                if (r.ResourceId == r.ResourceGroupId)
                {
                    Form.Traslation = r.Traslation;
                    Form.Plural = r.Plural;
                    Form.Language = r.Language;
                    Form.Culture = r.Culture;
                    Form.ShortId = r.ShortId;
                    Form.ResourceId = r.ResourceId;

                }
                else
                {
                    UIFormField field = new UIFormField();
                    field.Traslation = r.Traslation;
                    field.Plural = r.Plural;
                    field.Language = r.Language;
                    field.Culture = r.Culture;
                    field.ShortId = r.ShortId;
                    field.ResourceId = r.ResourceId;

                    DynamicFormMetadata m = metadata.Where(x => x.ResourceId == r.ResourceId).Take(1).SingleOrDefault();
                    if (m != null)
                    {
                        field.ActionAvailable = m.ActionAvailable;
                        field.AddActionAvailable = m.AddActionAvailable;
                        field.APIDictionaryEndpoint = m.APIDictionaryEndpoint;
                        field.BoolDisplayType = m.BoolDisplayType;
                        field.Col = m.Col;
                        field.ControlType = m.ControlType;
                        field.DataSourceType = m.DataSourceType;
                        field.DefaultValue = m.DefaultValue;
                        field.DeleteActionAvailable = m.DeleteActionAvailable;
                        field.DictionaryKey = m.DictionaryKey;
                        field.DictionaryValue = m.DictionaryValue ;
                        field.DictionaryValueIndex = m.DictionaryValueIndex;
                        field.Height = m.Height;
                        field.Row = m.Row ;
                        field.Type = m.Type;
                        field.UpdateActionAvailable = m.UpdateActionAvailable ;
                        field.Width = m.Width;
                        field.MaxLen = m.MaxLen;
                        field.MinLen = m.MinLen;
                        field.MaxValue = m.MaxValue;
                        field.MinValue = m.MinValue;
                       }
                    else
                    {
                        field.ActionAvailable = false;
                        field.AddActionAvailable = false;
                        field.APIDictionaryEndpoint = "";
                        field.BoolDisplayType = Domain.Shared.BooleanDisplayType.none;
                        field.Col = 0;
                        field.ControlType = Domain.Shared.ControlType.none;
                        field.DataSourceType = Domain.Shared.DataSourceType.none;
                        field.DefaultValue = "";
                        field.DeleteActionAvailable = false;
                        field.DictionaryKey = false;
                        field.DictionaryValue = false;
                        field.DictionaryValueIndex = 0;
                        field.Height = "";
                        field.Row = 0;
                        field.Type = Domain.Shared.DataType.text;
                        field.UpdateActionAvailable = false;
                        field.Width = "";
                        field.MaxLen = 0;
                        field.MinLen = 0;
                        field.MaxValue = "";
                        field.MinValue = "";
                    }


                    Form.FormFields.Add(field);
                }


            }


            return Task.FromResult(Form);

        }
    }
}
