﻿using Chloe.Utility;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Chloe.Core
{
    class DbSession : IDbSession
    {
        DbContext _dbContext;
        internal DbSession(DbContext dbContext)
        {
            this._dbContext = dbContext;
        }

        public IDbContext DbContext { get { return this._dbContext; } }
        public bool IsInTransaction { get { return this._dbContext.InnerDbSession.IsInTransaction; } }

        public int ExecuteNonQuery(string sql, params DbParam[] parameters)
        {
            return this.ExecuteNonQuery(sql, CommandType.Text, parameters);
        }
        public int ExecuteNonQuery(string sql, CommandType cmdType, params DbParam[] parameters)
        {
            Utils.CheckNull(sql, "sql");
            return this._dbContext.InnerDbSession.ExecuteNonQuery(sql, parameters, cmdType);
        }

        public object ExecuteScalar(string sql, params DbParam[] parameters)
        {
            return this.ExecuteScalar(sql, CommandType.Text, parameters);
        }
        public object ExecuteScalar(string sql, CommandType cmdType, params DbParam[] parameters)
        {
            Utils.CheckNull(sql, "sql");
            return this._dbContext.InnerDbSession.ExecuteScalar(sql, parameters, cmdType);
        }

        public IDataReader ExecuteReader(string sql, params DbParam[] parameters)
        {
            return this.ExecuteReader(sql, CommandType.Text, parameters);
        }
        public IDataReader ExecuteReader(string sql, CommandType cmdType, params DbParam[] parameters)
        {
            Utils.CheckNull(sql, "sql");
            return this._dbContext.InnerDbSession.ExecuteInternalReader(sql, parameters, cmdType);
        }

        public void BeginTransaction()
        {
            this._dbContext.InnerDbSession.BeginTransaction();
        }
        public void BeginTransaction(IsolationLevel il)
        {
            this._dbContext.InnerDbSession.BeginTransaction(il);
        }
        public void CommitTransaction()
        {
            this._dbContext.InnerDbSession.CommitTransaction();
        }
        public void RollbackTransaction()
        {
            this._dbContext.InnerDbSession.RollbackTransaction();
        }

        public void Dispose()
        {
            this._dbContext.Dispose();
        }
    }
}
