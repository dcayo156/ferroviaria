import { createApi} from '@reduxjs/toolkit/query/react'
import { IInspectionTrain,IInspectionTrainCreate} from '../../types/InspectionTrain';
import { baseQueryWithReauth } from '../FetchBaseQuery';


export const inspectionTrainApi = createApi({
    reducerPath: 'inspectionTrainApi',
    tagTypes: ['InspectionTrain','InspectionTrainsImage'],
    baseQuery:baseQueryWithReauth ,
    endpoints: (builder) => ({
        getFindInspectionTrainsById: builder.query<IInspectionTrain, string|null>({
            query: (id: string | null) => `InspectionTrains/FindInspectionTrainsById/${id}`,
            providesTags:[{type:"InspectionTrain", id:"List"}],
        }),
        getListInspectionTrains: builder.query<IInspectionTrain[], void>({
            query: () => `InspectionTrains/GetInspectionTrains`,
            providesTags:[{type:"InspectionTrain", id:"List"}],
            
        }),
        getFileInspectionTrains: builder.query<string, string>({
            query: (id) => `InspectionTrains/FindInspectionTrainsFileById/${id}`,
            providesTags:[{type:"InspectionTrain", id:"List"}],
            
        }),
        createInspectionTrains: builder.mutation<string,IInspectionTrainCreate>({
            query: (InspectionTrains) => ({
                url: 'InspectionTrains/CreateInspectionTrains',
                method: "POST",
                body: InspectionTrains
            }),
            invalidatesTags: (result, error, id ) => [{ type: 'InspectionTrain', id:"List" }],
        }),
        getFileDocAllInspectionTrains: builder.query<string, string>({
            query: () => `InspectionTrains/GetInspectionTrainsAll/`,
            providesTags:[{type:"InspectionTrain", id:"List"}],
            
        }),
        // createFileInspectionTrains: builder.mutation<IInspectionTrainsImageResponse,IInspectionTrainsImage>({
        //     query: (InspectionTrainsImage) => ({
        //         url: 'InspectionTrainss/CreateInspectionTrainssFile',
        //         method: "POST",
        //         body: InspectionTrainsImage
        //     }),
        //     invalidatesTags: (result, error, id ) => [{ type: 'InspectionTrainsImage', id:"ListInspectionTrains" }],
        // }),
        // updateInspectionTrains: builder.mutation<IInspectionTrains, IInspectionTrains>({
        //     query: (InspectionTrains) => ({
        //         url: 'InspectionTrainss/UpdateInspectionTrainss',
        //         method: "PUT",
        //         body: InspectionTrains
        //     }),
        //     invalidatesTags: (result, error, { id }) => [{ type: 'InspectionTrains', id:"List" }],
        // }),        
        deleteInspectionTrains: builder.mutation<string,string>({
            query: (id) => ({
                url: `InspectionTrainss/DeleteInspectionTrains/${id}`,
                method: "DELETE",
                body: {id:id}
            }),
            invalidatesTags:  [{ type: 'InspectionTrain', id:"List" }],
        }),
        getInspectionTrainsForYear: builder.query<any, void >({
            query: () => `InspectionTrains/GetInspectionTrainsForYear`,
            providesTags:[{type:"InspectionTrain", id:"List"}],            
        }),
    })
});
export const { 
    useGetFindInspectionTrainsByIdQuery, 
    useGetListInspectionTrainsQuery,
    useGetFileInspectionTrainsQuery,
    useCreateInspectionTrainsMutation,
    // useCreateFileInspectionTrainsMutation,
    // useUpdateInspectionTrainsMutation,
    useGetFileDocAllInspectionTrainsQuery,
    useDeleteInspectionTrainsMutation,
    useGetInspectionTrainsForYearQuery
} = inspectionTrainApi;