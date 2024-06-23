using EveryCupShop.Core.Interfaces.Repositories;
using EveryCupShop.Core.Models;
using EveryCupShop.Infrastructure.Database;

namespace EveryCupShop.Infrastructure.Repositories;

public class CupAttachmentEfRepository : EfRepository<CupAttachment>, ICupAttachmentRepository
{
    public CupAttachmentEfRepository(AppDbContext context) : base(context) { }
}