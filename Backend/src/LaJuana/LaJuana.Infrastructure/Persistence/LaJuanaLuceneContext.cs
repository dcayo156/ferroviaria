using Lucene.Net.Analysis;
using Lucene.Net.Analysis.Standard;
using Lucene.Net.Documents;
using Lucene.Net.Index;
using Lucene.Net.QueryParsers.Classic;
using Lucene.Net.Search;
using Lucene.Net.Store;
using Lucene.Net.Util;

using LuceneDirectory = Lucene.Net.Store.Directory;
using System.Configuration;  
using Microsoft.Extensions.Options;
using LaJuana.Application.Models;
using LaJuana.Application.Contracts.Infrastructure;
namespace LaJuana.Infrastructure.Persistence
{
    public class LaJuanaLuceneContext : ILuceneService
    {
        
        private String _indexPath;
        protected const LuceneVersion LUCENE_VERSION=LuceneVersion.LUCENE_48;
        public LuceneSettings _luceneSetting { get; }
        public LaJuanaLuceneContext(IOptions<LuceneSettings> luceneSetting)
        {
            var o=ConfigurationManager.AppSettings.AllKeys;
            _luceneSetting=luceneSetting.Value;
            _indexPath=_indexPath=_luceneSetting.LuceneDirectory != null
                    ?
                _luceneSetting.LuceneDirectory:
                "/home/shassain/code/luceneDB"; 
                Console.Write(_indexPath);                
        }
        public string getIndexPathContext(){
             Console.Write(_indexPath); 
            return _indexPath;
        }
        private string getIndexPath(string indexName)
        {
            return Path.Combine(getIndexPathContext(), indexName);
        }
        public T openReader<T>(string index, Func<IndexSearcher, Analyzer, T> func)
        {
            using (FSDirectory directory = FSDirectory.Open(getIndexPath(index)))
            using (Analyzer analyzer = new StandardAnalyzer(LUCENE_VERSION))
            using (IndexReader reader = DirectoryReader.Open(directory))
            {
                IndexSearcher searcher = new IndexSearcher(reader);
                return func(searcher, analyzer);
            }
        }
        void openWrite(string index, Func<IndexWriter, IndexWriter> func)
        {
            using (FSDirectory directory = FSDirectory.Open(getIndexPath(index)))
            using (Analyzer analyzer = new StandardAnalyzer(LUCENE_VERSION))
            {
                IndexWriterConfig config = new IndexWriterConfig(LUCENE_VERSION, analyzer);
                config.OpenMode = OpenMode.CREATE_OR_APPEND;
                using (IndexWriter writer = func(new IndexWriter(directory, config)))
                {
                    writer.Commit();
                    try
                    {
                        writer.Dispose();
                        directory.Dispose();
                    }
                    finally
                    {
                        if (IndexWriter.IsLocked(directory))
                        {
                            IndexWriter.Unlock(directory);
                        }
                    }
                }
            }
        }
        public void AddDocument(Document doc, string index)
        {
            this.openWrite(index, (writer) =>
            {
                writer.AddDocument(doc);
                return writer;
            });
        }

        public void AddDocumentAll(List<Document> listDoc, string index)
        {
            this.openWrite(index, (writer) =>
            {
                foreach (var doc in listDoc)
                {
                    writer.AddDocument(doc);
                }
                return writer;
            });
        }
        public void UpdateDocumentAll(List<Document> listDoc, string index)
        {
            this.openWrite(index, (writer) =>
            {
                foreach (Document doc in listDoc)
                {
                    writer.UpdateDocument(new Term("Id", doc.Get("Id")), doc);
                }
                return writer;
            });
        }

        public void UpdateDocumentById(Term term, Document doc, string index)
        {
            this.openWrite(index, (writer) =>
            {
                writer.UpdateDocument(term, doc);
                return writer;
            });
        }
        public void DeleteDocumentById(Term term, string index)
        {
            this.openWrite(index, (writer) =>
            {
                Query query = new TermQuery(term);
                writer.DeleteDocuments(query);
                return writer;
            });
        }

        public void DeleteDocumentAll(string index)
        {
            this.openWrite(index, (writer) =>
            {
                writer.DeleteAll();
                return writer;
            });
        }

        public List<T> SearchTopDocs<T>(string[] term, string text, string index, Func<TopDocs, IndexSearcher, List<T>> func)
        {
            List<T> list = this.openReader<List<T>>(index, (searcher, analyzer) =>
            {
                MultiFieldQueryParser queryParser = new MultiFieldQueryParser(
                    LUCENE_VERSION,
                    term,
                    analyzer
                    );
                queryParser.AllowLeadingWildcard = true;
                Query searchTermQuery = queryParser.Parse(text);
                TopDocs topDocs = searcher.Search(searchTermQuery, 10);
                return func(topDocs, searcher);
            });
            return list;
        }
        public List<T> AllDoc<T>(string index, Func<TopDocs, IndexSearcher, List<T>> func)
        {
            List<T> list = this.openReader<List<T>>(index, (searcher, analyzer) =>
            {
                MatchAllDocsQuery objMatchAll = new MatchAllDocsQuery();
                TopDocs topDocs = searcher.Search(objMatchAll,1000);
                return func(topDocs, searcher);
            });
            return list;
        }
        public Query WhereInTerm(Term[] Ids){
            MultiPhraseQuery multiplePhraseQuery=new MultiPhraseQuery();
            multiplePhraseQuery.Add(Ids);
            return multiplePhraseQuery;
        }
        public List<T> Execute<T>(Query[] queries,string index, Func<TopDocs, IndexSearcher, List<T>> func){
            List<T> list=this.openReader<List<T>>(index, (searcher, analyzer) =>
            {
                BooleanQuery filterQuery = new BooleanQuery();
                foreach(Query q in queries){
                    filterQuery.Add(new BooleanClause(q,Occur.SHOULD));
                }
                TopDocs topDocs = searcher.Search(filterQuery, 10);
                return func(topDocs, searcher);
            });
            return list;            
        }
        public Document? FindDocByID(string ID, string index)
        {
            Document d = this.openReader<Document>(index, (searcher, analyzer) =>
            {
                Query query = new TermQuery(new Term("Id", ID));
                TopDocs topDocs = searcher.Search(query, n: 1);
                return topDocs.ScoreDocs.Length == 1 ? searcher.Doc(topDocs.ScoreDocs[0].Doc) : new Document();
            });
            return d.Fields.Count == 0 ? null : d;
        }
    }
}
