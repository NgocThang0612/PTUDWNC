using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TatBlog.Core.Contracts;
using TatBlog.Core.Entities;
using TatBlog.Data.Contexts;
using TatBlog.Services.Extensions;

namespace TatBlog.Services.Subscribers
{
    public class SubscriberRepository : ISubscriberRepository
    {
        private readonly BlogDBContext _context;

        public SubscriberRepository(BlogDBContext context)
        {
            _context = context;
        }

        public async Task<bool> SubscribeAsync(
            string email,
            CancellationToken cancellationToken = default)
        {
            if (!string.IsNullOrWhiteSpace(email) && !IsExistedEmail(email).Result)
            {
                Subscriber s = new Subscriber()
                {
                    Email = email,
                    SubscribeDate = DateTime.Now,
                };
                _context.Subscribers.Add(s);
                return await _context.SaveChangesAsync(cancellationToken) > 0;
            }
            else
                return false;
        }

        public async Task UnsubscribeAsync(
            string email,
            string reason,
            bool voluntary,
            CancellationToken cancellationToken = default)
        {
            if (!string.IsNullOrWhiteSpace(email) && IsExistedEmail(email).Result)
            {
                await _context.Set<Subscriber>()
                .Where(s => s.Email.Equals(email))
                .ExecuteUpdateAsync(p => p
                    .SetProperty(x => x.ResonUnsubscribe, x => reason)
                    .SetProperty(x => x.Voluntary, x => voluntary),
                    cancellationToken);
            }

        }

        public async Task BlockSubscriberAsync(
            int id,
            string reason,
            string notes,
            CancellationToken cancellationToken = default)
        {
            if (id > 0)
            {
                await _context.Set<Subscriber>()
                .Where(s => s.Id == id)
                .ExecuteUpdateAsync(p => p
                    .SetProperty(x => x.ResonUnsubscribe, x => reason)
                    .SetProperty(x => x.Voluntary, x => false)
                    .SetProperty(x => x.Notes, x => notes),
                    cancellationToken);
            }
        }

        public async Task DeleteSubscriberAsync(
            int id,
            CancellationToken cancellationToken = default)
        {


            await _context.Set<Subscriber>()
                .Where(t => t.Id == id)
                .ExecuteDeleteAsync(cancellationToken);
        }

        public async Task<Subscriber> GetSubscriberByEmailAsync(
            string email,
            CancellationToken cancellationToken = default)
        {
            return await _context.Set<Subscriber>()
                .Where(s => s.Email.Equals(email))
                .FirstOrDefaultAsync(cancellationToken);
        }

        public async Task<Subscriber> GetSubscriberByIdAsync(
            int id,
            CancellationToken cancellationToken = default)
        {
            return await _context.Set<Subscriber>()
                .Where(s => s.Id == id)
                .FirstOrDefaultAsync(cancellationToken);
        }

        public Task<IPagedList<Subscriber>> SearchSubscribersAsync(
            IPagingParams pagingParams,
            CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> IsExistedEmail(
            string email,
            CancellationToken cancellationToken = default)
        {
            return await _context.Set<Subscriber>()
                .AnyAsync(s => s.Email.Equals(email), cancellationToken);
        }

        public async Task<IPagedList<Subscriber>> GetPagedSubscribersAsync(
            int pageNumber = 1,
            int pageSize = 10,
            CancellationToken cancellationToken = default)
        {
            return await _context.Set<Subscriber>()
                .ToPagedListAsync(
                pageNumber, pageSize,
                nameof(Subscriber.Email), "DESC", 
                cancellationToken);
        }

        public async Task UpdateSubscriberAsync(
            Subscriber subscriber,
            CancellationToken cancellationToken = default)
        {
            if (subscriber.Id > 0)
            {
                _context.Set<Subscriber>().Update(subscriber);
            }
            
            await _context.SaveChangesAsync(cancellationToken);

        }
    }
}
