﻿using System;
using System.Collections;
using System.Collections.Generic;

namespace MortgageCalculator.Models.Repositories
{
    public interface IRepository
    {
        T GetById<T>(object id);
        object GetById(Type targetType, object id);
        IEnumerable<T> WhereAllEq<T>(IDictionary queryFields, IDictionary<string, bool> orderByFields = null);
        IEnumerable WhereAllEq(Type targetType, IDictionary queryFields);

        void Save<T>(T obj);
        void SaveOrUpdate<T>(T obj);
        void Update<T>(T obj);
        void Delete<T>(T obj);
    }
}