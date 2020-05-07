﻿using Oqtane.Models;
using System.Threading.Tasks;
using System.Net.Http;
using System.Linq;
using System.Collections.Generic;
using System.Net;
using System;


namespace Oqtane.Services
{
    public class AliasService : ServiceBase, IAliasService
    {
        
        public AliasService(HttpClient http) :base(http) { }

        private string Apiurl => CreateApiUrl("Alias");

        public async Task<List<Alias>> GetAliasesAsync()
        {
            List<Alias> aliases = await GetJsonAsync<List<Alias>>(Apiurl);
            return aliases.OrderBy(item => item.Name).ToList();
        }

        public async Task<Alias> GetAliasAsync(int aliasId)
        {
            return await GetJsonAsync<Alias>($"{Apiurl}/{aliasId}");
        }

        public async Task<Alias> GetAliasAsync(string name, DateTime lastSyncDate)
        {
            name = (string.IsNullOrEmpty(name)) ? "~" : name;
            return await GetJsonAsync<Alias>($"{Apiurl}/name/{WebUtility.UrlEncode(name)}?sync={lastSyncDate.ToString("yyyyMMddHHmmssfff")}");
        }

        public async Task<Alias> AddAliasAsync(Alias alias)
        {
            return await PostJsonAsync<Alias>(Apiurl, alias);
        }

        public async Task<Alias> UpdateAliasAsync(Alias alias)
        {
            return await PutJsonAsync<Alias>($"{Apiurl}/{alias.AliasId}", alias);
        }
        public async Task DeleteAliasAsync(int aliasId)
        {
            await DeleteAsync($"{Apiurl}/{aliasId}");
        }
    }
}
