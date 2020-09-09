﻿using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace HQ78.Recipe.Api.Services
{
    public class MongoCollectionProvider<T> : IMongoCollection<T>
    {
        private readonly IMongoCollection<T> collection;

        public MongoCollectionProvider(
            IMongoDatabase mongoDatabase,
            MongoCollectionSettings mongoCollectionSettings
        )
        {
            var genericTypeName = typeof(T).Name;

            collection = mongoDatabase.GetCollection<T>(
                genericTypeName,
                mongoCollectionSettings
            );
        }

        public CollectionNamespace CollectionNamespace => collection.CollectionNamespace;

        public IMongoDatabase Database => collection.Database;

        public IBsonSerializer<T> DocumentSerializer => collection.DocumentSerializer;

        public IMongoIndexManager<T> Indexes => collection.Indexes;

        public MongoCollectionSettings Settings => collection.Settings;

        public IAsyncCursor<TResult> Aggregate<TResult>(
            PipelineDefinition<T, TResult> pipeline,
            AggregateOptions? options = null,
            CancellationToken cancellationToken = default
        )
        {
            return collection.Aggregate(pipeline, options, cancellationToken);
        }

        public IAsyncCursor<TResult> Aggregate<TResult>(
            IClientSessionHandle session,
            PipelineDefinition<T, TResult> pipeline,
            AggregateOptions? options = null,
            CancellationToken cancellationToken = default
        )
        {
            return collection.Aggregate(session, pipeline, options, cancellationToken);
        }

        public Task<IAsyncCursor<TResult>> AggregateAsync<TResult>(
            PipelineDefinition<T, TResult> pipeline,
            AggregateOptions? options = null,
            CancellationToken cancellationToken = default
        )
        {
            return collection.AggregateAsync(pipeline, options, cancellationToken);
        }

        public Task<IAsyncCursor<TResult>> AggregateAsync<TResult>(
            IClientSessionHandle session,
            PipelineDefinition<T, TResult> pipeline,
            AggregateOptions? options = null,
            CancellationToken cancellationToken = default
        )
        {
            return collection.AggregateAsync(session, pipeline, options, cancellationToken);
        }

        public void AggregateToCollection<TResult>(
            PipelineDefinition<T, TResult> pipeline,
            AggregateOptions? options = null,
            CancellationToken cancellationToken = default
        )
        {
            collection.AggregateToCollection(pipeline, options, cancellationToken);
        }

        public void AggregateToCollection<TResult>(
            IClientSessionHandle session,
            PipelineDefinition<T, TResult> pipeline,
            AggregateOptions? options = null,
            CancellationToken cancellationToken = default
        )
        {
            collection.AggregateToCollection(session, pipeline, options, cancellationToken);
        }

        public Task AggregateToCollectionAsync<TResult>(
            PipelineDefinition<T, TResult> pipeline,
            AggregateOptions? options = null,
            CancellationToken cancellationToken = default
        )
        {
            return collection.AggregateToCollectionAsync(pipeline, options, cancellationToken);
        }

        public Task AggregateToCollectionAsync<TResult>(
            IClientSessionHandle session,
            PipelineDefinition<T, TResult> pipeline,
            AggregateOptions? options = null,
            CancellationToken cancellationToken = default
        )
        {
            return collection.AggregateToCollectionAsync(session, pipeline, options, cancellationToken);
        }

        public BulkWriteResult<T> BulkWrite(
            IEnumerable<WriteModel<T>> requests,
            BulkWriteOptions? options = null,
            CancellationToken cancellationToken = default
        )
        {
            return collection.BulkWrite(requests, options, cancellationToken);
        }

        public BulkWriteResult<T> BulkWrite(
            IClientSessionHandle session,
            IEnumerable<WriteModel<T>> requests,
            BulkWriteOptions? options = null,
            CancellationToken cancellationToken = default
        )
        {
            return collection.BulkWrite(session, requests, options, cancellationToken);
        }

        public Task<BulkWriteResult<T>> BulkWriteAsync(
            IEnumerable<WriteModel<T>> requests,
            BulkWriteOptions? options = null,
            CancellationToken cancellationToken = default
        )
        {
            return collection.BulkWriteAsync(requests, options, cancellationToken);
        }

        public Task<BulkWriteResult<T>> BulkWriteAsync(
            IClientSessionHandle session,
            IEnumerable<WriteModel<T>> requests,
            BulkWriteOptions? options = null,
            CancellationToken cancellationToken = default
        )
        {
            return collection.BulkWriteAsync(session, requests, options, cancellationToken);
        }

        [Obsolete("Use CountDocuments or EstimatedDocumentCount instead.")]
        public long Count(
            FilterDefinition<T> filter,
            CountOptions? options = null,
            CancellationToken cancellationToken = default
        )
        {
            return collection.Count(filter, options, cancellationToken);
        }

        [Obsolete("Use CountDocuments or EstimatedDocumentCount instead.")]
        public long Count(
            IClientSessionHandle session,
            FilterDefinition<T> filter,
            CountOptions? options = null,
            CancellationToken cancellationToken = default
        )
        {
            return collection.Count(session, filter, options, cancellationToken);
        }

        [Obsolete("Use CountDocumentsAsync or EstimatedDocumentCountAsync instead.")]
        public Task<long> CountAsync(
            FilterDefinition<T> filter,
            CountOptions? options = null,
            CancellationToken cancellationToken = default
        )
        {
            return collection.CountAsync(filter, options, cancellationToken);
        }

        [Obsolete("Use CountDocumentsAsync or EstimatedDocumentCountAsync instead.")]
        public Task<long> CountAsync(
            IClientSessionHandle session,
            FilterDefinition<T> filter,
            CountOptions? options = null,
            CancellationToken cancellationToken = default
        )
        {
            return collection.CountAsync(session, filter, options, cancellationToken);
        }

        public long CountDocuments(
            FilterDefinition<T> filter,
            CountOptions? options = null,
            CancellationToken cancellationToken = default
        )
        {
            return collection.CountDocuments(filter, options, cancellationToken);
        }

        public long CountDocuments(
            IClientSessionHandle session,
            FilterDefinition<T> filter,
            CountOptions? options = null,
            CancellationToken cancellationToken = default
        )
        {
            return collection.CountDocuments(session, filter, options, cancellationToken);
        }

        public Task<long> CountDocumentsAsync(
            FilterDefinition<T> filter,
            CountOptions? options = null,
            CancellationToken cancellationToken = default)
        {
            return collection.CountDocumentsAsync(filter, options, cancellationToken);
        }

        public Task<long> CountDocumentsAsync(
            IClientSessionHandle session,
            FilterDefinition<T> filter,
            CountOptions? options = null,
            CancellationToken cancellationToken = default)
        {
            return collection.CountDocumentsAsync(session, filter, options, cancellationToken);
        }

        public DeleteResult DeleteMany(
            FilterDefinition<T> filter,
            CancellationToken cancellationToken = default
        )
        {
            return collection.DeleteMany(filter, cancellationToken);
        }

        public DeleteResult DeleteMany(
            FilterDefinition<T> filter,
            DeleteOptions options,
            CancellationToken cancellationToken = default
        )
        {
            return collection.DeleteMany(filter, options, cancellationToken);
        }

        public DeleteResult DeleteMany(
            IClientSessionHandle session,
            FilterDefinition<T> filter,
            DeleteOptions? options = null,
            CancellationToken cancellationToken = default
        )
        {
            return collection.DeleteMany(session, filter, options, cancellationToken);
        }

        public Task<DeleteResult> DeleteManyAsync(
            FilterDefinition<T> filter,
            CancellationToken cancellationToken = default
        )
        {
            return collection.DeleteManyAsync(filter, cancellationToken);
        }

        public Task<DeleteResult> DeleteManyAsync(
            FilterDefinition<T> filter,
            DeleteOptions options,
            CancellationToken cancellationToken = default
        )
        {
            return collection.DeleteManyAsync(filter, options, cancellationToken);
        }

        public Task<DeleteResult> DeleteManyAsync(
            IClientSessionHandle session,
            FilterDefinition<T> filter,
            DeleteOptions? options = null,
            CancellationToken cancellationToken = default
        )
        {
            return collection.DeleteManyAsync(session, filter, options, cancellationToken);
        }

        public DeleteResult DeleteOne(
            FilterDefinition<T> filter,
            CancellationToken cancellationToken = default
        )
        {
            return collection.DeleteOne(filter, cancellationToken);
        }

        public DeleteResult DeleteOne(
            FilterDefinition<T> filter,
            DeleteOptions options,
            CancellationToken cancellationToken = default
        )
        {
            return collection.DeleteOne(filter, options, cancellationToken);
        }

        public DeleteResult DeleteOne(
            IClientSessionHandle session,
            FilterDefinition<T> filter,
            DeleteOptions? options = null,
            CancellationToken cancellationToken = default
        )
        {
            return collection.DeleteOne(session, filter, options, cancellationToken);
        }

        public Task<DeleteResult> DeleteOneAsync(
            FilterDefinition<T> filter,
            CancellationToken cancellationToken = default
        )
        {
            return collection.DeleteOneAsync(filter, cancellationToken);
        }

        public Task<DeleteResult> DeleteOneAsync(
            FilterDefinition<T> filter,
            DeleteOptions options,
            CancellationToken cancellationToken = default
        )
        {
            return collection.DeleteOneAsync(filter, options, cancellationToken);
        }

        public Task<DeleteResult> DeleteOneAsync(
            IClientSessionHandle session,
            FilterDefinition<T> filter,
            DeleteOptions? options = null,
            CancellationToken cancellationToken = default
        )
        {
            return collection.DeleteOneAsync(session, filter, options, cancellationToken);
        }

        public IAsyncCursor<TField> Distinct<TField>(
            FieldDefinition<T, TField> field,
            FilterDefinition<T> filter,
            DistinctOptions? options = null,
            CancellationToken cancellationToken = default
        )
        {
            return collection.Distinct(field, filter, options, cancellationToken);
        }

        public IAsyncCursor<TField> Distinct<TField>(
            IClientSessionHandle session,
            FieldDefinition<T, TField> field,
            FilterDefinition<T> filter,
            DistinctOptions? options = null,
            CancellationToken cancellationToken = default
        )
        {
            return collection.Distinct(session, field, filter, options, cancellationToken);
        }

        public Task<IAsyncCursor<TField>> DistinctAsync<TField>(
            FieldDefinition<T, TField> field,
            FilterDefinition<T> filter,
            DistinctOptions? options = null,
            CancellationToken cancellationToken = default
        )
        {
            return collection.DistinctAsync(field, filter, options, cancellationToken);
        }

        public Task<IAsyncCursor<TField>> DistinctAsync<TField>(
            IClientSessionHandle session,
            FieldDefinition<T, TField> field,
            FilterDefinition<T> filter,
            DistinctOptions? options = null,
            CancellationToken cancellationToken = default
        )
        {
            return collection.DistinctAsync(session, field, filter, options, cancellationToken);
        }

        public long EstimatedDocumentCount(
            EstimatedDocumentCountOptions? options = null,
            CancellationToken cancellationToken = default
        )
        {
            return collection.EstimatedDocumentCount(options, cancellationToken);
        }

        public Task<long> EstimatedDocumentCountAsync(
            EstimatedDocumentCountOptions? options = null,
            CancellationToken cancellationToken = default
        )
        {
            return collection.EstimatedDocumentCountAsync(options, cancellationToken);
        }

        public Task<IAsyncCursor<TProjection>> FindAsync<TProjection>(
            FilterDefinition<T> filter,
            FindOptions<T, TProjection>? options = null,
            CancellationToken cancellationToken = default
        )
        {
            return collection.FindAsync(filter, options, cancellationToken);
        }

        public Task<IAsyncCursor<TProjection>> FindAsync<TProjection>(
            IClientSessionHandle session,
            FilterDefinition<T> filter,
            FindOptions<T, TProjection>? options = null,
            CancellationToken cancellationToken = default
        )
        {
            return collection.FindAsync(session, filter, options, cancellationToken);
        }

        public TProjection FindOneAndDelete<TProjection>(
            FilterDefinition<T> filter,
            FindOneAndDeleteOptions<T, TProjection>? options = null,
            CancellationToken cancellationToken = default
        )
        {
            return collection.FindOneAndDelete(filter, options, cancellationToken);
        }

        public TProjection FindOneAndDelete<TProjection>(
            IClientSessionHandle session,
            FilterDefinition<T> filter,
            FindOneAndDeleteOptions<T, TProjection>? options = null,
            CancellationToken cancellationToken = default
        )
        {
            return collection.FindOneAndDelete(session, filter, options, cancellationToken);
        }

        public Task<TProjection> FindOneAndDeleteAsync<TProjection>(
            FilterDefinition<T> filter,
            FindOneAndDeleteOptions<T, TProjection>? options = null,
            CancellationToken cancellationToken = default
        )
        {
            return collection.FindOneAndDeleteAsync(filter, options, cancellationToken);
        }

        public Task<TProjection> FindOneAndDeleteAsync<TProjection>(
            IClientSessionHandle session,
            FilterDefinition<T> filter,
            FindOneAndDeleteOptions<T, TProjection>? options = null,
            CancellationToken cancellationToken = default
        )
        {
            return collection.FindOneAndDeleteAsync(session, filter, options, cancellationToken);
        }

        public TProjection FindOneAndReplace<TProjection>(
            FilterDefinition<T> filter,
            T replacement,
            FindOneAndReplaceOptions<T, TProjection>? options = null,
            CancellationToken cancellationToken = default
        )
        {
            return collection.FindOneAndReplace(filter, replacement, options, cancellationToken);
        }

        public TProjection FindOneAndReplace<TProjection>(
            IClientSessionHandle session,
            FilterDefinition<T> filter,
            T replacement,
            FindOneAndReplaceOptions<T, TProjection>? options = null,
            CancellationToken cancellationToken = default
        )
        {
            return collection.FindOneAndReplace(session, filter, replacement, options, cancellationToken);
        }

        public Task<TProjection> FindOneAndReplaceAsync<TProjection>(
            FilterDefinition<T> filter,
            T replacement,
            FindOneAndReplaceOptions<T, TProjection>? options = null,
            CancellationToken cancellationToken = default
        )
        {
            return collection.FindOneAndReplaceAsync(filter, replacement, options, cancellationToken);
        }

        public Task<TProjection> FindOneAndReplaceAsync<TProjection>(
            IClientSessionHandle session,
            FilterDefinition<T> filter,
            T replacement,
            FindOneAndReplaceOptions<T, TProjection>? options = null,
            CancellationToken cancellationToken = default
        )
        {
            return collection.FindOneAndReplaceAsync(session, filter, replacement, options, cancellationToken);
        }

        public TProjection FindOneAndUpdate<TProjection>(
            FilterDefinition<T> filter,
            UpdateDefinition<T> update,
            FindOneAndUpdateOptions<T, TProjection>? options = null,
            CancellationToken cancellationToken = default
        )
        {
            return collection.FindOneAndUpdate(filter, update, options, cancellationToken);
        }

        public TProjection FindOneAndUpdate<TProjection>(
            IClientSessionHandle session,
            FilterDefinition<T> filter,
            UpdateDefinition<T> update,
            FindOneAndUpdateOptions<T, TProjection>? options = null,
            CancellationToken cancellationToken = default
        )
        {
            return collection.FindOneAndUpdate(session, filter, update, options, cancellationToken);
        }

        public Task<TProjection> FindOneAndUpdateAsync<TProjection>(
            FilterDefinition<T> filter,
            UpdateDefinition<T> update,
            FindOneAndUpdateOptions<T, TProjection>? options = null,
            CancellationToken cancellationToken = default
        )
        {
            return collection.FindOneAndUpdateAsync(filter, update, options, cancellationToken);
        }

        public Task<TProjection> FindOneAndUpdateAsync<TProjection>(
            IClientSessionHandle session,
            FilterDefinition<T> filter,
            UpdateDefinition<T> update,
            FindOneAndUpdateOptions<T, TProjection>? options = null,
            CancellationToken cancellationToken = default
        )
        {
            return collection.FindOneAndUpdateAsync(session, filter, update, options, cancellationToken);
        }

        public IAsyncCursor<TProjection> FindSync<TProjection>(
            FilterDefinition<T> filter,
            FindOptions<T, TProjection>? options = null,
            CancellationToken cancellationToken = default
        )
        {
            return collection.FindSync(filter, options, cancellationToken);
        }

        public IAsyncCursor<TProjection> FindSync<TProjection>(
            IClientSessionHandle session,
            FilterDefinition<T> filter,
            FindOptions<T, TProjection>? options = null,
            CancellationToken cancellationToken = default
        )
        {
            return collection.FindSync(session, filter, options, cancellationToken);
        }

        public void InsertMany(
            IEnumerable<T> documents,
            InsertManyOptions? options = null,
            CancellationToken cancellationToken = default
        )
        {
            collection.InsertMany(documents, options, cancellationToken);
        }

        public void InsertMany(
            IClientSessionHandle session,
            IEnumerable<T> documents,
            InsertManyOptions? options = null,
            CancellationToken cancellationToken = default
        )
        {
            collection.InsertMany(session, documents, options, cancellationToken);
        }

        public Task InsertManyAsync(
            IEnumerable<T> documents,
            InsertManyOptions? options = null,
            CancellationToken cancellationToken = default
        )
        {
            return collection.InsertManyAsync(documents, options, cancellationToken);
        }

        public Task InsertManyAsync(
            IClientSessionHandle session,
            IEnumerable<T> documents,
            InsertManyOptions? options = null,
            CancellationToken cancellationToken = default
        )
        {
            return collection.InsertManyAsync(session, documents, options, cancellationToken);
        }

        public void InsertOne(
            T document,
            InsertOneOptions? options = null,
            CancellationToken cancellationToken = default
        )
        {
            collection.InsertOne(document, options, cancellationToken);
        }

        public void InsertOne(
            IClientSessionHandle session,
            T document,
            InsertOneOptions? options = null,
            CancellationToken cancellationToken = default
        )
        {
            collection.InsertOne(session, document, options, cancellationToken);
        }

        [Obsolete("Use the new overload of InsertOneAsync with an InsertOneOptions parameter instead.")]
        public Task InsertOneAsync(T document, CancellationToken _cancellationToken)
        {
            return collection.InsertOneAsync(document, _cancellationToken);
        }

        public Task InsertOneAsync(
            T document,
            InsertOneOptions? options = null,
            CancellationToken cancellationToken = default
        )
        {
            return collection.InsertOneAsync(document, options, cancellationToken);
        }

        public Task InsertOneAsync(
            IClientSessionHandle session,
            T document,
            InsertOneOptions? options = null,
            CancellationToken cancellationToken = default
        )
        {
            return collection.InsertOneAsync(session, document, options, cancellationToken);
        }

        public IAsyncCursor<TResult> MapReduce<TResult>(
            BsonJavaScript map,
            BsonJavaScript reduce,
            MapReduceOptions<T, TResult>? options = null,
            CancellationToken cancellationToken = default
        )
        {
            return collection.MapReduce(map, reduce, options, cancellationToken);
        }

        public IAsyncCursor<TResult> MapReduce<TResult>(
            IClientSessionHandle session,
            BsonJavaScript map,
            BsonJavaScript reduce,
            MapReduceOptions<T, TResult>? options = null,
            CancellationToken cancellationToken = default
        )
        {
            return collection.MapReduce(session, map, reduce, options, cancellationToken);
        }

        public Task<IAsyncCursor<TResult>> MapReduceAsync<TResult>(
            BsonJavaScript map,
            BsonJavaScript reduce,
            MapReduceOptions<T, TResult>? options = null,
            CancellationToken cancellationToken = default
        )
        {
            return collection.MapReduceAsync(map, reduce, options, cancellationToken);
        }

        public Task<IAsyncCursor<TResult>> MapReduceAsync<TResult>(
            IClientSessionHandle session,
            BsonJavaScript map,
            BsonJavaScript reduce,
            MapReduceOptions<T, TResult>? options = null,
            CancellationToken cancellationToken = default
        )
        {
            return collection.MapReduceAsync(session, map, reduce, options, cancellationToken);
        }

        public IFilteredMongoCollection<TDerivedDocument> OfType<TDerivedDocument>() where TDerivedDocument : T
        {
            return collection.OfType<TDerivedDocument>();
        }

        public ReplaceOneResult ReplaceOne(
            FilterDefinition<T> filter,
            T replacement,
            ReplaceOptions? options = null,
            CancellationToken cancellationToken = default
        )
        {
            return collection.ReplaceOne(filter, replacement, options, cancellationToken);
        }

        [Obsolete("Use the overload that takes a ReplaceOptions instead of an UpdateOptions.")]
        public ReplaceOneResult ReplaceOne(
            FilterDefinition<T> filter,
            T replacement,
            UpdateOptions options,
            CancellationToken cancellationToken = default
        )
        {
            return collection.ReplaceOne(filter, replacement, options, cancellationToken);
        }

        public ReplaceOneResult ReplaceOne(
            IClientSessionHandle session,
            FilterDefinition<T> filter,
            T replacement,
            ReplaceOptions? options = null,
            CancellationToken cancellationToken = default
        )
        {
            return collection.ReplaceOne(session, filter, replacement, options, cancellationToken);
        }

        [Obsolete("Use the overload that takes a ReplaceOptions instead of an UpdateOptions.")]
        public ReplaceOneResult ReplaceOne(
            IClientSessionHandle session,
            FilterDefinition<T> filter,
            T replacement,
            UpdateOptions options,
            CancellationToken cancellationToken = default
        )
        {
            return collection.ReplaceOne(session, filter, replacement, options, cancellationToken);
        }

        public Task<ReplaceOneResult> ReplaceOneAsync(
            FilterDefinition<T> filter,
            T replacement,
            ReplaceOptions? options = null,
            CancellationToken cancellationToken = default
        )
        {
            return collection.ReplaceOneAsync(filter, replacement, options, cancellationToken);
        }

        [Obsolete("Use the overload that takes a ReplaceOptions instead of an UpdateOptions.")]
        public Task<ReplaceOneResult> ReplaceOneAsync(
            FilterDefinition<T> filter,
            T replacement,
            UpdateOptions options,
            CancellationToken cancellationToken = default
        )
        {
            return collection.ReplaceOneAsync(filter, replacement, options, cancellationToken);
        }

        public Task<ReplaceOneResult> ReplaceOneAsync(
            IClientSessionHandle session,
            FilterDefinition<T> filter,
            T replacement,
            ReplaceOptions? options = null,
            CancellationToken cancellationToken = default
        )
        {
            return collection.ReplaceOneAsync(session, filter, replacement, options, cancellationToken);
        }

        [Obsolete("Use the overload that takes a ReplaceOptions instead of an UpdateOptions.")]
        public Task<ReplaceOneResult> ReplaceOneAsync(
            IClientSessionHandle session,
            FilterDefinition<T> filter,
            T replacement,
            UpdateOptions options,
            CancellationToken cancellationToken = default
        )
        {
            return collection.ReplaceOneAsync(session, filter, replacement, options, cancellationToken);
        }

        public UpdateResult UpdateMany(
            FilterDefinition<T> filter,
            UpdateDefinition<T> update,
            UpdateOptions? options = null,
            CancellationToken cancellationToken = default
        )
        {
            return collection.UpdateMany(filter, update, options, cancellationToken);
        }

        public UpdateResult UpdateMany(
            IClientSessionHandle session,
            FilterDefinition<T> filter,
            UpdateDefinition<T> update,
            UpdateOptions? options = null,
            CancellationToken cancellationToken = default
        )
        {
            return collection.UpdateMany(session, filter, update, options, cancellationToken);
        }

        public Task<UpdateResult> UpdateManyAsync(
            FilterDefinition<T> filter,
            UpdateDefinition<T> update,
            UpdateOptions? options = null,
            CancellationToken cancellationToken = default
        )
        {
            return collection.UpdateManyAsync(filter, update, options, cancellationToken);
        }

        public Task<UpdateResult> UpdateManyAsync(
            IClientSessionHandle session,
            FilterDefinition<T> filter,
            UpdateDefinition<T> update,
            UpdateOptions? options = null,
            CancellationToken cancellationToken = default
        )
        {
            return collection.UpdateManyAsync(session, filter, update, options, cancellationToken);
        }

        public UpdateResult UpdateOne(
            FilterDefinition<T> filter,
            UpdateDefinition<T> update,
            UpdateOptions? options = null,
            CancellationToken cancellationToken = default
        )
        {
            return collection.UpdateOne(filter, update, options, cancellationToken);
        }

        public UpdateResult UpdateOne(
            IClientSessionHandle session,
            FilterDefinition<T> filter,
            UpdateDefinition<T> update,
            UpdateOptions? options = null,
            CancellationToken cancellationToken = default
        )
        {
            return collection.UpdateOne(session, filter, update, options, cancellationToken);
        }

        public Task<UpdateResult> UpdateOneAsync(
            FilterDefinition<T> filter,
            UpdateDefinition<T> update,
            UpdateOptions? options = null,
            CancellationToken cancellationToken = default
        )
        {
            return collection.UpdateOneAsync(filter, update, options, cancellationToken);
        }

        public Task<UpdateResult> UpdateOneAsync(
            IClientSessionHandle session,
            FilterDefinition<T> filter,
            UpdateDefinition<T> update,
            UpdateOptions? options = null,
            CancellationToken cancellationToken = default
        )
        {
            return collection.UpdateOneAsync(session, filter, update, options, cancellationToken);
        }

        public IChangeStreamCursor<TResult> Watch<TResult>(
            PipelineDefinition<ChangeStreamDocument<T>, TResult> pipeline,
            ChangeStreamOptions? options = null,
            CancellationToken cancellationToken = default
        )
        {
            return collection.Watch(pipeline, options, cancellationToken);
        }

        public IChangeStreamCursor<TResult> Watch<TResult>(
            IClientSessionHandle session,
            PipelineDefinition<ChangeStreamDocument<T>, TResult> pipeline,
            ChangeStreamOptions? options = null,
            CancellationToken cancellationToken = default
        )
        {
            return collection.Watch(session, pipeline, options, cancellationToken);
        }

        public Task<IChangeStreamCursor<TResult>> WatchAsync<TResult>(
            PipelineDefinition<ChangeStreamDocument<T>, TResult> pipeline,
            ChangeStreamOptions? options = null,
            CancellationToken cancellationToken = default
        )
        {
            return collection.WatchAsync(pipeline, options, cancellationToken);
        }

        public Task<IChangeStreamCursor<TResult>> WatchAsync<TResult>(
            IClientSessionHandle session,
            PipelineDefinition<ChangeStreamDocument<T>, TResult> pipeline,
            ChangeStreamOptions? options = null,
            CancellationToken cancellationToken = default
        )
        {
            return collection.WatchAsync(session, pipeline, options, cancellationToken);
        }

        public IMongoCollection<T> WithReadConcern(ReadConcern readConcern)
        {
            return collection.WithReadConcern(readConcern);
        }

        public IMongoCollection<T> WithReadPreference(ReadPreference readPreference)
        {
            return collection.WithReadPreference(readPreference);
        }

        public IMongoCollection<T> WithWriteConcern(WriteConcern writeConcern)
        {
            return collection.WithWriteConcern(writeConcern);
        }
    }
}
