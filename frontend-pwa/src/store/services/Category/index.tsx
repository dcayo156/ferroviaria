import { BaseQueryFn, createApi, FetchArgs, fetchBaseQuery, FetchBaseQueryError } from '@reduxjs/toolkit/query/react'
import { ICategory,ICategoryWithParent } from '../../types/Category';
import { baseQueryWithReauth } from '../FetchBaseQuery';


export const categoryApi = createApi({
    reducerPath: 'categoryApi',
    tagTypes: ['Category'],
    baseQuery:baseQueryWithReauth ,
    endpoints: (builder) => ({
        getFindCategoryById: builder.query<ICategory, string|null>({
            query: (id: string | null) => `Categories/FindCategoriesById/${id}`,
            providesTags:[{type:"Category", id:"Find"}]
        }),
        getListCategory: builder.query<ICategoryWithParent[], void>({
            query: () => `Categories/GetCategories`,
            providesTags:[{type:"Category", id:"List"}],
            
        }),
        createCategory: builder.mutation<string,ICategory>({
            query: (categoryWithTags) => ({
                url: 'Categories/CreateCategories',
                method: "POST",
                body: categoryWithTags
            }),
            invalidatesTags: (result, error, id ) => [{ type: 'Category', id:"List" }],
        }),
        updateCategory: builder.mutation<ICategory, ICategory>({
            query: (category) => ({
                url: 'Categories/UpdateCategories',
                method: "PUT",
                body: category
            }),
            invalidatesTags: (result, error, { id }) => [{ type: 'Category', id:"List" }],
        }),
        deleteCategory: builder.mutation<string,string>({
            query: (id) => ({
                url: `Categories/DeleteCategories/${id}`,
                method: "DELETE",
                body: {id:id}
            }),
            invalidatesTags:  [{ type: 'Category', id:"List" }],
        })
    })
});
export const { 
    useGetFindCategoryByIdQuery, 
    useGetListCategoryQuery,
    useCreateCategoryMutation,
    useUpdateCategoryMutation,
    useDeleteCategoryMutation
} = categoryApi;