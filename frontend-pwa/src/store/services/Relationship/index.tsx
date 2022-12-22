import { createApi } from '@reduxjs/toolkit/query/react'
import { IRelationShip, IRelationshipByPerson, IRelationShipTypes } from '../../types/delete-Relationship';
import { ICreateRelationshipType, IRelationshipType, IRelationshipTypesGrouped } from '../../types/delete-RelationshipType';
import { baseQueryWithReauth } from '../FetchBaseQuery';

export const relationshipApi = createApi({
    reducerPath: 'relationshipApi',
    tagTypes: ['Relationship','RelationshipType'],
    baseQuery: baseQueryWithReauth,
    endpoints: (builder) => ({
        getRelationShipsByPersonId: builder.query<IRelationshipByPerson[], string>({
            query: (personId) => `Relationship/GetRelationShipsByPersonId/${personId}`,
            providesTags:[{type:"Relationship", id:"List"}]
        }),
        getRelationShipTypes: builder.query<IRelationShipTypes[],void>({
            query: () => `Relationship/GetRelationShipTypes`,
            providesTags:[{type:"Relationship", id:"TypeList"}]
        }),
        getRelationshipTypesGrouped: builder.query<IRelationshipTypesGrouped[],void>({
            query: () => `Relationship/GetRelationShipGroupedTypes`,
            providesTags:[{type:"RelationshipType", id:"RelationshipTypeList"}]
        }),
        createRelationshipType: builder.mutation<string, ICreateRelationshipType>({
            query: (realtionShip) => ({
                url: 'Relationship/CreateRelationshipType',
                method: "POST",
                body: realtionShip
            }),
            invalidatesTags: (result, error, relation ) => [{ type: 'RelationshipType', id:"RelationshipTypeList" }],
        }),
        updateRelationshipType: builder.mutation<string, IRelationshipType>({
            query: (realtionShip) => ({
                url: 'Relationship/UpdateRelationshipType',
                method: "PUT",
                body: realtionShip
            }),
            invalidatesTags: (result, error, relation ) => [{ type: 'RelationshipType', id:"RelationshipTypeList" }],
        }),
        createRelationship: builder.mutation<string, IRelationShip>({
            query: (realtionShip) => ({
                url: 'Relationship',
                method: "POST",
                body: realtionShip
            }),
            invalidatesTags: (result, error, relation ) => [{ type: 'Relationship', id:"List" }],
        }),
        deleteRelationship: builder.mutation<string,string>({
            query: (id) => ({
                url: `Relationship/${id}`,
                method: "DELETE",
                body: {id:id}
            }),
            invalidatesTags:  [{ type: 'Relationship', id:"List" }],
        }),
        deleteRelationshipType: builder.mutation<string,string>({
            query: (id) => ({
                url: `Relationship/DeleteRelationshipType/${id}`,
                method: "DELETE",
                body: {id:id}
            }),
            invalidatesTags:  [{ type: 'RelationshipType', id:"RelationshipTypeList" }],
        })
    })
});
export const { 
    useCreateRelationshipMutation,
    useDeleteRelationshipMutation,
    useGetRelationShipsByPersonIdQuery,
    useGetRelationShipTypesQuery,
    useCreateRelationshipTypeMutation,
    useGetRelationshipTypesGroupedQuery,
    useUpdateRelationshipTypeMutation,
    useDeleteRelationshipTypeMutation
} = relationshipApi;