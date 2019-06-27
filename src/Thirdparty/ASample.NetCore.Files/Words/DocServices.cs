using GemBox.Document;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace ASample.NetCore.Files.Words
{
    public class DocServices
    {
        private readonly DocumentOptions _options;

        public DocServices(IOptions<DocumentOptions> options)
        {
            _options = options.Value;
        }
        public async Task WriteAsync()
        {
            var path = Path.Combine(_options.TemplatePath, _options.TemplateName);
            // Load template document
            var document = DocumentModel.Load(path);

            document.MailMerge.FieldMerging += (sender, e) =>
            {
                if (e.IsValueFound)
                {
                    switch (e.FieldName)
                    {
                        case "Date":
                            ((Run)e.Inline).Text = ((DateTime)e.Value).ToString("dddd, MMMM d, yyyy", CultureInfo.InvariantCulture);
                            break;
                        case "Price":
                        case "Total":
                        case "GrandTotal":
                            ((Run)e.Inline).Text = ((double)e.Value).ToString("F2", CultureInfo.InvariantCulture);
                            break;
                    }
                }
            };

            // Fill table and grand total
            //document.MailMerge.Execute(model.Items, "Item");
            //document.MailMerge.Execute(model);
        }
    }
}
