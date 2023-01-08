import { BaseQueryFn, createApi, FetchArgs, fetchBaseQuery, FetchBaseQueryError } from '@reduxjs/toolkit/query/react'
import { IAccessProgram } from '../../types/AccessProgram';
import { baseQueryWithReauth } from '../FetchBaseQuery';


export const accessProgramApi = createApi({
    reducerPath: 'accessProgramApi',
    tagTypes: ['AccessProgram'],
    baseQuery:baseQueryWithReauth ,
    endpoints: (builder) => ({
        getFindProgramById: builder.query<IAccessProgram, string|null>({
            query: (id: string | null) => `Programs/FindProgramsById/${id}`,
            providesTags:[{type:"AccessProgram", id:"Find"}]
        }),
        getListProgram: builder.query<IAccessProgram[], void>({
            query: () => `Programs/GetPrograms`,
            providesTags:[{type:"AccessProgram", id:"List"}],
            
        }),
        getFileProgram: builder.query<string, string>({
            query: (id) => `Programs/FindProgramsFileById/${id}`,
            providesTags:[{type:"AccessProgram", id:"List"}],
            
        }),
        createProgram: builder.mutation<string,IAccessProgram>({
            query: (accessProgramWithTags) => ({
                url: 'Programs/CreatePrograms',
                method: "POST",
                body: accessProgramWithTags
            }),
            invalidatesTags: (result, error, id ) => [{ type: 'AccessProgram', id:"List" }],
        }),
        updateProgram: builder.mutation<IAccessProgram, IAccessProgram>({
            query: (accessProgram) => ({
                url: 'Programs/UpdatePrograms',
                method: "PUT",
                body: accessProgram
            }),
            invalidatesTags: (result, error, { id }) => [{ type: 'AccessProgram', id:"List" }],
        }),
        deleteProgram: builder.mutation<string,string>({
            query: (id) => ({
                url: `Programs/DeletePrograms/${id}`,
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
    useGetFileProgramQuery,
    useCreateProgramMutation,
    useUpdateProgramMutation,
    useDeleteProgramMutation
} = accessProgramApi;