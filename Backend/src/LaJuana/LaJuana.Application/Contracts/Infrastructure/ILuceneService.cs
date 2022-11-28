
using Lucene.Net.Analysis;
using Lucene.Net.Analysis.Standard;
using Lucene.Net.Documents;
using Lucene.Net.Index;
using Lucene.Net.QueryParsers.Classic;
using Lucene.Net.Search;
using Lucene.Net.Store;
using Lucene.Net.Util;

namespace LaJuana.Application.Contracts.Infrastructure
{
    public interface ILuceneService
    {
        void AddDocument(Document doc, string index);
        List<T> SearchTopDocs<T>(string[] term, string text, string index, Func<TopDocs, IndexSearcher, List<T>> func);
        List<T> AllDoc<T>(string index, Func<TopDocs, IndexSearcher, List<T>> func);
        
        void AddDocumentAll(List<Document> listDoc, string index);
        void UpdateDocumentAll(List<Document> listDoc, string index);
        void UpdateDocumentById(Term term, Document doc, string index);
        void DeleteDocumentById(Term term, string index);
        void DeleteDocumentAll(string index);
        Document? FindDocByID(string ID, string index);
        Query WhereInTerm(Term[] Ids);
        List<T> Execute<T>(Query[] queries,string index, Func<TopDocs, IndexSearcher, List<T>> func);
    }
}
