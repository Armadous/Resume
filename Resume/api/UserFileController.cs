using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using Resume.Models;
using System.IO;
using System.Threading.Tasks;
using System.Net.Http.Headers;
using NHibernate;
using NHibernate.Linq;

namespace Resume.api
{
    public class UserFileController : ApiController
    {
        private readonly ISession db;
        public UserFileController(ISession session)
        {
            db = session;
        }

        // GET api/UserFile
        [Authorize]
        public IQueryable<UserFile> GetUserFiles()
        {
            return db.Query<UserFile>().Where(f => f.OwnerIdentity == User.Identity.Name);
        }

        // GET api/UserFile/5
        public HttpResponseMessage GetUserFile(int id)
        {
            UserFile userfile = db.Get<UserFile>(id);
            var response = new HttpResponseMessage();
            if (userfile == null)
            {
                response.StatusCode = HttpStatusCode.NotFound;
                return response;
            }

            var root = System.Web.HttpContext.Current.Server.MapPath("~/App_Data/userfiles");
            response.Content = new StreamContent(new FileStream(String.Format("{0}/{1}", root, userfile.LocalFileName), FileMode.Open));
            response.Content.Headers.ContentType = new MediaTypeHeaderValue(userfile.ContentType);            

            return response;
        }

        // POST api/UserFile
        [ResponseType(typeof(UserFile))]
        [Authorize]
        public async Task<IHttpActionResult> PostUserFile()
        {
            HttpRequestMessage request = this.Request;
            if (!request.Content.IsMimeMultipartContent())
                throw new HttpResponseException(HttpStatusCode.UnsupportedMediaType);

            var root = System.Web.HttpContext.Current.Server.MapPath("~/App_Data/userfiles");
            var provider = new MultipartFormDataStreamProvider(root);

            await request.Content.ReadAsMultipartAsync(provider);

            var data = provider.FileData.FirstOrDefault();
            var fileName = data.Headers.ContentDisposition.FileName;
            if(string.IsNullOrEmpty(fileName))
                throw new HttpResponseException(HttpStatusCode.NotAcceptable);

            var fileInfo = new FileInfo(data.LocalFileName);

            var record = new UserFile() 
            { 
                FileName = fileName, 
                LocalFileName = fileInfo.Name, 
                ContentType = data.Headers.ContentType.ToString(), 
                FileGuid = Guid.NewGuid(), 
                OwnerIdentity = User.Identity.Name 
            };

            db.Save(record);

            return Ok(record);
        }

        // DELETE api/UserFile/5
        [ResponseType(typeof(UserFile))]
        [Authorize]
        public IHttpActionResult DeleteUserFile(int id)
        {
            UserFile userfile = db.Get<UserFile>(id);
            if (userfile == null)
            {
                return NotFound();
            }

            // Can you delete this?
            if (userfile.OwnerIdentity != User.Identity.Name)
                return BadRequest();

            using(var tx = db.BeginTransaction())
            {
                db.Delete(userfile);
                tx.Commit();
            }

            return Ok(userfile);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool UserFileExists(int id)
        {
            return db.Get<UserFile>(id) != null;
        }
    }
}