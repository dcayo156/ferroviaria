import { BaseQueryFn, createApi, FetchArgs, fetchBaseQuery, FetchBaseQueryError } from '@reduxjs/toolkit/query/react'
import { IAccessProgram } from '../../types/AccessProgram'
import { baseQueryWithReauth } from '../FetchBaseQuery';


export const accessProgramApi = createApi({
    reducerPath: 'accessProgramApi',
    tagTypes: ['AccessProgram'],
    baseQuery:baseQueryWithReauth ,
    endpoints: (builder) => ({
        getFindProgramById: builder.query<IAccessProgram, string|null>({
            query: (id: string | null) => `Program/FindProgramById/${id}`,
            providesTags:[{type:"AccessProgram", id:"Find"}]
        }),
        getListProgram: builder.query<IAccessProgram[], void>({
            query: () => `Program/GetPrograms`,
            providesTags:[{type:"AccessProgram", id:"List"}],
            
        }),
        createProgram: builder.mutation<string,IAccessProgram>({
            query: (accessProgramWithTags) => ({
                url: 'Program/CreatePrograms',
                method: "POST",
                body: accessProgramWithTags
            }),
            invalidatesTags: (result, error, id ) => [{ type: 'AccessProgram', id:"List" }],
        }),
        updateProgram: builder.mutation<IAccessProgram, Partial<IAccessProgram>>({
            query: (accessProgram) => ({
                url: 'Program/UpdatePrograms',
                method: "PUT",
                body: accessProgram
            }),
            invalidatesTags: (result, error, { id }) => [{ type: 'AccessProgram', id:"List" }],
        }),
        deleteProgram: builder.mutation<string,string>({
            query: (id) => ({
                url: `Program/DeletePrograms/${id}`,
                method: "DELETE",
                body: {id:id}
            }),
            invalidatesTags:  [{ type: 'AccessProgram', id:"List" }],
        })
    })
});
export const { 
    useGetFindProgramByIdQuery, 
    useGetListProgramQuery,
    useCreateProgramMutation,
    useUpdateProgramMutation,
    useDeleteProgramMutation
} = accessProgramApi;