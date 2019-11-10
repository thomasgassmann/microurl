﻿namespace MicroUrl.Web.Markdown
{
    using System.Threading.Tasks;

    public interface IMarkdownService
    {
        Task<string> GetMarkdownStringAsync(string key);
    }
}
