using Sitecore.ContentSearch;
using Sitecore.Data;
using Sitecore.Data.Items;
using Sitecore.Diagnostics;
using Sitecore.ListManagement.Configuration;
using Sitecore.ListManagement.ContentSearch;
using Sitecore.ListManagement.ContentSearch.Model;
using Sitecore.ListManagement.ContentSearch.Pipelines;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Sitecore.Support.ListManagement.ContentSearch.Pipelines.GetAllLists
{
  public class GetAllContactLists : ListProcessor
  {
    private readonly ISearchIndex index;
    public GetAllContactLists()
    {
      this.index = ContentSearchManager.GetIndex(ListManagementSettings.ContactListIndexName);
    }

    public virtual void Process(ListsArgs args)
    {
      Assert.ArgumentNotNull(args, "args");
      ID itemId = (args.RootId != (ID)null) ? args.RootId : base.RootId;
      Item item = base.Database.GetItem(itemId);
      if (item == null)
      {
        args.ResultLists = Enumerable.Empty<ContactList>().AsQueryable<ContactList>();
      }
      else
      {
        ID[] templateIds = new ID[] { base.TemplateId, base.SegmentedListTemplateId };
        args.ResultLists = new QueryableProxy<ContactList>(new ContactListQueryProvider(this.index, item.ID, item.ID, templateIds, args.IncludeSubFolders));
      }
    }

  }
}