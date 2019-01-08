namespace Tlahui.Context.WebAPI.Migrations
{
    using CustomAttributes;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using System.Reflection;
    using Tlahui.Domain.Base.Entities;
    using Tlahui.Domain.Common.Tenant;
    using Tlahui.Domain.Infraestructure.Entities;
    using Tlahui.Domain.Store.Entities;
    
    internal sealed class Configuration : DbMigrationsConfiguration<Tlahui.Context.WebAPI.WebAPIContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }


        private void cleanDynamic(Tlahui.Context.WebAPI.WebAPIContext context) {
            context.Database.ExecuteSqlCommand("delete from [Infrastructure].[DynamicFormMetadata]", new object[0]);
            context.Database.ExecuteSqlCommand("delete from [Infrastructure].[DynamicTableMetadata]", new object [0]);
            context.Database.ExecuteSqlCommand("delete from [Infrastructure].[LocalizableResource]", new object[0]);
            context.Database.ExecuteSqlCommand("delete from [Infrastructure].[EntityQuery]", new object[0]);
        }


        protected override void Seed(Tlahui.Context.WebAPI.WebAPIContext context)
        {
            //  This method will be called after migrating to the latest version.
            cleanDynamic(context);

            LoadLocalizableResourcesFromAssemblies(context);
            LoadDynamicTableMetadataFromAssemblies(context);
            LoadDynamicFormMetadataFromAssemblies(context);

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.
            context.Buckets.AddOrUpdate(x => x.Id, new Bucket()
            {
                CreateDate = DateTime.UtcNow,
                CreatorId = "demo_user",
                Deleted = false,
                Id = "demo_bucket",
                ModifierId = "demo_user",
                Name = "Demo Bucket",
                UpdateDate = DateTime.Now,
                UserId = "demo_user"
            });

            context.Categories.AddOrUpdate(x => x.Id, new Category() {
                Id ="democategorie001", BucketId = "demo_bucket",
                Name ="Demo Categor 001", Description= "Desc Demo Categor 001",
                KeyWords="demo category",
                DisplayOrder=1, Published =true, Deleted =false , ParentCategoryId =null 
            }, new Category()
            {
                Id = "democategorie002",
                BucketId = "demo_bucket",
                Name = "Demo Categor 002",
                Description = "Desc Demo Categor 002",
                KeyWords = "demo category child",
                DisplayOrder = 1,
                Published = true,
                Deleted = false,
                ParentCategoryId = "democategorie001", 
            
            }
            );

        }



        private void LoadDynamicFormMetadataFromAssemblies(Tlahui.Context.WebAPI.WebAPIContext context)
        {

            List<Assembly> assemblies = AppDomain.CurrentDomain.GetAssemblies().ToList();

            foreach (Assembly a in assemblies)
            {
                var uidynamics = a.DefinedTypes.Where(type => type.ImplementedInterfaces.Any(inter => inter == typeof(IDynamicUIResource))).ToList();
                foreach (TypeInfo tipo in uidynamics)
                {


                    System.Type t = System.Type.GetType(tipo.AssemblyQualifiedName);
                    System.Attribute[] attrs;

                    //Check localization on type
                    attrs = System.Attribute.GetCustomAttributes(t);
                    foreach (System.Attribute attr in attrs)
                    {
                        if (attr is EntitiesUIMetadata)
                        {
                            EntitiesUIMetadata resource = (EntitiesUIMetadata)attr;
                            context.DynamicFormMetadataProperties.AddOrUpdate(x => new { x.ResourceId },
                                new DynamicFormMetadata()
                                {
                                    ResourceId = t.FullName,
                                    ResourceGroupId = t.FullName,
                                    ShortId = t.FullName.Split('.').ToList().Last(),
                                    APIDictionaryEndpoint = resource.APIDictionaryEndpoint,
                                    DictionaryKey = resource.DictionaryKey,
                                    DictionaryValue = resource.DictionaryValue,
                                    DictionaryValueIndex = resource.DictionaryValueIndex,
                                    BoolDisplayType = resource.BoolDisplayType,
                                    ActionAvailable = resource.ActionAvailable,
                                    AddActionAvailable = resource.AddActionAvailable,
                                    Col = resource.Col,
                                    ControlType = resource.ControlType,
                                    DataSourceType = resource.DataSourceType,
                                    DefaultValue = resource.DefaultValue,
                                    DeleteActionAvailable = resource.DeleteActionAvailable,
                                    Height = resource.Height,
                                    Row = resource.Row,
                                    Type = resource.Type,
                                    UpdateActionAvailable = resource.UpdateActionAvailable,
                                    Width = resource.Width,
                                    MaxLen = resource.MaxLen,
                                    MaxValue = resource.MaxValue,
                                    MinLen = resource.MinLen,
                                    MinValue = resource.MinValue,
                                    Required = resource.Required
                                    
                                });
                        }

                    }


                    //Check localization within interfaces
                    var interfaces = t.GetInterfaces().ToList();
                    foreach (Type ifc in interfaces)
                    {
                        var iprops = ifc.GetProperties().ToList();

                        foreach (PropertyInfo pi in iprops)
                        {
                            attrs = System.Attribute.GetCustomAttributes(pi);
                            foreach (System.Attribute attr in attrs)
                            {
                                if (attr is EntitiesUIMetadata)
                                {
                                    EntitiesUIMetadata resource = (EntitiesUIMetadata)attr;
                                    context.DynamicFormMetadataProperties.AddOrUpdate(x => new { x.ResourceId },
                                        new DynamicFormMetadata()
                                        {
                                            ResourceId = t.FullName + "." + pi.Name,
                                            ResourceGroupId = t.FullName,
                                            ShortId = pi.Name,
                                            APIDictionaryEndpoint = resource.APIDictionaryEndpoint,
                                            DictionaryKey = resource.DictionaryKey,
                                            DictionaryValue = resource.DictionaryValue,
                                            DictionaryValueIndex = resource.DictionaryValueIndex,
                                            BoolDisplayType = resource.BoolDisplayType,
                                            ActionAvailable = resource.ActionAvailable,
                                            AddActionAvailable = resource.AddActionAvailable,
                                            Col = resource.Col,
                                            ControlType = resource.ControlType,
                                            DataSourceType = resource.DataSourceType,
                                            DefaultValue = resource.DefaultValue,
                                            DeleteActionAvailable = resource.DeleteActionAvailable,
                                            Height = resource.Height,
                                            Row = resource.Row,
                                            Type = resource.Type,
                                            UpdateActionAvailable = resource.UpdateActionAvailable,
                                            Width = resource.Width,
                                            MaxLen = resource.MaxLen,
                                            MaxValue = resource.MaxValue,
                                            MinLen = resource.MinLen,
                                            MinValue = resource.MinValue,
                                            Required = resource.Required
                                        });
                                }

                            }
                        }
                    }




                    //Check localization within properties
                    var props = t.GetProperties().ToList();
                    foreach (PropertyInfo pi in props)
                    {
                        attrs = System.Attribute.GetCustomAttributes(pi);
                        foreach (System.Attribute attr in attrs)
                        {
                            if (attr is EntitiesUIMetadata)
                            {
                                EntitiesUIMetadata resource = (EntitiesUIMetadata)attr;
                                context.DynamicFormMetadataProperties.AddOrUpdate(x => new { x.ResourceId },
                                    new DynamicFormMetadata()
                                    {
                                        ResourceId = t.FullName + "." + pi.Name,
                                        ResourceGroupId = t.FullName,
                                        ShortId = pi.Name,
                                        APIDictionaryEndpoint = resource.APIDictionaryEndpoint,
                                        DictionaryKey = resource.DictionaryKey,
                                        DictionaryValue = resource.DictionaryValue,
                                        DictionaryValueIndex = resource.DictionaryValueIndex,
                                        BoolDisplayType = resource.BoolDisplayType,
                                        ActionAvailable = resource.ActionAvailable,
                                        AddActionAvailable = resource.AddActionAvailable,
                                        Col = resource.Col,
                                        ControlType = resource.ControlType,
                                        DataSourceType = resource.DataSourceType,
                                        DefaultValue = resource.DefaultValue,
                                        DeleteActionAvailable = resource.DeleteActionAvailable,
                                        Height = resource.Height,
                                        Row = resource.Row,
                                        Type = resource.Type,
                                        UpdateActionAvailable = resource.UpdateActionAvailable,
                                        Width = resource.Width,
                                        MaxLen = resource.MaxLen,
                                        MaxValue = resource.MaxValue,
                                        MinLen = resource.MinLen,
                                        MinValue = resource.MinValue,
                                        Required = resource.Required
                                    });
                            }

                        }
                    }


                }
            }
        }


        private void LoadDynamicTableMetadataFromAssemblies(Tlahui.Context.WebAPI.WebAPIContext context)
        {

            List<Assembly> assemblies = AppDomain.CurrentDomain.GetAssemblies().ToList();

            foreach (Assembly a in assemblies)
            {
                var uidynamics = a.DefinedTypes.Where(type => type.ImplementedInterfaces.Any(inter => inter == typeof(IDynamicUIResource))).ToList();
                foreach (TypeInfo tipo in uidynamics)
                {


                    System.Type t = System.Type.GetType(tipo.AssemblyQualifiedName);
                    System.Attribute[] attrs;

                    try
                    {
                        //Intenta extraer el query de la clase existe la constante QUERY 
                        string query = (string)t.GetField("QUERY").GetValue(null);
                        context.EntityQueries.AddOrUpdate(x => x.ResourceId, new EntityQuery()
                        {
                            Query = query,
                            ResourceId = t.FullName
                        });
                    }
                    catch (Exception ex)
                    {
 
                    }
              
                     

                    //Check localization on type
                    attrs = System.Attribute.GetCustomAttributes(t);
                    foreach (System.Attribute attr in attrs)
                    {
                        if (attr is EntitiesUIMetadata)
                        {
                            EntitiesUIMetadata resource = (EntitiesUIMetadata)attr;
                            context.DynamicTableMetadataProperties.AddOrUpdate(x => new { x.ResourceId },
                                new DynamicTableMetadata()
                                {
                                    DisplayByDefault = resource.DisplayByDefault,
                                    AlwaysHidden = resource.AlwaysHidden,
                                    DisplayIndex = resource.DisplayIndex,
                                    IsID = resource.IsID,
                                    OutpuFormat = resource.OutpuFormat,
                                    Searchable = resource.Searchable,
                                    Type = resource.Type.GetHashCode(),
                                    ResourceId = t.FullName,
                                    ResourceGroupId = t.FullName,
                                    ShortId = t.FullName.Split('.').ToList().Last(),
                                    APIDictionaryEndpoint = resource.APIDictionaryEndpoint,
                                    DeafultSort = resource.DefaultSort,
                                    DictionaryKey = resource.DictionaryKey,
                                    DictionaryValue = resource.DictionaryValue,
                                    DictionaryValueIndex = resource.DictionaryValueIndex,
                                    BoolDisplayType = resource.BoolDisplayType,
                                     MarkDeletedField = resource.MarkDeletedField


                                });
                        }

                    }


                    //Check localization within interfaces
                    var interfaces = t.GetInterfaces().ToList();
                    foreach (Type ifc in interfaces)
                    {
                        var iprops = ifc.GetProperties().ToList();

                        foreach (PropertyInfo pi in iprops)
                        {
                            attrs = System.Attribute.GetCustomAttributes(pi);
                            foreach (System.Attribute attr in attrs)
                            {
                                if (attr is EntitiesUIMetadata)
                                {
                                    EntitiesUIMetadata resource = (EntitiesUIMetadata)attr;
                                    context.DynamicTableMetadataProperties.AddOrUpdate(x => new { x.ResourceId },
                                        new DynamicTableMetadata()
                                        {
                                            DisplayByDefault = resource.DisplayByDefault,
                                            AlwaysHidden = resource.AlwaysHidden,
                                            DisplayIndex = resource.DisplayIndex,
                                            IsID = resource.IsID,
                                            OutpuFormat = resource.OutpuFormat,
                                            Searchable = resource.Searchable,
                                            Type = resource.Type.GetHashCode(),
                                            ResourceId = t.FullName + "." + pi.Name,
                                            ResourceGroupId = t.FullName,
                                            ShortId = pi.Name,
                                            APIDictionaryEndpoint = resource.APIDictionaryEndpoint,
                                            DeafultSort = resource.DefaultSort,
                                            DictionaryKey = resource.DictionaryKey,
                                            DictionaryValue = resource.DictionaryValue,
                                            DictionaryValueIndex = resource.DictionaryValueIndex,
                                            BoolDisplayType = resource.BoolDisplayType,
                                            MarkDeletedField = resource.MarkDeletedField

                                        });
                                }

                            }
                        }
                    }




                    //Check localization within properties
                    var props = t.GetProperties().ToList();
                    foreach (PropertyInfo pi in props)
                    {
                        attrs = System.Attribute.GetCustomAttributes(pi);
                        foreach (System.Attribute attr in attrs)
                        {
                            if (attr is EntitiesUIMetadata)
                            {
                                EntitiesUIMetadata resource = (EntitiesUIMetadata)attr;
                                context.DynamicTableMetadataProperties.AddOrUpdate(x => new { x.ResourceId },
                                    new DynamicTableMetadata()
                                    {
                                        DisplayByDefault = resource.DisplayByDefault,
                                        AlwaysHidden = resource.AlwaysHidden,
                                        DisplayIndex = resource.DisplayIndex,
                                        IsID = resource.IsID,
                                        OutpuFormat = resource.OutpuFormat,
                                        Searchable = resource.Searchable,
                                        Type = resource.Type.GetHashCode(),
                                        ResourceId = t.FullName + "." + pi.Name,
                                        ResourceGroupId = t.FullName,
                                        ShortId = pi.Name,
                                        APIDictionaryEndpoint = resource.APIDictionaryEndpoint,
                                        DeafultSort = resource.DefaultSort,
                                        DictionaryKey = resource.DictionaryKey,
                                        DictionaryValue = resource.DictionaryValue,
                                        DictionaryValueIndex = resource.DictionaryValueIndex,
                                        BoolDisplayType = resource.BoolDisplayType,
                                        MarkDeletedField = resource.MarkDeletedField
                                    });
                            }

                        }
                    }


                }
            }
        }


        private void LoadLocalizableResourcesFromAssemblies(Tlahui.Context.WebAPI.WebAPIContext context) {
            
           List<Assembly> assemblies= AppDomain.CurrentDomain.GetAssemblies().ToList();

            foreach (Assembly a in assemblies) {
                var localizables = a.DefinedTypes.Where(type => type.ImplementedInterfaces.Any(inter => inter == typeof(ILocalizableResource))).ToList();
                foreach (TypeInfo tipo in localizables) {

                   


                    System.Type t = System.Type.GetType(tipo.AssemblyQualifiedName);
                    System.Attribute[] attrs;
                    

                    //Check localization on type
                    attrs = System.Attribute.GetCustomAttributes(t);
                    foreach (System.Attribute attr in attrs)
                    {
                        if (attr is Localizable)
                        {
                            Localizable resource = (Localizable)attr;
                            context.LocalizableResources.AddOrUpdate(x => new { x.ResourceId, x.Culture, x.Language },
                                new LocalizableResource()
                                {
                                    Context = resource.Context,
                                    Culture = resource.Culture,
                                    Language = resource.Language,
                                    Plural = resource.Plural,
                                    Traslation = resource.Traslation,
                                    TraslationId = resource.TraslationId,
                                    ResourceId = t.FullName,
                                    ResourceGroupId= t.FullName,
                                    ShortId = t.FullName.Split('.').ToList().Last()
                                });
                        }

                    }


                    //Check localization within interfaces
                    var interfaces = t.GetInterfaces().ToList();
                    foreach(Type ifc  in interfaces ){
                        var iprops = ifc.GetProperties().ToList();

                        foreach (PropertyInfo pi in iprops)
                        {
                            attrs = System.Attribute.GetCustomAttributes(pi);
                            foreach (System.Attribute attr in attrs)
                            {
                                if (attr is Localizable)
                                {
                                    Localizable resource = (Localizable)attr;
                                    context.LocalizableResources.AddOrUpdate(x => new { x.ResourceId, x.Culture, x.Language },
                                        new LocalizableResource()
                                        {
                                            Context = resource.Context,
                                            Culture = resource.Culture,
                                            Language = resource.Language,
                                            Plural = resource.Plural,
                                            Traslation = resource.Traslation,
                                            TraslationId = resource.TraslationId,
                                            ResourceId = t.FullName + "." + pi.Name,
                                            ResourceGroupId = t.FullName,
                                            ShortId = pi.Name
                                        });
                                }

                            }
                        }
                    }

                  


                    //Check localization within properties
                    var props=t.GetProperties().ToList();
                    foreach (PropertyInfo pi in props) {
                        attrs = System.Attribute.GetCustomAttributes(pi);
                        foreach (System.Attribute attr in attrs)
                        {
                            if (attr is Localizable)
                            {
                                Localizable resource = (Localizable)attr;
                                context.LocalizableResources.AddOrUpdate(x => new { x.ResourceId, x.Culture, x.Language },
                                    new LocalizableResource()
                                    {
                                        Context = resource.Context,
                                        Culture = resource.Culture,
                                        Language = resource.Language,
                                        Plural = resource.Plural,
                                        Traslation = resource.Traslation,
                                        TraslationId = resource.TraslationId,
                                        ResourceId = t.FullName + "." + pi.Name,
                                        ResourceGroupId = t.FullName,
                                        ShortId = pi.Name
                                    });
                            }

                        }
                    }

              
                }
            }
        }


    }
}
