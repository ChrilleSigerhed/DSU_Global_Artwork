﻿using DSU21_5.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Linq.Expressions;

namespace DSU21_5Tests.Data
{
    //internal class TestDbSet<T> : DbSet<T>, IQueryable, IEnumerable<T> where T : class
    //{
    //ObservableCollection<T> _data;
    //IQueryable _query;

    //public TestDbSet()
    //{
    //    _data = new ObservableCollection<T>();
    //    _query = _data.AsQueryable();
    //}

    //public override T Add(T item)
    //{
    //    _data.Add(item);
    //    return item;
    //}

    //public override T Remove(T item)
    //{
    //    _data.Remove(item);
    //    return item;
    //}

    //public override T Attach(T item)
    //{
    //    _data.Add(item);
    //    return item;
    //}

    //public override T Create()
    //{
    //    return Activator.CreateInstance<T>();
    //}

    //public override TDerivedEntity Create<TDerivedEntity>()
    //{
    //    return Activator.CreateInstance<TDerivedEntity>();
    //}

    //public override ObservableCollection<T> Local
    //{
    //    get { return _data; }
    //}

    //Type IQueryable.ElementType
    //{
    //    get { return _query.ElementType; }
    //}

    //Expression IQueryable.Expression
    //{
    //    get { return _query.Expression; }
    //}

    //IQueryProvider IQueryable.Provider
    //{
    //    get { return new TestDbAsyncQueryProvider<T>(_query.Provider); }
    //}

    //System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
    //{
    //    return _data.GetEnumerator();
    //}

    //IEnumerator<T> IEnumerable<T>.GetEnumerator()
    //{
    //    return _data.GetEnumerator();
    //}
    //    }
}