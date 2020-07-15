using UploadFile.WebApi.Repositories.Interfaces;
using UploadFile.WebApi.Models;
using UploadFile.WebApi.Context;
using System.Threading.Tasks;
using UploadFile.WebApi.DTOs;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace UploadFile.WebApi.Repositories
{
    public class DocumentRepository : BaseRepository<Document>, IDocumentRepository
    {
        public DocumentRepository(UploadFileDbContext db)
        :base(db) {    }

        public async Task<List<DocumentDTO>> GetDocumentByUser(long userId)
        {
            return await _db.Document.Where(x =>( x.UserId == userId && !x.Removed.Value))
                                .Select(res => 
                                    new DocumentDTO {
                                        Id = res.Id,
                                        FileName = res.FileName,
                                        UserId = res.UserId
                                    }
                                ).ToListAsync();
        }

        public async Task DocumentDelete(long documentId)
        {
            var user = await _db.Document.FindAsync(documentId);

            user.Removed = true;

            await _db.SaveChangesAsync();
        }
    }
}