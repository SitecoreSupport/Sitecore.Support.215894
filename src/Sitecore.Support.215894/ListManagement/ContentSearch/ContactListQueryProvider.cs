using Sitecore.ContentSearch;
using Sitecore.Data;
using Sitecore.ListManagement.Configuration;
using Sitecore.ListManagement.ContentSearch.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Sitecore.Support.ListManagement.ContentSearch
{
  public class ContactListQueryProvider: Sitecore.ListManagement.ContentSearch.ContactListQueryProvider
  {
    private readonly Database listDatabase = Sitecore.Configuration.Factory.GetDatabase(ListManagementSettings.Database);

    public ContactListQueryProvider(ISearchIndex index, ID repositoryId, ID parentId, IEnumerable<ID> templateIds, bool includeSubFolders) : base(index,repositoryId,parentId,templateIds,includeSubFolders)
    {
      /*Assert.ArgumentNotNull(templateIds, "templateIds");
      Assert.ArgumentNotNull(repositoryId, "repositoryId");
      this.repositoryId = repositoryId;
      this.templateIds = templateIds;
      this.IncludeSubFolders = includeSubFolders;
      this.parentId = parentId;*/
    }

    public ContactListQueryProvider(ISearchIndex index, string repositoryPath, ID parentId, IEnumerable<ID> templateIds, bool includeSubFolders) : base(index,repositoryPath,parentId,templateIds,includeSubFolders)
    {
      /*Assert.ArgumentNotNull(templateIds, "templateIds");
      Assert.ArgumentNotNull(repositoryPath, "repositoryPath");
      this.templateIds = templateIds;
      this.IncludeSubFolders = includeSubFolders;
      this.repositoryPath = repositoryPath.ToLowerInvariant();
      this.repositoryPath = EnsureEndsWithSeparator(this.repositoryPath);
      this.parentId = parentId;*/
    }
    protected override object EnsureResultIsNotQueryable(object result)
    {
      object obj2 = base.EnsureResultIsNotQueryable(result);
      ContactList contactList = obj2 as ContactList;
      if (contactList != null)
      {
        if (!CheckSecurity(contactList)) return null;
      }
      ContactList[] source = obj2 as ContactList[];
      if (source == null)
      {
        return obj2;
      }
      source = source.Distinct<ContactList>().Where(l=>CheckSecurity(l)).ToArray<ContactList>();
      return source;
    }
    private bool CheckSecurity(ContactList contactList)
    {
      return listDatabase.GetItem(contactList.Id) != null;
    }

  }
}