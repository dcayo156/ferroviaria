import { BaseQueryFn, createApi, FetchArgs, fetchBaseQuery, FetchBaseQueryError } from '@reduxjs/toolkit/query/react'
import { IDocument,IDocumentImage,IDocumentImageResponse } from '../../types/Document';
import { baseQueryWithReauth } from '../FetchBaseQuery';


export const documentApi = createApi({
    reducerPath: 'documentApi',
    tagTypes: ['Document','DocumentImage'],
    baseQuery:baseQueryWithReauth ,
    endpoints: (builder) => ({
        getFindDocumentById: builder.query<IDocument, string|null>({
            query: (id: string | null) => `Documents/FindDocumentsById/${id}`,
            providesTags:[{type:"Document", id:"List"}],
        }),
        getListDocument: builder.query<IDocument[], void>({
            query: () => `Documents/GetDocuments`,
            providesTags:[{type:"Document", id:"List"}],
            
        }),
        getFileDocument: builder.query<string, string>({
            query: (id) => `Documents/FindDocumentsFileById/${id}`,
            providesTags:[{type:"Document", id:"List"}],
            
        }),
        createDocument: builder.mutation<string,IDocument>({
            query: (document) => ({
                url: 'Documents/CreateDocuments',
                method: "POST",
                body: document
            }),
            invalidatesTags: (result, error, id ) => [{ type: 'Document', id:"List" }],
        }),
        createFileDocument: builder.mutation<IDocumentImageResponse,IDocumentImage>({
            query: (documentImage) => ({
                url: 'Documents/CreateDocumentsFile',
                method: "POST",
                body: documentImage
            }),
            invalidatesTags: (result, error, id ) => [{ type: 'DocumentImage', id:"ListDocument" }],
        }),
        updateDocument: builder.mutation<IDocument, IDocument>({
            query: (document) => ({
                url: 'Documents/UpdateDocuments',
                method: "PUT",
                body: document
            }),
            invalidatesTags: (result, error, { id }) => [{ type: 'Document', id:"List" }],
        }),
        deleteDocument: builder.mutation<string,string>({
            query: (id) => ({
                url: `Documents/DeleteDocuments/${id}`,
                method: "DELETE",
                body: {id:id}
            }),
            invalidatesTags:  [{ type: 'Document', id:"List" }],
        })
    })
});
export const { 
    useGetFindDocumentByIdQuery, 
    useGetListDocumentQuery,
    useGetFileDocumentQuery,
    useCreateDocumentMutation,
    useCreateFileDocumentMutation,
    useUpdateDocumentMutation,
    useDeleteDocumentMutation
} = documentApi;