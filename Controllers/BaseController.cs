using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using petder.data;
using petder.Models.DataModels;
using petder.Providers;

namespace petder.Controllers
{
    public class BaseController : Controller
    {
        protected readonly ApplicationDbContext _db;
        protected readonly IMapper _mapper;
        protected readonly IDateTimeProvider _dateTimeProvider;

        public BaseController(ApplicationDbContext _db)
        {
            this._db = _db;
        }
        public BaseController(ApplicationDbContext db, IDateTimeProvider dateTimeProvider)
        {
            _db = db;
            _dateTimeProvider = dateTimeProvider;
        }

        public BaseController(ApplicationDbContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        public BaseController(ApplicationDbContext db, IMapper mapper, IDateTimeProvider dateTimeProvider)
        {
            _db = db;
            _mapper = mapper;
            _dateTimeProvider = dateTimeProvider;
        }
    }
}