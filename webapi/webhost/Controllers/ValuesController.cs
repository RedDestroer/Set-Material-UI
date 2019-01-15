using System.IO;
using System.Xml;
using Microsoft.AspNetCore.Mvc;
using SetMeta.Abstract;
using SetMeta.Entities;
using SetMeta.Util;
using WebHost.Other;
using WebHost.Requests;

namespace WebHost.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OptionSetController : ControllerBase
    {
        // POST api/optionSet/parseOptionSet
        [HttpPost]
        public ActionResult<OptionSet> ParseOptionsSet(ParseOptionsSetRequest request)
        {
            Validate.NotNull(request, nameof(request));
            Validate.NotNull(request.Data, nameof(request.Data));

            OptionSetParser parser = null;
            if (!string.IsNullOrWhiteSpace(request.Version))
                parser = OptionSetParser.Create(request.Version);

            if (parser == null)
            {
                using (var stringReader = new StringReader(request.Data))
                using (var xmlTextReader = new XmlTextReader(stringReader))
                {
                    parser = OptionSetParser.Create(xmlTextReader);
                }
            }

            var validator = new DefaultOptionSetValidator();
            var result = parser.Parse(request.Data, validator);

            return Ok(result);
        }
    }
}
