﻿using Business.Abstract;
using Business.Constants;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Concrete
{
    public class RentalManager : IRentalService
    {
        IRentalDal _rentalDal;
        public IResult Add(Rental rental)
        {
            var result = _rentalDal.Get(r => r.Id == rental.Id && r.ReturnDate > DateTime.Now);

            if(result == null)
            {
                _rentalDal.Add(rental);
                return new SuccessResult();
            }
            return new ErrorResult(Messages.Error);

        }

        public IResult Delete(Rental rental)
        {
            var result = _rentalDal.GetAll(r => r.Id == rental.Id);
            if( result == null)
            {
                return new ErrorResult(Messages.Error);
            }
            _rentalDal.Delete(rental);
            return new SuccessResult();
        }

        public IDataResult<Rental> Get(int id)
        {
            return new SuccessDataResult<Rental>(_rentalDal.Get(r => r.Id == id));
        }

        public IDataResult<List<Rental>> GetAll()
        {
            return new SuccessDataResult<List<Rental>>(_rentalDal.GetAll());
        }

        public IResult Update(Rental rental)
        {
            var result = _rentalDal.GetAll(r => r.Id == rental.Id);
            if (result == null)
            {
                return new ErrorResult(Messages.Error);
            }
            _rentalDal.Update(rental);
            return new SuccessResult();
        }
    }
}