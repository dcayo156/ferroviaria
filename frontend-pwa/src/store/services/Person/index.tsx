import { BaseQueryFn, createApi, FetchArgs, fetchBaseQuery, FetchBaseQueryError } from '@reduxjs/toolkit/query/react'
import { IPerson,IPersonSimple,IPersonWithTag } from '../../types/Person'
import { URL_API_V1 } from '..'
import { RootState } from '../..'
import { baseQueryWithReauth } from '../FetchBaseQuery';


export const personApi = createApi({
    reducerPath: 'personApi',
    tagTypes: ['Person'],
    baseQuery:baseQueryWithReauth ,
    endpoints: (builder) => ({
        getFindPeopleById: builder.query<IPerson, string|null>({
            query: (id: string | null) => `People/FindPeopleById/${id}`,
            providesTags:[{type:"Person", id:"Find"}]
        }),
        getListPeople: builder.query<IPerson[], void>({
            query: () => `People`,
            providesTags:[{type:"Person", id:"List"}],
            
        }),
        createPeople: builder.mutation<string,IPersonWithTag>({
            query: (personWithTags) => ({
                url: 'People',
                method: "POST",
                body: {...personWithTags,tags:personWithTags.tags.map(t=>t.id)}
            }),
            invalidatesTags: (result, error, id ) => [{ type: 'Person', id:"List" }],
        }),
        updatePeople: builder.mutation<IPerson, Partial<IPerson>>({
            query: (person) => ({
                url: 'People',
                method: "PUT",
                body: person
            }),
            invalidatesTags: (result, error, { id }) => [{ type: 'Person', id:"List" }],
        }),
        deletePeople: builder.mutation<string,string>({
            query: (id) => ({
                url: `People/${id}`,
                method: "DELETE",
                body: {id:id}
            }),
            invalidatesTags:  [{ type: 'Person', id:"List" }],
        })
    })
});
export const { 
    useGetFindPeopleByIdQuery, 
    useGetListPeopleQuery,
    useCreatePeopleMutation,
    useUpdatePeopleMutation,
    useDeletePeopleMutation
} = personApi;