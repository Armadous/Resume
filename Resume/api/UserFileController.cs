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

namespace Resume.api
{
    public class UserFileController : ApiController
    {
        private ResumeDb db = new ResumeDb();

        // GET api/UserFile
        public IQueryable<UserFile> GetUserFiles()
        {
            return db.UserFiles;
        }

        // GET api/UserFile/5
        public HttpResponseMessage GetUserFile(int id)
        {
            UserFile userfile = db.UserFiles.Find(id);
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

        // PUT api/UserFile/5
        public IHttpActionResult PutUserFile(int id, UserFile userfile)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != userfile.Id)
            {
                return BadRequest();
            }

            db.Entry(userfile).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserFileExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST api/UserFile
        [ResponseType(typeof(UserFile))]
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

            var record = new UserFile() { FileName = fileName, LocalFileName = fileInfo.Name, ContentType = data.Headers.ContentType.ToString(), FileGuid = Guid.NewGuid() };
            using (var db = new ResumeDb())
            {
                db.UserFiles.Add(record);
                db.SaveChanges();
            }

            return Ok(record);
        }

        // DELETE api/UserFile/5
        [ResponseType(typeof(UserFile))]
        public IHttpActionResult DeleteUserFile(int id)
        {
            UserFile userfile = db.UserFiles.Find(id);
            if (userfile == null)
            {
                return NotFound();
            }

            db.UserFiles.Remove(userfile);
            db.SaveChanges();

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
            return db.UserFiles.Count(e => e.Id == id) > 0;
        }
    }
}