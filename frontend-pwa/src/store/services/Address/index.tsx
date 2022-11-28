import { createApi } from '@reduxjs/toolkit/query/react'
import { IAddressPerson, IAddressRequest } from '../../types/Address'
import { baseQueryWithReauth } from '../FetchBaseQuery';

export const addressApi = createApi({
    reducerPath: 'addressApi',
    tagTypes: ['Address'],
    baseQuery: baseQueryWithReauth,
    endpoints: (builder) => ({
        getAddressByArea: builder.query<IAddressPerson[],IAddressRequest>({
            query: (request) =>{
                return  `Location/FindAddressesByArea/${request?.longitudeFrom}/${request?.latitudeFrom}/${request?.longitudeTo}/${request?.latitudeTo}`
            },
            providesTags:[{type:"Address", id:"List"}]
        })
    })
});
export const { 
    useGetAddressByAreaQuery
} = addressApi;