﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Raven.Client;
using Raven.Client.Document;
using RavenDbDemo.DAL.Repository;
using StingyPrice.DAL.Models;

namespace StingyPrice.DAL.Repositories {
  public class RavenRepository : IRepository {
    private DocumentStore _store;
    private IDocumentSession _session;

    public RavenRepository(DocumentStore store) {
      _store = store;
      _session = _store.OpenSession();
    }

    public T SingleOrDefault<T>(Func<T, bool> predicate) where T : IModel {
      return _session.Query<T>().SingleOrDefault(predicate);
    }

    public IEnumerable<T> All<T>() where T : IModel {
      return _session.Query<T>();
    }

    public void Add<T>(T item) where T : IModel {
      _session.Store(item);
    }

    public void Delete<T>(T item) where T : IModel {
      _session.Delete(item);
    }

    public void Save() {
      _session.SaveChanges();
    }
  }
}